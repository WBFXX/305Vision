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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace _305Vision
{
    public partial class Test2 : Form
    {
        private Point startPoint;
        private Point endPoint;
        private Rectangle selectedRectangle;
        private bool isDrawing;
        private bool isRotating;
        private Point rotateHandle;
        private float rotationAngle;

        public Test2()
        {
            
            InitializeComponent();
            //string imgePath = "e:/1.jpg";
            //Bitmap bitmap = new Bitmap(imgePath);
            //pictureBox1.Image = (Image)bitmap;
            isDrawing = false;
            isRotating = false;
            rotateHandle = Point.Empty;
            rotationAngle = 0;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.Paint += PictureBox1_Paint;
            Text = "右键定点 左键拖拽";

        }

        int ox = -1;
        int oy = -1;
        int mx = 0;
        int my = 0;
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            using (var path = new GraphicsPath())
            {
                path.AddRectangle(new Rectangle(30, 30, 230, 160));

                if (ox < 0 || oy < 0)
                {
                    g.DrawPath(Pens.Green, path);
                    return;
                }

                g.DrawLine(Pens.Red, ox - 5, oy, ox + 5, oy);
                g.DrawLine(Pens.Red, ox, oy - 5, ox, oy + 5);

                if (mx > 0 || my > 0)
                {
                    var a = Math.Atan2(my - oy, mx - ox);
                    var n1 = (float)Math.Cos(a);
                    var n2 = (float)Math.Sin(a);
                    var n3 = -(float)Math.Sin(a);
                    var n4 = (float)Math.Cos(a);
                    var n5 = (float)((ox * (1 - Math.Cos(a)) + oy * Math.Sin(a)));
                    var n6 = (float)((oy * (1 - Math.Cos(a)) - ox * Math.Sin(a)));
                    path.Transform(new Matrix(n1, n2, n3, n4, n5, n6));
                }
                g.DrawPath(Pens.Black, path);
                Invalidate();
            }
        }



        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mx = e.X;
                my = e.Y;
                Invalidate();
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ox = e.X;
                oy = e.Y;
                mx = e.X;
                my = e.Y;
                Invalidate();
            }
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


            float x = ((float)((clientMouse.X - Lsize.Width) * wrate));
            float y = ((float)((clientMouse.Y - Lsize.Height) * hrate));


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

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
