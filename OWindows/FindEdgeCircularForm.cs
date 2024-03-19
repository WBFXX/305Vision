using _305Vision.BLL;
using _305Vision.Common;
using _305Vision.DAL;
using _305Vision.Model;
using _305Vision.SDK;
using Newtonsoft.Json.Linq;
using NLog;
using PictureWindowControl;
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

        private bool isMovee = false;//记录目标是否在画框
        private bool isFinished = false;//记录是否已经拖动完画图


        public Image OverImage { get => overImage; set => overImage = value; }
        public Image ResouseImage { get => resouseImage; set => resouseImage = value; }

        public int pointX { get; set; }
        public int pointY { get; set; }
        public int radiusSmall { get; set; }
        public int radiusBig { get; set; }
        public int EdgeNum { get; set; }
        public int gradientThreshold { get; set; }
        public Point End { get; set; }
        public _305Enum.EdgeDetectionType edgeDetectionType { get; set; }
        #region 点集添加
        private int[] array;//点集添加
        public int[] Array { get => array; set => array = value; }
        #endregion
        #endregion
        public FindEdgeCircularForm()
        {
            InitializeComponent();
        }

        public void InitializeParameters(int pointX, int pointY, int radiusSmall, int radiusBig, int edgeNum, int gradientThreshold, _305Enum.EdgeDetectionType EdgeDetectionType)
        {
            this.pointX = pointX;
            this.pointY = pointY;
            this.radiusSmall = radiusSmall;
            this.radiusBig = radiusBig;
            this.EdgeNum = edgeNum;
            this.gradientThreshold = gradientThreshold;
            this.edgeDetectionType = EdgeDetectionType;
        }

        private void RetangelROI_Load(object sender, EventArgs e)
        {
            pictureBox11.Image = (Bitmap)ResouseImage;
            pictureBox11.PictureBox.MouseDown += pictureBox11_MouseDown;
            pictureBox11.PictureBox.MouseMove += pictureBox1_MouseMove;
            pictureBox11.PictureBox.MouseUp += pictureBox1_MouseUp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox11.Image != null)
            {
                OverImage = pictureBox11.Image;
                logger.Info("ROI点图像处理完成。");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox11_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标没在按键
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {

                PictureBox pictureBox = sender as PictureBox;
                //PictureBox pictureBox = pictureWindow.PictureBox;
                Point clientMouse = e.Location;
                //resouseImage = pictureBox.Image;
                Image image = pictureBox.Image;
                if (isFinished)
                {
                    pictureBox.Image = resouseImage;
                    isFinished = false;
                }
                if (image != null)
                {
                    Size lSize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    double wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    double hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                    double startX = ((double)((clientMouse.X) * wrate));
                    double startY = ((double)((clientMouse.Y) * hrate));
                    this.pointX = (int)startX;
                    this.pointY = (int)startY;
                    isMovee = true;
                    logger.Info("圆心坐标为：(" + pointX + "," + pointY + ")");
                }
                else
                {
                    MessageBox.Show("当前窗口无图像");
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //logger.Info("Ismove是：" + isMovee);
            if (isMovee && (Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                PictureBox pictureBox = sender as PictureBox;
                //PictureBox pictureBox = pictureWindow.PictureBox;
                if (pictureBox != null)
                {
                    // 获取PictureBox图像的边界
                    Rectangle imageBounds = ImageRoiBLL.GetImageBounds(pictureBox);
                    // 检查鼠标是否在PictureBox图像的范围内
                    if (imageBounds.Contains(e.Location) )
                    {
                        Point clientMouse = e.Location;
                        Size lSize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                        double wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                        double hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                        double x = ((double)((clientMouse.X) * wrate));
                        double y = ((double)((clientMouse.Y) * hrate));
                        End = new Point((int)x, (int)y);
                        this.radiusBig = (int)UtilsBLL.CalculateDistance(pointX, pointY, End.X, End.Y);//求两点间距离
                        this.radiusSmall = radiusBig / 2;//小圆半径为大圆半径的一半
                        //防止小圆半径出镜 报错
                        if (pointX - radiusSmall > 0 && pointY - radiusSmall >0)
                        {
                            Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)resouseImage, imageData => ProcessImageData(imageData));
                            pictureBox.Image = (Bitmap)processedImage;
                        }
                        
                    }

                }
            }
        }


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMovee && (Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                isFinished = true;
                isMovee = false;
            }
        }

        private byte[] ProcessImageData(BitmapData imageData)
        {
            try
            {
                basicImageInfo = BasicImageInfo.GetImgInfo(imageData);
                //logger.Info("小圆半径：" + Math.Abs(End.X - pointX));
                //logger.Info("大圆半径：" + Math.Abs(End.Y - pointY));
                unsafe
                {
                    IntPtr Points = IntPtr.Zero;
                    int sizee = 0;
                    byte* imageDataPtr = OpenCVSDK.findEdgeCircular(basicImageInfo.ImagePtr, (int)basicImageInfo.Width,
                        (int)basicImageInfo.Height, (int)basicImageInfo.Stride, pointX, pointY, radiusSmall, radiusBig, edgeDetectionType, EdgeNum, gradientThreshold, ref Points, ref sizee);
                    #region 读取点集
                    byte* arrayPtr = (byte*)Points;//读取点集
                    array = new int[sizee];//读取点集
                    Marshal.Copy((IntPtr)arrayPtr, array, 0, sizee);//复制点集数组
                    this.Array = array;
                    #endregion

                    return UtilsBLL.GetImageBytes((IntPtr)imageDataPtr, imageData.Width, imageData.Height, 3);
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
