using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _305Vision.DAL;

namespace _305Vision.BLL
{
    public class ProcessImageBLL
    {
        /// <summary>
        /// 3通道图像数据处理。
        /// </summary>
        /// <param name="imageData">要处理的图像数据。</param>
        /// <param name="binaryzationParams">二值化参数:输入。</param>
        /// <returns>处理后的图像数据。</returns>
        public static Bitmap ProcessImage(Bitmap originalImage, Func<BitmapData, byte[]> imageProcessingFunc)
        {
            return ProcessImageDAL.ProcessImage(originalImage, imageProcessingFunc);
        }
    }
}
