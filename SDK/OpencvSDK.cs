using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace _305Vision.SDK
{
    class OpenCVSDK
    {   /// <summary>
        /// 灰度处理
        /// </summary>
        /// <param name="data">图像指针</param>
        /// <param name="width">图像宽</param>
        /// <param name="height">图像高</param>
        /// <param name="stride">图像步长</param>
        /// <returns>图像指针</returns>
        [DllImport("demo.dll", EntryPoint = "grayScale", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* grayScale(IntPtr data, int width, int height, int stride);
        /// <summary>
        /// 释放指针资源
        /// </summary>
        /// <param name="intPtr">指针</param>
        /// <returns>IntPtr</returns>
        [DllImport("demo.dll", EntryPoint = "releaseBuffer", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* releaseBuffer(IntPtr intPtr);
        /// <summary>
        /// 二值化
        /// </summary>
        /// <param name="intPtr">图像指针</param>
        /// <param name="width">图像宽</param>
        /// <param name="height">图像高</param>
        /// <param name="stride">图像步长</param>
        /// <param name="thresh">阈值</param>
        /// <param name="max">大于阈值设置值</param>
        /// <param name="min">小于阈值设置值</param>
        /// <returns>图像指针</returns>
        [DllImport("demo.dll", EntryPoint = "binaryzation", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* binaryzation(IntPtr intPtr, int width, int height, int stride, int thresh, int max, int min);
        /// <summary>
        /// 图像腐蚀
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="stride"></param>
        /// <param name="kerSizeX"></param>
        /// <param name="kerSizeY"></param>
        /// <param name="kerStr"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        [DllImport("demo.dll", EntryPoint = "eroding", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* eroding(IntPtr intPtr, int width, int height, int stride, int kerSizeX, int kerSizeY, int kerStr, int pointX, int pointY, int iterations);
        /// <summary>
        /// 图像膨胀 
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="stride"></param>
        /// <param name="kerSizeX"></param>
        /// <param name="kerSizeY"></param>
        /// <param name="kerStr"></param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        /// <param name="iterations"></param>
        /// <returns></returns>
        [DllImport("demo.dll", EntryPoint = "expansion", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* expansion(IntPtr intPtr, int width, int height, int stride, int kerSizeX, int kerSizeY, int kerStr, int pointX, int pointY, int iterations);

    }
}
