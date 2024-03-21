using _305Vision.DAL;
using _305Vision.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        /// <returns>新图像</returns>
        public static Bitmap DrawLineOnImage(Bitmap image, LineInfo lineInfo, int lineLength)
        {
            return DrawDAL.DrawLineOnImage(image,lineInfo, lineLength);
        }
        /// <summary>
        /// 在图上画直线
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="XielvK">斜率</param>
        /// <returns>新的图像</returns>
        public static Bitmap DrawCircleOnImage(Bitmap image, CircleInfo circleInfo)
        {
            try
            {
                return DrawDAL.DrawCircleOnImage(image, circleInfo);

            }catch (Exception e)
            {

                return null;
            }
        }
    }
}
