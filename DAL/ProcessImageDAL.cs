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


        public static Bitmap ProcessCoriImage(Bitmap originalImage, Func<BitmapData, byte[]> imageProcessingFunc,int width,int height)
        {
            //克隆图像，避免把原图像修改回原来的节点
            Bitmap bmp = originalImage;
            originalImage = (Bitmap)originalImage.Clone();

            // 获取图像信息
            int owidth = originalImage.Width;
            int oheight = originalImage.Height;

            // 锁定位图数据
            BitmapData originalImageData = originalImage.LockBits(new Rectangle(0, 0, owidth, oheight), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // 调用特定的图像处理函数
            byte[] processedData = imageProcessingFunc(originalImageData);
            // 解锁原始图像数据
            originalImage.UnlockBits(originalImageData);
            // 创建新的图像
            Bitmap processedImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData processedImageData = processedImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            // 将处理后的数据流复制到新图像
            Marshal.Copy(processedData, 0, processedImageData.Scan0, processedData.Length);



            #region DEBUG
            // 获取扫描行地址
            IntPtr scan0 = processedImageData.Scan0;

            // 计算图像数据的大小
            int imageSize = processedImageData.Stride * processedImageData.Height;

            // 创建一个托管数组来存储图像数据
            byte[] imageDataArray = new byte[imageSize];

            // 复制图像数据到托管数组
            Marshal.Copy(scan0, imageDataArray, 0, imageSize);

            // 查看托管数组的第0个字节
            byte firstByte = imageDataArray[0];

            #endregion

            processedImage.UnlockBits(processedImageData);
            

            return processedImage;
        }
    }
}
