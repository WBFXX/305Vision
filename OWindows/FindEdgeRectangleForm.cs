using _305Vision.BLL;
using _305Vision.Model;
using _305Vision.SDK;
using NLog;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace _305Vision.OWindows
{
    public partial class FindEdgeRectangleForm : Form
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        #region 公共图像参数
        private Image overImage;
        private Image resouseImage;
        private Point start;
        private Point end;
        private double angle;
        private int edgeNum;
        BasicImageInfo basicImageInfo;

        private bool isMove = false;//记录目标是否在画框
        #endregion

        public Image OverImage { get => overImage; set => overImage = value; }
        public Image ResouseImage { get => resouseImage; set => resouseImage = value; }
        public int EdgeNum { get => edgeNum; set => edgeNum = value; }
        public Point Start { get => start; set => start = value; }
        public Point End { get => end; set => end = value; }
        public double Angle { get => angle; set => angle = value; }

        public FindEdgeRectangleForm()
        {
            InitializeComponent();
        }

        public void InitializeParameters(int edgeNum, Point start, Point end)
        {
            EdgeNum = edgeNum;
            Start = start;
            End = end;
        }

        private void RetangelROI_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = ResouseImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                OverImage = pictureBox1.Image;
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

            Image image = pictureBox.Image;

            if (image != null)
            {
                Size lSize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                double wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                double hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                double startX = ((double)((clientMouse.X - lSize.Width) * wrate));
                double startY = ((double)((clientMouse.Y - lSize.Height) * hrate));
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
                    Size lSize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    double wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    double hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    double x = ((double)((clientMouse.X - lSize.Width) * wrate));
                    double y = ((double)((clientMouse.Y - lSize.Height) * hrate));
                    End = new Point((int)x, (int)y);


                    Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)resouseImage, imageData => ProcessImageData(imageData));
                    pictureBox.Image = processedImage;
                }
            }
        }


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }

        private byte[] ProcessImageData(BitmapData imageData)
        {
            try
            {
                basicImageInfo = BasicImageInfo.NewMethod(imageData);
                logger.Info("截取宽度：" + Math.Abs(End.X - Start.X));
                logger.Info("截取高度" + Math.Abs(End.Y - Start.Y));
                logger.Info("起点坐标：" + Start + ";" + "终点坐标：" + End);

                unsafe
                {
                    byte* imageDataPtr = OpenCVSDK.findEdgeRectangle(basicImageInfo.ImagePtr, (int)basicImageInfo.Width,
                        (int)basicImageInfo.Height, (int)basicImageInfo.Stride, Start.X, Start.Y, End.X, End.Y, Angle, EdgeNum);

                    int size = (int)(basicImageInfo.Width * basicImageInfo.Height * 3);
                    byte[] imageByte = new byte[size];
                    Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                    OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);

                    return imageByte;
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
