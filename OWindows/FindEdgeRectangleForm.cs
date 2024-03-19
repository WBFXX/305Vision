using _305Vision.BLL;
using _305Vision.Common;
using _305Vision.Model;
using _305Vision.SDK;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
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
        private int gradientThreshold;
        private List<Point> listPoints = new List<Point>();
        private int[] array;
        private _305Enum.EdgeDetectionType edgeDetectionType;
        BasicImageInfo basicImageInfo;
        
        
        private bool isMove = false;//记录目标是否在画框
        #endregion

        public Image OverImage { get => overImage; set => overImage = value; }
        public Image ResouseImage { get => resouseImage; set => resouseImage = value; }
        public int EdgeNum { get => edgeNum; set => edgeNum = value; }
        public int GradientThreshold { get => gradientThreshold; set => gradientThreshold = value; }
        public Point Start { get => start; set => start = value; }
        public Point End { get => end; set => end = value; }
        public double Angle { get => angle; set => angle = value; }
        //public List<Point> ListPoints { get => listPoints; set => listPoints = value; }
        public int[] Array { get => array; set => array = value; }
        public _305Enum.EdgeDetectionType EdgeDetectionType { get=> edgeDetectionType; set => edgeDetectionType = value; }
        public FindEdgeRectangleForm()
        {
            InitializeComponent();
        }
        PictureBox pictureBox { get; set; }
        //这里添加了array
        public void InitializeParameters(int edgeNum, Point start, Point end, int[] array , int gradientThreshold , _305Enum.EdgeDetectionType edgeDetectionType)
        {
            EdgeNum = edgeNum;
            Start = start;
            End = end;
            Array = array;
            GradientThreshold = gradientThreshold;
            EdgeDetectionType = edgeDetectionType;
        }

        private void RetangelROI_Load(object sender, EventArgs e)
        {
            pictureBox = pictureWindow1.PictureBox;
            pictureWindow1.Image = (Bitmap)ResouseImage;
            pictureBox.MouseDown += pictureBox1_MouseDown;
            pictureBox.MouseMove += pictureBox1_MouseMove;
            pictureBox.MouseUp += pictureBox1_MouseUp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                OverImage = pictureBox.Image;
                logger.Info("ROI点图像处理完成。");
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //当没有按下Ctrl键时 触发 画方形操作
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                PictureBox pictureBox = sender as PictureBox;

                Point clientMouse = e.Location;
                resouseImage = this.pictureBox.Image;
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
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove && (Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                PictureBox pictureBox = sender as PictureBox;

                if (pictureBox != null && pictureBox.Image != null)
                {
                    // 获取PictureBox图像的边界
                    Rectangle imageBounds = ImageRoiBLL.GetImageBounds(pictureBox);

                    // 检查鼠标是否在PictureBox图像的范围内
                    if (imageBounds.Contains(e.Location))
                    {
                        // 鼠标在图像范围内，执行算法
                        Size lSize = UtilsBLL.GetBlackSize(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                        double wrate = UtilsBLL.GetPictureWRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                        double hrate = UtilsBLL.GetPictureHRate(pictureBox, UtilsBLL.GetPictureBoxCurrentSize(pictureBox));
                        double x = ((double)((e.Location.X - lSize.Width) * wrate));
                        double y = ((double)((e.Location.Y - lSize.Height) * hrate));
                        End = new Point((int)x, (int)y);
                        Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)resouseImage, imageData => ProcessImageData(imageData));
                        pictureBox.Image = processedImage;
                    }
                    
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control) isMove = false;
        }

        private byte[] ProcessImageData(BitmapData imageData)
        {
            try
            {
                basicImageInfo = BasicImageInfo.GetImgInfo(imageData);
                unsafe
                {
                    IntPtr Points = IntPtr.Zero;
                    int sizee = 0;
                    byte* imageDataPtr = OpenCVSDK.findEdgeRectangle(basicImageInfo.ImagePtr, (int)basicImageInfo.Width,
                        (int)basicImageInfo.Height, (int)basicImageInfo.Stride, Start.X, Start.Y, End.X, End.Y, Angle, EdgeDetectionType ,EdgeNum, GradientThreshold, ref Points, ref sizee);
                    int size = (int)(basicImageInfo.Width * basicImageInfo.Height * 3);
                    
                    
                    #region 读取点集
                    byte* arrayPtr = (byte*)Points;//读取点集
                    array = new int[sizee];//读取点集
                    Marshal.Copy((IntPtr)arrayPtr, array, 0, sizee);//复制点集数组
                    this.Array = array;
                    //listPoints = UtilsBLL.ConvertArrayToPointList(array);
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
