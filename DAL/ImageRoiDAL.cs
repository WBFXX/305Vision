using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.DAL
{
    public class ImageRoiDAL
    {
        /// <summary>
        /// 获取图像在picture中的边界
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <returns></returns>
        public static Rectangle GetImageBounds(PictureBox pictureBox)
        {
            if (pictureBox.SizeMode == PictureBoxSizeMode.Normal)
            {
                return new Rectangle(0, 0, pictureBox.Image.Width, pictureBox.Image.Height);
            }
            else if (pictureBox.SizeMode == PictureBoxSizeMode.StretchImage)
            {
                return pictureBox.ClientRectangle;
            }
            else
            {
                float imageRatio = (float)pictureBox.Image.Width / pictureBox.Image.Height;
                float controlRatio = (float)pictureBox.Width / pictureBox.Height;

                int width, height;
                if (imageRatio > controlRatio)
                {
                    width = pictureBox.Width;
                    height = (int)(pictureBox.Width / imageRatio);
                }
                else
                {
                    width = (int)(pictureBox.Height * imageRatio);
                    height = pictureBox.Height;
                }

                return new Rectangle((pictureBox.Width - width) / 2, (pictureBox.Height - height) / 2, width, height);
            }
        }
    }
}
