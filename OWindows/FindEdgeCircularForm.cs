using _305Vision.BLL;
using _305Vision.Model;
using _305Vision.SDK;
using NLog;
using ST.Library.UI.NodeEditor;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace _305Vision.OWindows
{
    public partial class FindEdgeCircularForm : Form
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        #region 公共图像参数
        private Image overImage;
        private Image resouseImage;
        BasicImageInfo basicImageInfo;

        private bool isMove = false;//记录目标是否在画框
        

        public Image OverImage { get => overImage; set => overImage = value; }
        public Image ResouseImage { get => resouseImage; set => resouseImage = value; }

        public int pointX { get; set; }
        public int pointY { get; set; }
        public int radiusSmall { get; set; }
        public int radiusBig { get; set; }
        public int EdgeNum { get; set; }
        public int gradientThreshold { get; set; }
        public Point End { get; set; }
        #endregion
        public FindEdgeCircularForm()
        {
            InitializeComponent();
        }

        public void InitializeParameters(int pointX, int pointY, int radiusSmall, int radiusBig, int edgeNum, int gradientThreshold)
        {
            this.pointX = pointX;
            this.pointY = pointY;
            this.radiusSmall = radiusSmall;
            this.radiusBig = radiusBig;
            this.EdgeNum = edgeNum;
            this.gradientThreshold = gradientThreshold;
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
                this.pointX = (int)startX;
                this.pointY = (int)startY;
                isMove = true;
                logger.Info("圆心坐标为：(" + pointX + "," + pointY + ")");
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
                    this.radiusBig = (int)UtilsBLL.CalculateDistance(pointX, pointY, End.X, End.Y);//求两点间距离
                    this.radiusSmall = radiusBig / 2;//小圆半径为大圆半径的一半


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
                logger.Info("小圆半径：" + Math.Abs(End.X - pointX));
                logger.Info("大圆半径：" + Math.Abs(End.Y - pointY));

                unsafe
                {
                    byte* imageDataPtr = OpenCVSDK.findEdgeCircular(basicImageInfo.ImagePtr, (int)basicImageInfo.Width,
                        (int)basicImageInfo.Height, (int)basicImageInfo.Stride, pointX,pointY,radiusSmall,radiusBig,EdgeNum,gradientThreshold);

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
