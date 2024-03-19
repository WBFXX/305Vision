using _305Vision.BLL;
using _305Vision.SDK;
using _305Vision.Utils;
using NLog;
using PictureWindowControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace _305Vision
{
    public partial class TestForm : Form
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public TestForm()
        {
            InitializeComponent();
        }


        public Image Image { get; set; }

        private void TestForm_Load(object sender, EventArgs e)
        {

            MessageBox.Show("测试");

            //MyPictureBox myPictureBox1 = new MyPictureBox(Image); // 使用带参数的构造函数初始化控件
            //myPictureBox1.Dock = DockStyle.Fill;

            ////myPictureBox1.Location = new Point(100,100); // 设置控件位置
            //this.Controls.Add(myPictureBox1); // 将控件添加到窗体中

        }











































        private void pictureWindow1_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            Point clientMouse = e.Location;
            //logger.Info("点击坐标:" + clientMouse);

            // 获取 PictureBox 的大小
            Size pictureBoxSize = pictureBox.ClientSize;

            //logger.Info(UtilsBLL.GetPictureBoxCurrentSize(clickedPictureBox));


            Size Lsize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
            double wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
            double hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
            //logger.Info("留白:" + Lsize);
            logger.Info("缩放比例:" + wrate);


            float x = ((float)((clientMouse.X - Lsize.Width) * wrate));
            float y = ((float)((clientMouse.Y - Lsize.Height) * hrate));


            //if (pictureBox.Image != null)
            //{
            //    Image image = pictureBox.Image;
            //    //float x = e.X * image.Width / pictureBox.Width;
            //    //float y = e.Y * image.Height / pictureBox.Height;
            //    using (Graphics graphics = Graphics.FromImage(image))
            //    {
            //        graphics.DrawEllipse(Pens.Red, x, y, 2, 2);
            //    }
            //    pictureBox.Image = image;
            //}








            //    logger.Info("点了一次");

            //    PictureBox clickedPictureBox = sender as PictureBox;

            //    if (clickedPictureBox != null)
            //    {
            //        Point clientMouse = e.Location;
            //        logger.Info("点击坐标:" + clientMouse);

            //        // 获取 PictureBox 的大小
            //        Size pictureBoxSize = clickedPictureBox.ClientSize;

            //        //logger.Info(UtilsBLL.GetPictureBoxCurrentSize(clickedPictureBox));


            //        Size Lsize = UtilsBLL.GetBlackSize(clickedPictureBox, UtilsBLL.GetPictureBoxCurrentSize(clickedPictureBox));
            //        double rate = UtilsBLL.GetPictureWRate(clickedPictureBox, UtilsBLL.GetPictureBoxCurrentSize(clickedPictureBox));

            //        int x = (int)((clientMouse.X - Lsize.Width) / rate);
            //        int y = (int)((clientMouse.Y - Lsize.Height) / rate);
            //        logger.Info("换算的横坐标：" + x + "，换算的纵坐标：" + y);

            // 获取 PictureBox 显示的图片
            Image image = pictureBox.Image;

            if (image != null)
            {


                // 调用方法，处理原始图片的像素位置
                Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)pictureBox.Image,
                    imageData =>
                    {
                        // 具体的处理逻辑
                        unsafe
                        {
                            try
                            {

                                byte* imageDataPtr = OpenCVSDK.drawPoint(imageData.Scan0, imageData.Width, imageData.Height, imageData.Stride, 20
                                    , (int)x, (int)y, 255, 0, 200,2);
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


                pictureBox.Image = processedImage;

                //        }
                //    }
            }


        }
    }
}
