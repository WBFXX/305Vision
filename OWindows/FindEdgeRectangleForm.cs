using _305Vision.BLL;
using _305Vision.DAL;
using _305Vision.Model;
using _305Vision.SDK;
using NLog;
using NLog.Fluent;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.OWindows
{
    public partial class FindEdgeRectangleForm : Form
    {
        Logger logger = LogManager.GetCurrentClassLogger();


        #region 公共图像参数
        private Image overImage;
        private Image resouseImage;
        private Point start;//起点坐标
        private Point end;//终点坐标
        private double angle;//矩形旋转角度
        private Size Lsize;//留白大小
        private double wrate;//宽缩放比率
        private double hrate;//高缩放比率
        private bool isMove = false;//记录鼠标是否在画框
        private int edgeNum;//找边数量
        BasicImageInfo BasicImageInfo;//图形基础
        


        /// <summary>
        /// 处理好的图片
        /// </summary>
        public Image OverImage { get => overImage; set => overImage = value; }
        /// <summary>
        /// 处理前的图片
        /// </summary>
        public Image ResouseImage { get => resouseImage; set => resouseImage = value; }
        public int EdgeNum { get => edgeNum; set => edgeNum = value; }
        public Point Start { get => start; set => start = value; }
        public Point End { get => end; set => end = value; }
        public double Angle { get => angle; set => angle = value; }

        #endregion
        //返回数值


        public FindEdgeRectangleForm()
        {
            InitializeComponent();

        }

        private void RetangelROI_Load(object sender, EventArgs e)
        {
            //在图像加载的时候就把图像源加载好
            pictureBox1.Image = ResouseImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(pictureBox1.Image != null)
            {
                
                overImage = pictureBox1.Image;
                logger.Info("ROI点图像处理完成。");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            Point clientMouse = e.Location;

            resouseImage = pictureBox1.Image;
            // 获取 PictureBox 显示的图片
            Image image = pictureBox.Image;

            if (image != null )
            {
                //获取留白
                Lsize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                //宽度缩放比率
                wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                //高度缩放比率
                hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                double startx = ((double)((clientMouse.X - Lsize.Width) * wrate));
                double starty = ((double)((clientMouse.Y - Lsize.Height) * hrate));
                Start = new Point((int)startx, (int)starty);//记录开始坐标
                isMove =true;
                logger.Info("起点真坐标为：" + Start);
            }
            else
            {
                MessageBox.Show("当前窗口无图像");
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove)
            {
                PictureBox pictureBox = sender as PictureBox;
                
                if (pictureBox != null)
                {
                    Point clientMouse = e.Location;
                    //获取留白
                    Lsize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    //宽度缩放比率
                    wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    //高度缩放比率
                    hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    double x = ((double)((clientMouse.X - Lsize.Width) * wrate));
                    double y = ((double)((clientMouse.Y - Lsize.Height) * hrate));
                    End = new Point((int)x, (int)y);//记录结束坐标


                    // 调用方法，处理原始图片的像素位置
                    Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)resouseImage,
                            imageData =>
                            {
                                // 具体的处理逻辑
                                unsafe
                                {
                                    try
                                    {
                                        BasicImageInfo = new BasicImageInfo()
                                        {
                                            ImagePtr = imageData.Scan0,
                                            Width = imageData.Width,
                                            Height = imageData.Height,
                                            Stride = imageData.Stride
                                        };
                                        logger.Info("截取宽度：" + Math.Abs(End.X - Start.X));
                                        logger.Info("截取高度" + Math.Abs(End.Y - Start.Y));
                                        logger.Info("起点坐标：" + Start + ";" + "终点坐标：" + End);
                                        byte* imageDataPtr = OpenCVSDK.findEdgeRectangle(BasicImageInfo.ImagePtr, (int)BasicImageInfo.Width,(int)BasicImageInfo.Height,(int)BasicImageInfo.Stride
                                            , Start.X, Start.Y, End.X, End.Y, Angle, EdgeNum);
                                        // 处理后的数据流复制到托管数组
                                        
                                            int size = imageData.Width * imageData.Height *3;
                                            byte[] imageByte = new byte[size];
                                            Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                                            OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);
                                            //bytess = imageByte;
                                            return imageByte;
                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                        return null;
                                    }
                                }
                            });
                    //把处理完的图像传给当前显示窗口
                    pictureBox.Image = processedImage;

                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;

        }

        
    }
}
