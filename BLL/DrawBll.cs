using _305Vision.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.BLL
{
    public class DrawBll
    {
        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="image"></param>
        /// <param name="centerX">圆心X</param>
        /// <param name="centerY">圆心Y</param>
        /// <param name="radius">半径</param>
        /// <returns>新图像</returns>
        public static Bitmap DrawLineOnImage(Bitmap image, double pointX, double pointY, double XielvK)
        {
            return DrawDAL.DrawLineOnImage(image,pointX,pointY,XielvK);
        }
        /// <summary>
        /// 在图上画直线
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="XielvK">斜率</param>
        /// <returns>新的图像</returns>
        public static Bitmap DrawCircleOnImage(Bitmap image, double centerX, double centerY, double radius)
        {
            return DrawDAL.DrawCircleOnImage(image,centerX,centerY,radius);
        }
    }
}
