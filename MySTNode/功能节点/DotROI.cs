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
using System.Windows;
using _305Vision.DAL;

namespace _305Vision.MySTNode.功能节点
{

    
    /// <summary>
    /// 灰度处理
    /// </summary>
    [STNode("/ROI功能节点", "在图像上画点")]
    public class DotROI : ImageBaseNode
    {
        private STNodeOption in_option;
        private OWindows.DotROIForm RetangelROI = new OWindows.DotROIForm();
        
        internal BasicImageInfo BasicImageInfo { get; set; }
        
        public Image ResouseImage { get ; set ; }
        [STNodeProperty("点坐标", "开始坐标")]
        public System.Drawing.Point Point { get; set; }
        [STNodeProperty("点颜色", "开始坐标")]
        public Color Color { get; set; }
        [STNodeProperty("点长度", "开始坐标")]
        public int Size { get; set; }
        [STNodeProperty("点大小", "开始坐标")]
        public int Thickness { get; set; }




        //private STNodeOption out_option;

        protected override void OnCreate()
        {
            base.OnCreate();
            //初始化参数
            Color = Color.FromArgb(255, 0, 200);
            Thickness = 2;
            Size = 20;

            this.Title = "画点";
            in_option = this.InputOptions.Add("输入图像", typeof(Image), true);
            
            this.AutoSize = false;
            this.Height += 30;
            var ctrl = new STNodeButton();
            ctrl.Text = "绘制";
            ctrl.Location = new System.Drawing.Point(42, 110);
            this.Controls.Add(ctrl);
            

            //当输入节点有数据输入时候
            in_option.DataTransfer += new STNodeOptionEventHandler(Op_img_in_DataTransfer);
            
            ctrl.MouseClick += Owner_Click;
        }


        
        private void Owner_Click(object sender, EventArgs e)
        {
            //创建RetangelROI类的实例，并传递RetangelROIInfo类的实例
            RetangelROI.Point1 = Point;
            RetangelROI.Color = Color;
            RetangelROI.Size = Size;
            RetangelROI.Thickness = Thickness;
            //RetangelROI.BasicImageInfo1 = BasicImageInfo;

            //实例化图像处理窗口
            DialogResult result = RetangelROI.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                return; // 用户选择了关闭，提前退出方法
            }

            Point = RetangelROI.Point1;
            Color = RetangelROI.Color;
            Size = RetangelROI.Size;
            Thickness = RetangelROI.Thickness;
            //BasicImageInfo = RetangelROI.BasicImageInfo1;

            m_img_draw = RetangelROI.OverImage;
            m_op_img_out.TransferData(RetangelROI.OverImage);//out选项 输出

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
                #region 画点算法
                Bitmap img = (Bitmap)e.TargetOption.Data;
                Bitmap processedImage = ProcessImageDAL.ProcessImage((Bitmap)img, imageData =>
                {
                    unsafe
                    {
                        BasicImageInfo = BasicImageInfo.NewMethod(imageData);
                        byte* imageDataPtr = OpenCVSDK.drawPoint(BasicImageInfo.ImagePtr, (int)BasicImageInfo.Width, (int)BasicImageInfo.Height, (int)BasicImageInfo.Stride, Size
                            , Point.X, Point.Y, Color.R, Color.G, Color.B, Thickness);
                        // 处理后的数据流复制到托管数组
                        int size = imageData.Width * imageData.Height * 3;
                        byte[] imageByte = new byte[size];
                        Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                        OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);
                        return imageByte;
                        
                    }
                });
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
