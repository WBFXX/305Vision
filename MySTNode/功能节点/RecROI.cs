using _305Vision.BLL;
using _305Vision.DAL;
using _305Vision.Model;
using _305Vision.MySTNode.控件库;
using _305Vision.SDK;
using _305Vision.图片操作测试;
using NLog;
using ST.Library.UI.NodeEditor;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace _305Vision.MySTNode.功能节点
{
    /// <summary>
    /// 矩形ROI
    /// </summary>
    [STNode("/ROI功能节点", "在图像上画点")]
    public class FindEdgeRectangle : ImageBaseNode
    {
        private OWindows.RecROIForm recROIForm = new OWindows.RecROIForm();

        [STNodeProperty("开始坐标", "开始坐标")]
        public Point Start { set; get; }
        [STNodeProperty("终点坐标", "终点坐标")]
        public Point End { set; get; }
        [STNodeProperty("旋转角度", "旋转角度")]
        public double Angle { set; get; }

        protected override void OnCreate()
        {
            base.OnCreate();

            this.Title = "矩形ROI";

            this.AutoSize = false;
            this.Height += 30;
            var ctrl = new STNodeButton();
            ctrl.Text = "选取";
            ctrl.Location = new Point(42, 110);
            this.Controls.Add(ctrl);

            // 当输入节点有数据输入时候
            inOption.DataTransfer += new STNodeOptionEventHandler(Op_img_in_DataTransfer);

            ctrl.MouseClick += Owner_Click;
        }

        private void Owner_Click(object sender, EventArgs e)
        {
            // 创建 RecROIForm 类的实例，并传递参数
            recROIForm.Start = Start;
            recROIForm.End = End;
            recROIForm.Angle = Angle;

            // 实例化图像处理窗口
            DialogResult result = recROIForm.ShowDialog();

            // 检查用户的操作
            if (result == DialogResult.Cancel)
            {
                return; // 用户选择了关闭，提前退出方法
            }

            // 选取完框框，展示图像
            Start = recROIForm.Start;
            End = recROIForm.End;
            Angle = recROIForm.Angle;

            m_img_draw = recROIForm.OverImage;
            m_op_img_out.TransferData(recROIForm.OverImage); // 输出
            this.Invalidate();
        }

        void Op_img_in_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            // 如果当前不是连接状态或者接受到的数据为空
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {
                m_op_img_out.TransferData(null); // 向所有输出节点输出空数据
                m_img_draw = null;               // 需要绘制显示的图片置为空
            }
            //else if (e.Status == ConnectionStatus.Connected && m_img_draw == null)
            //{
            //    Bitmap img = (Bitmap)e.TargetOption.Data;

            //    // 把图像传给操作表格
            //    recROIForm.ResouseImage = (Image)img;
            //    m_op_img_out.TransferData((Image)img); // 输出
            //    m_img_draw = (Image)img;
            //    this.Invalidate();
            //}
            else
            {
                Bitmap img = (Bitmap)e.TargetOption.Data;
                if (isSecond)
                {
                    #region 矩形裁剪算法
                    Bitmap processedImage = ProcessImageDAL.ProcessCoriImage((Bitmap)img, imageData =>
                    {
                        unsafe
                        {
                            int aWidth = Math.Abs(End.X - Start.X);
                            int aHeight = Math.Abs(End.Y - Start.Y);

                            // 保存提取数据结构
                            BasicImageInfo info = BasicImageInfo.NewMethod(imageData);
                            byte* imageDataPtr = OpenCVSDK.roiCropping(info.ImagePtr, (int)info.Width, (int)info.Height, (int)info.Stride, Start.X, Start.Y, End.X, End.Y, 255, 0, 200, Angle);

                            int size = (aWidth + 3) / 4 * 4 * aHeight * 3;
                            byte[] imageByte = new byte[size];
                            Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                            OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);
                            return imageByte;
                        }
                    }, (Math.Abs(End.X - Start.X) + 3) / 4 * 4, Math.Abs(End.Y - Start.Y));
                    #endregion

                    // 把图像传给操作表格
                    recROIForm.OverImage = (Image)processedImage;
                    m_op_img_out.TransferData((Image)processedImage); // 输出
                    m_img_draw = (Image)processedImage;
                    this.Invalidate();
                }
                else
                {
                    // 把图像传给操作表格
                    recROIForm.ResouseImage = (Image)img;
                    m_op_img_out.TransferData((Image)img);
                    isSecond = true;
                }
                
            }
        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            // 非 static 需要先实例化
            STNodeBLL.DrawBody(dt, m_img_draw, this.Left, this.Top);
        }
    }
}
