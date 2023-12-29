using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace _305Vision.DAL
{
    public class ProcessImageDAL
    {
        /// <summary>
        /// 3通道图像数据处理。
        /// </summary>
        /// <param name="imageData">要处理的图像数据。</param>
        /// <param name="binaryzationParams">二值化参数:输入。</param>
        /// <returns>处理后的图像数据。</returns>
        public static Bitmap ProcessImage(Bitmap originalImage, Func<BitmapData, byte[]> imageProcessingFunc)
        {
                //克隆图像，避免把原图像修改回原来的节点
                Bitmap bmp = originalImage;
                originalImage = (Bitmap)originalImage.Clone();

                // 获取图像信息
                int width = originalImage.Width;
                int height = originalImage.Height;

                // 锁定位图数据
                BitmapData originalImageData = originalImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                // 调用特定的图像处理函数
                byte[] processedData = imageProcessingFunc(originalImageData);

                // 创建新的图像
                Bitmap processedImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData processedImageData = processedImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                // 将处理后的数据流复制到新图像
                Marshal.Copy(processedData, 0, processedImageData.Scan0, processedData.Length);

                processedImage.UnlockBits(processedImageData);
                // 解锁原始图像数据
                originalImage.UnlockBits(originalImageData);

                return processedImage;
        }
    }
}
