using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.Model
{
    struct CircleInfo
    {
        public Point Center { get; set; } // 圆心
        public double Radius { get; set; } // 半径

        // 构造函数
        public CircleInfo(Point center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        // 方法用于计算圆的面积
        public double Area()
        {
            return Math.PI * Radius * Radius;
        }
    }
}
