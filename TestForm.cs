using _305Vision.BLL;
using _305Vision.SDK;
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



        private void TestForm_Load(object sender, EventArgs e)
        {
            string imgePath = "e:/1.jpg";
            Bitmap bitmap = new Bitmap(imgePath);
            pictureBox1.Image = bitmap;
            pictureBox1.MouseClick += pictureWindow1_MouseClick;

        }

        

        private void pictureWindow1_MouseClick(object sender, MouseEventArgs e)
        {
            logger.Info("点了一次");

            PictureBox clickedPictureBox = sender as PictureBox;

            if (clickedPictureBox != null)
            {
                Point clientMouse = e.Location;
                logger.Info(clientMouse);

                // 获取 PictureBox 的大小
                Size pictureBoxSize = clickedPictureBox.ClientSize;

                // 获取 PictureBox 显示的图片
                Image image = clickedPictureBox.Image;

                if (image != null)
                {
                    

                    // 调用方法，处理原始图片的像素位置
                    Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)clickedPictureBox.Image,
                        imageData =>
                        {
                            // 具体的处理逻辑
                            unsafe
                            {
                                try
                                {
                                    byte* imageDataPtr = OpenCVSDK.drawPoint(imageData.Scan0, imageData.Width, imageData.Height, imageData.Stride, 10, clientMouse.X, clientMouse.Y, 255, 0, 200);
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

                    clickedPictureBox.Image = processedImage;
                }
            }
        }


    }
}
