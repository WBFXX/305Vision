using _305Vision.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.BLL
{
    public class ImageRoiBLL
    {
        /// <summary>
        /// 获取图像在picture中的边界
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <returns></returns>
        public static Rectangle GetImageBounds(PictureBox pictureBox)
        {
            return ImageRoiDAL.GetImageBounds(pictureBox);
        }

    }
}
