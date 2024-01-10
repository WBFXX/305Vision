using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    }
}
