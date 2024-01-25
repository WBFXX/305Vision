using _305Vision.BLL;
using _305Vision.DAL;
using _305Vision.Model;
using _305Vision.SDK;
using NLog;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace _305Vision.OWindows
{
    public partial class RecROIForm : Form
    {
        #region 变量集合
        private Logger logger = LogManager.GetCurrentClassLogger();
        private Image overImage;
        private Image resouseImage;
        private Point start;
        private Point end;
        private double angle;
        private Size Lsize;
        private double wrate;
        private double hrate;
        private bool isMove = false;

        #endregion
        public Image OverImage { get => overImage; set => overImage = value; }
        public Image ResouseImage { get => resouseImage; set => resouseImage = value; }
        public Point Start { get => start; set => start = value; }
        public Point End { get => end; set => end = value; }
        public double Angle { get => angle; set => angle = value; }

        public RecROIForm()
        {
            InitializeComponent();
        }

        private void RecROIForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = ResouseImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                overImage = pictureBox1.Image;
                logger.Info("ROI点图像处理完成。");
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            Point clientMouse = e.Location;

            resouseImage = pictureBox1.Image;

            Image image = pictureBox.Image;

            if (image != null)
            {
                Lsize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                double startX = ((double)((clientMouse.X - Lsize.Width) * wrate));
                double startY = ((double)((clientMouse.Y - Lsize.Height) * hrate));
                Start = new Point((int)startX, (int)startY);
                isMove = true;
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
                    Lsize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));

                    double x = ((double)((clientMouse.X - Lsize.Width) * wrate));
                    double y = ((double)((clientMouse.Y - Lsize.Height) * hrate));
                    End = new Point((int)x, (int)y);

                    Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)resouseImage, imageData =>
                    {
                        unsafe
                        {
                            try
                            {
                                logger.Info("截取宽度：" + Math.Abs(End.X - Start.X));
                                logger.Info("截取高度：" + Math.Abs(End.Y - Start.Y));
                                logger.Info("起点坐标：" + Start + ";" + "终点坐标：" + End);
                                byte* imageDataPtr = OpenCVSDK.drawRotatedRect(imageData.Scan0, imageData.Width, imageData.Height, imageData.Stride,
                                    Start.X, Start.Y, End.X, End.Y, 255, 0, 200, 0);

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
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }

        private void btnCrop_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = (Bitmap)resouseImage;

            Bitmap processedImage = ProcessImageDAL.ProcessCoriImage((Bitmap)bitmap, imageData =>
            {
                unsafe
                {
                    try
                    {
                        byte* imageDataPtr = OpenCVSDK.roiCropping(imageData.Scan0, imageData.Width, imageData.Height, imageData.Stride,
                            Start.X, Start.Y, End.X, End.Y, 255, 0, 200, Angle);

                        int size = (Math.Abs(End.X - Start.X) + 3) / 4 * 4 * Math.Abs(End.Y - Start.Y) * 3;
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
            }, (Math.Abs(End.X - Start.X) + 3) / 4 * 4, Math.Abs(End.Y - Start.Y));

            pictureBox1.Image = (Bitmap)processedImage;
            logger.Info("裁剪成功。");

            DialogResult = DialogResult.OK;
            Close();
        }

        
    }
}
