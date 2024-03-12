using _305Vision.BLL;
using _305Vision.Model;
using _305Vision.MySTNode.控件库;
using _305Vision.SDK;
using _305Vision.图片操作测试;
using NLog;
using ST.Library.UI.NodeEditor;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace _305Vision.MySTNode.功能节点
{
    /// <summary>
    /// 灰度处理
    /// </summary>
    [STNode("/ROI功能节点", "在图像上画点")]
    public class DotROI : ImageBaseNode
    {
        private OWindows.DotROIForm dotROIForm = new OWindows.DotROIForm();

        internal BasicImageInfo BasicImageInfo { get; set; }

        public Image ResouseImage { get; set; }

        [STNodeProperty("点坐标", "开始坐标")]
        public System.Drawing.Point Point { get; set; }

        [STNodeProperty("点颜色", "开始坐标")]
        public Color Color { get; set; }

        [STNodeProperty("点长度", "开始坐标")]
        public int Size { get; set; }

        [STNodeProperty("点大小", "开始坐标")]
        public int Thickness { get; set; }

        private bool isMove = false; // 记录目标是否在画框

        protected override void OnCreate()
        {
            base.OnCreate();
            // 初始化参数
            Color = Color.FromArgb(255, 0, 200);
            Thickness = 2;
            Size = 20;

            this.Title = "画点";

            this.AutoSize = false;
            this.Height += 30;

            var ctrl = new STNodeButton();
            ctrl.Text = "绘制";
            ctrl.Location = new System.Drawing.Point(42, 110);
            this.Controls.Add(ctrl);

            // 当输入节点有数据输入时候
            inOption.DataTransfer += new STNodeOptionEventHandler(Op_img_in_DataTransfer);

            ctrl.MouseClick += Owner_Click;
        }

        private void Owner_Click(object sender, EventArgs e)
        {
            // 点击按钮的时候把参数传过去而不是 OnCreate 时候
            dotROIForm.Point1 = Point;
            dotROIForm.Color = Color;
            dotROIForm.Size = Size;
            dotROIForm.Thickness = Thickness;

            // 实例化图像处理窗口
            DialogResult result = dotROIForm.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                return; // 用户选择了关闭，提前退出方法
            }

            Point = dotROIForm.Point1;
            Color = dotROIForm.Color;
            Size = dotROIForm.Size;
            Thickness = dotROIForm.Thickness;

            m_img_draw = dotROIForm.OverImage;
            m_op_img_out.TransferData(dotROIForm.OverImage); // out 选项 输出
        }

        void Op_img_in_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            // 如果当前不是连接状态 或者 接受到的数据为空
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {
                m_op_img_out.TransferData(null); // 向所有输出节点输出空数据
                m_img_draw = null; // 需要绘制显示的图片置为空
            }
            //else if (e.Status == ConnectionStatus.Connected && m_img_draw == null)
            //{
            //    Bitmap img = (Bitmap)e.TargetOption.Data;
            //    // 把图像传给操作表格
            //    dotROIForm.ResouseImage = (Image)img;
            //    m_op_img_out.TransferData((Image)img); // out 选项 输出
            //    m_img_draw = (Image)img;
            //    this.Invalidate();
            //}
            else
            {
                
                Bitmap img = (Bitmap)e.TargetOption.Data;
                if (isSecond)
                {
                    // 画点算法
                    Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)img, imageData => ProcessImageData(imageData));
                    // 把图像传给操作表格
                    dotROIForm.OverImage = (Image)processedImage;
                    m_op_img_out.TransferData((Image)processedImage); // out 选项 输出
                    m_img_draw = (Image)processedImage;
                    this.Invalidate();
                }
                else
                {
                    dotROIForm.ResouseImage = (Image)img;
                    m_op_img_out.TransferData((Image)img); // out 选项 输出
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

        private byte[] ProcessImageData(BitmapData imageData)
        {
            try
            {
                BasicImageInfo = BasicImageInfo.GetImgInfo(imageData);
                unsafe
                {
                    byte* imageDataPtr = OpenCVSDK.drawPoint(BasicImageInfo.ImagePtr, (int)BasicImageInfo.Width,
                        (int)BasicImageInfo.Height, (int)BasicImageInfo.Stride, Size, Point.X, Point.Y,
                        Color.R, Color.G, Color.B, Thickness);
                    // 处理后的数据流复制到托管数组
                    return UtilsBLL.GetImageBytes((IntPtr)imageDataPtr, imageData.Width, imageData.Height, 3);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
