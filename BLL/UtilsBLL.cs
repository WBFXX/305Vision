using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using _305Vision.DAL;

namespace _305Vision.BLL
{
    /// <summary>
    /// 公共工具类
    /// </summary>
    public class UtilsBLL
    {

        /// <summary>
        /// 创建输入框并判断 输入提示信息，窗口标题，输入栏提示词
        /// </summary>
        /// <param name="prompt">输入提示信息</param>
        /// <param name="title">窗口标题</param>
        /// <param name="defaultValue">输入栏提示词</param>
        /// <returns>用户输入的数,返回0为输入无效</returns>
        public static int InputBox(string prompt, string title = "Input", string defaultValue = "")
        {
            //把用户输入的值赋值给inputValue
            int.TryParse(UtilsDAL.InputBox(prompt, title, defaultValue), out int inputValue);
            try
            {
                // 检查规则
                if (inputValue > 9 || inputValue <= 0)
                {
                    MessageBox.Show("输入无效，请输入一个介于1和9之间的数字。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0; // 输入无效，关闭窗口
                }

                return inputValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

        }

        /// <summary>
        /// 获取图像实际宽高
        /// </summary>
        /// <param name="pictureBox">图像盒子</param>
        /// <returns>Size( width,heigh )</returns>
        public static Size GetPictureBoxCurrentSize(PictureBox p_PictureBox)
        {
            if (p_PictureBox == null || p_PictureBox.Image == null )
            {
                return Size.Empty;
            }
            else
            {
                return UtilsDAL.GetPictureCurrentSize(p_PictureBox);
            }
        }
        /// <summary>
        /// 获取图像宽度缩放比例
        /// </summary>
        /// <param name="pictureBox">图像盒子</param>
        /// <param name="currentSize">真实尺寸</param>
        /// <returns>double缩放比例</returns>
        public static double GetPictureWRate(PictureBox pictureBox, Size currentSize)
        {
            if (pictureBox == null || pictureBox.Image == null) { return 0; }
            else
            {
                return UtilsDAL.GetPictureWRate(pictureBox, currentSize);
            }

        }

        /// <summary>
        /// 获取图像高度缩放比例
        /// </summary>
        /// <param name="pictureBox">图像盒子</param>
        /// <param name="currentSize">真实尺寸</param>
        /// <returns>double缩放比例</returns>
        public static double GetPictureHRate(PictureBox pictureBox, Size currentSize)
        {
            if (pictureBox == null || pictureBox.Image == null) { return 0; }
            else
            {
                double rate = (double)pictureBox.Image.Height / (double)currentSize.Height;
                return rate;
            }
            
        }

        /// <summary>
        /// 获取缩放后图像左边距和上边距
        /// </summary>
        /// <param name="pictureBox">图像盒子</param>
        /// <param name="currentSize">真实尺寸</param>
        /// <returns>Size(左边距，上边距)</returns>
        public static Size GetBlackSize(PictureBox pictureBox, Size currentSize)
        {
            if (pictureBox == null || pictureBox.Image == null) { return Size.Empty; }
            else
            {
                return UtilsDAL.GetBlackSize(pictureBox, currentSize);
            }
            
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
            return UtilsDAL.CalculateDistance(x1,y1,x2,y2);
        }
        /// <summary>
        /// 解析数组到List
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static List<Point> ConvertArrayToPointList(int[] array)
        {
            return UtilsDAL.ConvertArrayToPointList(array); 
        }
        /// <summary>
        /// 解析点集
        /// </summary>
        /// <param name="points">传回来的点集数组</param>
        /// <param name="size">传回来的数组大小</param>
        /// <returns></returns>
        public static int[] ReadPoints(IntPtr points, int size)
        {
            try
            {
                return UtilsDAL.ReadPoints(points, size);

            }
            catch (Exception ex)
            {
                MessageBox.Show("解析点集失败：" + ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
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
            try
            {
                return UtilsDAL.GetImageBytes(imageDataPtr, width, height, channel);

            }
            catch (Exception ex)
            {
                MessageBox.Show("图像数据(imageDataPtr)拷贝失败：" + ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
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
            try
            {
                return UtilsDAL.MergeArrays(xCoords, yCoords);
            }catch (Exception ex)
            {
                MessageBox.Show("合并数组失败：" + ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
    }
}
