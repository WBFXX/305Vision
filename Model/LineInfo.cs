using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.Model
{
    struct LineInfo
    {
        public Point PointOnLine { get; set; } // 直线上的一点
        public double Slope { get; set; } // 斜率

        // 构造函数
        public LineInfo(Point point, double slope)
        {
            PointOnLine = point;
            Slope = slope;
        }

        /// <summary>
        /// 方法用于计算直线的Y截距
        /// </summary>
        /// <returns>double</returns>
        public double YIntercept()
        {
            return PointOnLine.Y - Slope * PointOnLine.X;
        }

        /// <summary>
        /// 方法用于判断某个点是否在直线上
        /// </summary>
        /// <param name="point"></param>
        /// <returns>bool</returns>
        public bool IsPointOnLine(Point point)
        {
            return Math.Abs(point.Y - (Slope * point.X + YIntercept())) < 0.0001; // 使用一个小误差范围来比较浮点数
        }
    }
}
