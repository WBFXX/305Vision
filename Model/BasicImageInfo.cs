using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.Model
{
    /// <summary>
    /// 图像基础信息
    /// </summary>
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    struct BasicImageInfo
    {
        double width;
        double height;
        double stride;

        public IntPtr ImagePtr { get; set; }
        public double Width { get => width; set => width = value; }
        public double Height { get => height; set => height = value; }
        public double Stride { get => stride; set => stride = value; }

        public static BasicImageInfo GetImgInfo(BitmapData imageData)
        {
            return new BasicImageInfo()
            {
                ImagePtr = imageData.Scan0,
                Width = imageData.Width,
                Height = imageData.Height,
                Stride = imageData.Stride,
            };
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
