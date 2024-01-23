using _305Vision.SDK;
using _305Vision.图片操作测试;
using NLog;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _305Vision.BLL;
using _305Vision.OWindows;
using _305Vision.MySTNode.控件库;
using _305Vision.Model;
using _305Vision.DAL;

namespace _305Vision.MySTNode.功能节点
{


    /// <summary>
    /// 灰度处理
    /// </summary>
    [STNode("/ROI功能节点", "在图像上画点")]
    public class FindEdgeRectangle : ImageBaseNode
    {
        private STNodeOption in_option;
        private OWindows.RecROIForm RetangelROI = new OWindows.RecROIForm();
        [STNodeProperty("开始坐标", "开始坐标")]
        public Point Start { set; get; }
        [STNodeProperty("终点坐标", "终点坐标")]
        public Point End { set; get; }
        [STNodeProperty("旋转角度", "旋转角度")]
        public double Angle { set; get; }



        //private STNodeOption out_option;

        protected override void OnCreate()
        {
            base.OnCreate();
           

            this.Title = "矩形ROI";
            in_option = this.InputOptions.Add("输入图像", typeof(Image), true);
            
            this.AutoSize = false;
            this.Height += 30;
            var ctrl = new STNodeButton();
            ctrl.Text = "选取";
            ctrl.Location = new Point(42, 110);
            this.Controls.Add(ctrl);
            

            //当输入节点有数据输入时候
            in_option.DataTransfer += new STNodeOptionEventHandler(Op_img_in_DataTransfer);
            
            ctrl.MouseClick += Owner_Click;
        }


        
        private void Owner_Click(object sender, EventArgs e)
        {
            //创建RetangelROI类的实例，并传递RetangelROIInfo类的实例
            //点击按钮的时候把参数传过去而不是OnCreate时候
            RetangelROI.Start = Start;
            RetangelROI.End = End;
            RetangelROI.Angle1 = Angle;

            //创建RetangelROI类的实例，并传递RetangelROIInfo类的实例
            //实例化图像处理窗口
            DialogResult result = RetangelROI.ShowDialog();
            // 检查用户的操作
            if (result == DialogResult.Cancel)
            {
                return; // 用户选择了关闭，提前退出方法
            }


            //选取完框框，展示图像
            Start = RetangelROI.Start;
            End = RetangelROI.End;
            Angle = RetangelROI.Angle1;

            m_img_draw = RetangelROI.OverImage;
            m_op_img_out.TransferData(RetangelROI.OverImage);//out选项 输出
            this.Invalidate();

        }
        
        void Op_img_in_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            //如果当前不是连接状态 或者 接受到的数据为空
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {

                m_op_img_out.TransferData(null);    //向所有输出节点输出空数据
                m_img_draw = null;                  //需要绘制显示的图片置为空
                
            }
            else if (e.Status == ConnectionStatus.Connected && m_img_draw == null)
            {
                Bitmap img = (Bitmap)e.TargetOption.Data;

                //把图像传给操作表格
                RetangelROI.ResouseImage = (Image)img;
                m_op_img_out.TransferData((Image)img);//out选项 输出
                m_img_draw = (Image)img;
                this.Invalidate();

            }
            else
            {
                #region 矩形裁剪算法
                Bitmap img = (Bitmap)e.TargetOption.Data;
                Bitmap processedImage = ProcessImageDAL.ProcessCoriImage((Bitmap)img,imageData =>
                {
                    unsafe
                    {
                        int aWidth = Math.Abs(End.X - Start.X);
                        int aHeight = Math.Abs(End.Y - Start.Y);
                        //保存提取数据结构
                        BasicImageInfo info = BasicImageInfo.NewMethod(imageData);
                        byte* imageDataPtr = OpenCVSDK.roiCropping(info.ImagePtr, (int)info.Width, (int)info.Height,(int)info.Stride, Start.X, Start.Y, End.X, End.Y, 255, 0, 200, Angle);
                        int size = (aWidth + 3) / 4 * 4 * aHeight * 3;
                        byte[] imageByte = new byte[size];
                        Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                        OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);
                        return imageByte;

                    }
                }, (Math.Abs(End.X - Start.X) + 3) / 4 * 4, Math.Abs(End.Y - Start.Y));
                #endregion

                //把图像传给操作表格
                RetangelROI.ResouseImage = (Image)processedImage;
                m_op_img_out.TransferData((Image)processedImage);//out选项 输出
                m_img_draw = (Image)processedImage;
                this.Invalidate();
            }
        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            //非static 需要先实例化
            STNodeBLL.DrawBody(dt, m_img_draw,this.Left,this.Top);
        }

        

    }
}
