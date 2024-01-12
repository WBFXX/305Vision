using _305Vision.BLL;
using _305Vision.Model;
using _305Vision.SDK;
using NLog;
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

namespace _305Vision.OWindows
{
    public partial class DotROIForm : Form
    {
        Logger logger = LogManager.GetCurrentClassLogger();


        #region 公共图像参数
        private Image overImage;
        private Image resouseImage;
        /// <summary>
        /// 处理好的图片
        /// </summary>
        public Image OverImage { get => overImage; set => overImage = value; }
        /// <summary>
        /// 处理前的图片
        /// </summary>
        public Image ResouseImage { get => resouseImage; set => resouseImage = value; }
        #endregion


        public DotROIForm()
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
             this.Close();
        }

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            Point clientMouse = e.Location;
            

            // 获取 PictureBox 显示的图片
            Image image = pictureBox.Image;

            if (image != null)
            {
                Size pictureBoxSize = pictureBox.ClientSize;
                //获取留白
                Size Lsize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                //宽度缩放比率
                double wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                //高度缩放比率
                double hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));


                float x = ((float)((clientMouse.X - Lsize.Width) * wrate));
                float y = ((float)((clientMouse.Y - Lsize.Height) * hrate));

                // 调用方法，处理原始图片的像素位置
                Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)pictureBox.Image,
                    imageData =>
                    {
                        // 具体的处理逻辑
                        unsafe
                        {
                            try
                            {
                                byte* imageDataPtr = OpenCVSDK.drawPoint(imageData.Scan0, imageData.Width, imageData.Height, imageData.Stride, 10
                                    , (int)x, (int)y, 255, 0, 200);
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
            else
            {
                MessageBox.Show("当前窗口无图像");
            }

        }

       
    }
}
