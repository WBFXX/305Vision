using _305Vision.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.DAL
{
    public class DrawDAL
    {
        /// <summary>
        /// 画圆
        /// </summary>
        /// <param name="image"></param>
        /// <param name="centerX">圆心X</param>
        /// <param name="centerY">圆心Y</param>
        /// <param name="radius">半径</param>
        /// <returns>新图像</returns>
        public static Bitmap DrawCircleOnImage(Bitmap image, CircleInfo circleInfo)
        {
            // 创建一个新的图像副本，以免修改原始图像
            Bitmap newImage = new Bitmap(image);

            // 在图像上创建Graphics对象
            using (Graphics g = Graphics.FromImage(newImage))
            {
                // 创建一个画笔
                using (Pen pen = new Pen(Color.Red, 2)) // 这里使用红色笔绘制圆形，可以根据需要进行调整
                {
                    // 计算圆的边界矩形
                    int x = (int)(circleInfo.Center.X - circleInfo.Radius);
                    int y = (int)(circleInfo.Center.Y - circleInfo.Radius);
                    int diameter = (int)(2 * circleInfo.Radius);

                    // 在图像上绘制圆
                    g.DrawEllipse(pen, x, y, diameter, diameter);
                }
            }

            return newImage;
        }
        /// <summary>
        /// 在图上画直线
        /// </summary>
        /// <param name="image">图像</param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="XielvK">斜率</param>
        /// <returns>新的图像</returns>
        public static Bitmap DrawLineOnImage(Bitmap image,LineInfo lineInfo , int lineLength)
        {
            // 创建一个新的图像副本，以免修改原始图像  
            Bitmap newImage = new Bitmap(image);

            // 在图像上创建Graphics对象  
            using (Graphics g = Graphics.FromImage(newImage))
            {
                // 创建一个画笔  
                using (Pen pen = new Pen(Color.Red, 2)) // 这里使用红色笔绘制直线，可以根据需要进行调整  
                {
                    // 计算直线的终点坐标  
                    double startX = lineInfo.PointOnLine.X - lineLength/2; // 根据传入的lineLength计算直线终点的X坐标  
                    double startY = lineInfo.PointOnLine.Y - lineInfo.Slope * lineLength/2; // 根据斜率计算直线终点的Y坐标
                    double endX = lineInfo.PointOnLine.X + lineLength/2; // 根据传入的lineLength计算直线终点的X坐标  
                    double endY = lineInfo.PointOnLine.Y + lineInfo.Slope * lineLength/2; // 根据斜率计算直线终点的Y坐标  

                    // 在图像上绘制直线  
                    g.DrawLine(pen, (float)startX, (float)startY, (float)endX, (float)endY);
                }
            }

            return newImage;
        }
    }
}
