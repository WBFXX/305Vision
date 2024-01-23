using _305Vision.BLL;
using _305Vision.DAL;
using _305Vision.Model;
using _305Vision.SDK;
using NLog;
using NLog.Fluent;
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
    public partial class RecROIForm : Form
    {
        Logger logger = LogManager.GetCurrentClassLogger();


        #region 公共图像参数
        private Image overImage;
        private Image resouseImage;
        private Point start;//起点坐标
        private Point end;//终点坐标
        private double Angle;//旋转角度
        private Size Lsize;//留白大小
        private double wrate;//宽缩放比率
        private double hrate;//高缩放比率
        private bool isMove = false;//记录鼠标是否在画框




        /// <summary>
        /// 处理好的图片
        /// </summary>
        public Image OverImage { get => overImage; set => overImage = value; }
        /// <summary>
        /// 处理前的图片
        /// </summary>
        public Image ResouseImage { get => resouseImage; set => resouseImage = value; }
        public Point Start { get => start; set => start = value; }
        public Point End { get => end; set => end = value; }
        public double Angle1 { get => Angle; set => Angle = value; }
        #endregion


        public RecROIForm()
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
            // 设置对话框的返回值为 OK
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
                    
                    //if ((clientMouse.X - start.X) * 3 % 4 == 1 || (clientMouse.X - start.X) * 3 <= 4)
                    //{
                        double x = ((double)((clientMouse.X - Lsize.Width) * wrate));
                        double y = ((double)((clientMouse.Y - Lsize.Height) * hrate));
                        End = new Point((int)x, (int)y);//记录结束坐标
                    //}
                        
                    // 调用方法，处理原始图片的像素位置
                    Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)resouseImage,
                            imageData =>
                            {
                                // 具体的处理逻辑
                                unsafe
                                {
                                    try
                                    {
                                        //if ((end.X - start.X) * 3 % 4 == 0 || (end.X - start.X) * 3 <= 4)
                                        //{
                                            logger.Info("截取宽度：" + Math.Abs(End.X - Start.X));
                                            logger.Info("截取高度：" + Math.Abs(End.Y - Start.Y));
                                            logger.Info("起点坐标：" + Start + ";" + "终点坐标：" + End);
                                            byte* imageDataPtr = OpenCVSDK.drawRotatedRect(imageData.Scan0, imageData.Width, imageData.Height, imageData.Stride
                                            , Start.X, Start.Y, End.X, End.Y, 255, 0, 200, 0);
                                            
                                            // 处理后的数据流复制到托管数组
                                            int size = imageData.Width * imageData.Height * 3;
                                            byte[] imageByte = new byte[size];
                                            Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                                            OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);
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



        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = (Bitmap) resouseImage;

            // 调用方法，处理原始图片的像素位置
            Bitmap processedImage = ProcessImageDAL.ProcessCoriImage((Bitmap)bitmap,
                    imageData =>
                    {
                        // 具体的处理逻辑
                        unsafe
                        {
                            try
                            {
                                //this.aWidth = ((end.X - start.X) * 3 + 4 - (end.X - start.X) * 3 % 4) / 3;//475
                                int aWidth = Math.Abs(End.X - Start.X);//475
                                //logger.Info("abba是：" + a + ". " + "awidth是：" + aWidth);
                                int aHeight = Math.Abs(End.Y - Start.Y);

                                 byte* imageDataPtr = OpenCVSDK.roiCropping(imageData.Scan0, imageData.Width, imageData.Height, imageData.Stride
                                 , Start.X, Start.Y, End.X, End.Y, 255, 0, 200, 0);
                                int a=0;
                                OpenCVSDK.abba(ref a);
                                logger.Info(a);
                                //int b = OpenCVSDK.abbb();

                                int size = (aWidth+3)/4*4 * aHeight *3;
                                
                                // 处理后的数据流复制到托管数组
                                
                                //int size = this.aWidth  * aHeight *3;

                                
                                byte[] imageByte = new byte[size];
                                Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                                OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);
                                return imageByte;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                return null;
                            }
                        }
                    }, (Math.Abs(End.X-Start.X) + 3) / 4 * 4, Math.Abs(End.Y - Start.Y));
            //logger.Info(aWidth);
            
            //把处理完的图像传给当前显示窗口
            pictureBox1.Image = (Bitmap)processedImage;
            //logger.Info(processedImage.Size.ToString());//476
            

            logger.Info("裁剪成功。");
            // 设置对话框的返回值为 OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
