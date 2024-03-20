using _305Vision.SDK;
using NLog;
using PictureWindowControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.DAL
{
    /// <summary>
    /// 输入框，获取图像真实大小
    /// </summary>
    public class UtilsDAL
    {
        private const int FormWidth = 500;
        private const int FormHeight = 170;
        private const int LabelLeft = 50;
        private const int LabelTop = 20;
        private const int TextBoxLeft = 50;
        private const int TextBoxTop = 50;
        private const int TextBoxWidth = 400;
        private const int ButtonLeft = 350;
        private const int ButtonTop = 90;
        private const int ButtonWidth = 100;
        /// <summary>
        /// 输入框，返回用户输入的内容
        /// </summary>
        /// <param name="prompt">提示内容</param>
        /// <param name="title">窗口标题</param>
        /// <param name="defaultValue">默认内容</param>
        /// <returns>返回string类型内容</returns>
        public static string InputBox(string prompt, string title = "Input", string defaultValue = "")
        {
            Form promptForm = new Form
            {
                Width = FormWidth,
                Height = FormHeight,
                //BackColor = Color.AliceBlue,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false,
                Opacity = 0.95,
                ShowIcon = false,
                ShowInTaskbar = false,
            };

            Label textLabel = new Label { Left = LabelLeft, Top = LabelTop, Text = prompt, Width = TextBoxWidth };
            TextBox textBox = new TextBox { Left = TextBoxLeft, Top = TextBoxTop, Width = TextBoxWidth, Text = defaultValue };

            Button confirmation = new Button { Text = "确定", Left = ButtonLeft, Width = ButtonWidth, Top = ButtonTop, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => promptForm.Close();

            promptForm.Controls.Add(textBox);
            promptForm.Controls.Add(confirmation);
            promptForm.Controls.Add(textLabel);
            promptForm.AcceptButton = confirmation;

            return promptForm.ShowDialog() == DialogResult.OK ? textBox.Text : defaultValue;
        }

        /// <summary>
        /// 获取图像实际宽高
        /// </summary>
        /// <param name="pictureBox">图像盒子</param>
        /// <returns>Size( width,heigh )</returns>
        public static Size GetPictureCurrentSize(PictureBox pictureBox)
        {

            //原始宽高
            int originalWidth = pictureBox.Image.Width;
            int originalHeight = pictureBox.Image.Height;


            PropertyInfo rectangleProperty = pictureBox.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
            Rectangle rectangle = (Rectangle)rectangleProperty.GetValue(pictureBox, null);
            //实际宽高
            int currentWidth = rectangle.Width;
            int currentHeight = rectangle.Height;
            return new Size(currentWidth, currentHeight);



            ////缩放比例
            //double rate = (double)currentHeight / (double)originalHeight;

            //int black_left_width = (currentWidth == pictureBox.Width) ? 0 : (pictureBox.Width - currentWidth) / 2;
            //int black_top_height = (currentHeight == pictureBox.Height) ? 0 : (pictureBox.Height - currentHeight) / 2;
            //int zoom_x = e.X - black_left_width;
            //int zoom_y = e.Y - black_top_height;

            //double original_x = (double)zoom_x / rate;
            //double original_y = (double)zoom_y / rate;

            //StringBuilder sb = new StringBuilder();

            //sb.AppendFormat("原始尺寸{0}/{1}(宽/高)\r\n", originalWidth, originalHeight);
            //sb.AppendFormat("缩放状态图片尺寸{0}/{1}(宽/高)\r\n", currentWidth, currentHeight);
            //sb.AppendFormat("缩放比率{0}\r\n", rate);
            //sb.AppendFormat("左留白宽度{0}\r\n", black_left_width);
            //sb.AppendFormat("上留白高度{0}\r\n", black_top_height);
            //sb.AppendFormat("当前鼠标坐标{0}/{1}(X/Y)\r\n", e.X, e.Y);
            //sb.AppendFormat("缩放图中鼠标坐标{0}/{1}(X/Y)\r\n", zoom_x, zoom_y);
            //sb.AppendFormat("原始图中鼠标坐标{0}/{1}(X/Y)\r\n", original_x, original_y);
            //this.label1.Text = sb.ToString();

        }
        /// <summary>
        /// 获取图像宽度缩放比例
        /// </summary>
        /// <param name="pictureBox">图像盒子</param>
        /// <param name="currentSize">真实尺寸</param>
        /// <returns>double缩放比例</returns>
        public static double GetPictureWRate(PictureBox pictureBox, Size currentSize)
        {

            //缩放比例
            //double rate =   pictureBox.Image.Width / currentSize.Width;
            double rate = (double)pictureBox.Image.Height / (double)currentSize.Height;
            return rate;
        }
        /// <summary>
        /// 获取图像高度缩放比例
        /// </summary>
        /// <param name="pictureBox">图像盒子</param>
        /// <param name="currentSize">真实尺寸</param>
        /// <returns>double缩放比例</returns>
        public static double GetPictureHRate(PictureBox pictureBox, Size currentSize)
        {
            //缩放比例
            //double rate =   pictureBox.Image.Width / currentSize.Width;
            double rate = (double)pictureBox.Image.Height / (double)currentSize.Height;
            return rate;
        }
        /// <summary>
        /// 获取缩放后图像左边距和上边距
        /// </summary>
        /// <param name="pictureBox">图像盒子</param>
        /// <param name="currentSize">真实尺寸</param>
        /// <returns>Size(左边距，上边距)</returns>
        public static Size GetBlackSize(PictureBox pictureBox, Size currentSize)
        {
            int black_left_width = (currentSize.Width == pictureBox.Width) ? 0 : (pictureBox.Width - currentSize.Width) / 2;
            int black_top_height = (currentSize.Height == pictureBox.Height) ? 0 : (pictureBox.Height - currentSize.Height) / 2;
            return new Size(black_left_width, black_top_height);
        }
        /// <summary>
        /// 两点之间的距离
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns>距离</returns>
        public static double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        /// <summary>
        /// 解析数组到List
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<Point> ConvertArrayToPointList(int[] array)
        {
            List<Point> pointList = new List<Point>();

            // 确保数组长度为偶数，因为每个点需要两个值
            if (array.Length % 2 != 0)
            {
                throw new ArgumentException("数组长度不是偶数！");
            }

            // 将数组拆解为点，并添加到List<Point>中
            for (int i = 0; i < array.Length; i += 2)
            {
                Point point = new Point(array[i], array[i + 1]);
                pointList.Add(point);
            }

            return pointList;
        }
        /// <summary>
        /// 解析点集
        /// </summary>
        /// <param name="points">传回来的点集数组</param>
        /// <param name="size">传回来的数组大小</param>
        /// <returns></returns>
        public static int[] ReadPoints(IntPtr points, int size)
        {
            unsafe
            {
                byte* ptr = (byte*)points;
                int[] array = new int[size];
                Marshal.Copy((IntPtr)ptr, array, 0, size);
                return array;
            }
        }
        /// <summary>
        /// 获创建3通道，图像数据的拷贝
        /// </summary>
        /// <param name="imageDataPtr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="channel">通道数</param>
        /// <returns>imageBytes</returns>
        public static byte[] GetImageBytes(IntPtr imageDataPtr, int width, int height, int channel)
        {
            int size = width * height * channel;
            byte[] imageBytes = new byte[size];
            Marshal.Copy(imageDataPtr, imageBytes, 0, size);
            unsafe
            {
                OpenCVSDK.releaseBuffer(imageDataPtr);

            }
            return imageBytes;
        }
        /// <summary>
        /// 合并数组
        /// </summary>
        /// <param name="xCoords">X坐标</param>
        /// <param name="yCoords">Y坐标</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static int[] MergeArrays(int[] xCoords, int[] yCoords)
        {
            if (xCoords.Length != yCoords.Length)
            {
                throw new ArgumentException("X坐标数组和Y坐标数组长度不一致");
            }

            int[] mergedArray = new int[xCoords.Length * 2];
            int index = 0;

            for (int i = 0; i < xCoords.Length; i++)
            {
                mergedArray[index] = xCoords[i];
                mergedArray[index + 1] = yCoords[i];
                index += 2;
            }

            return mergedArray;
        }
    }

}
