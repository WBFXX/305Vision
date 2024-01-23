using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace _305Vision.SDK
{
    class OpenCVSDK
    {
        /// <summary>
        /// 图像宽度预处理（读图片）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="stride"></param>
        /// <returns></returns>
        [DllImport("demo.dll", EntryPoint = "showImage", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* showImage(IntPtr data, int width, int height, int stride);

        /// <summary>
        /// 灰度处理
        /// </summary>
        /// <param name="data">图像指针</param>
        /// <param name="width">图像宽</param>
        /// <param name="height">图像高</param>
        /// <param name="stride">图像步长</param>
        /// <returns>图像指针</returns>
        [DllImport("demo.dll", EntryPoint = "grayScale", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* grayScale(IntPtr data, int width, int height);
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


        /// <summary>
        /// 画点
        /// </summary>
        /// <param name="intPtr">图像</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="stride">步长</param>
        /// <param name="size">线的长短</param>
        /// <param name="x">点的x坐标</param>
        /// <param name="y">点的y坐标</param>
        /// <param name="r">点的颜色r</param>
        /// <param name="g">点的颜色g</param>
        /// <param name="b">点的颜色b</param>
        /// <param name="thickness">粗细/param>
        /// <returns>图像数组</returns>
        [DllImport("demo.dll", EntryPoint = "drawPoint", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* drawPoint(IntPtr intPtr, int width, int height, int stride, int size, int x, int y, int r, int g, int b , int thickness);
        /// <summary>
        /// 绘制旋转矩形区域
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="stride"></param>
        /// <param name="startX">起点x</param>
        /// <param name="startY">起点Y</param>
        /// <param name="endX">终点x</param>
        /// <param name="endY">终点Y</param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="angle">旋转角度，不转为0</param>
        /// <returns>图像数组</returns>
        [DllImport("demo.dll", EntryPoint = "drawRotatedRect", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* drawRotatedRect(IntPtr intPtr, int width, int height, int stride, double startX,double startY,double endX,double endY,int r,int g,int b,double angle);
        /// <summary>
        /// 裁剪
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="stride"></param>
        /// <param name="startX">起点x</param>
        /// <param name="startY">起点Y</param>
        /// <param name="endX">终点x</param>
        /// <param name="endY">终点Y</param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="angle">旋转角度，不转为0</param>
        /// <returns>图像数组</returns>
        [DllImport("demo.dll", EntryPoint = "roiCropping", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* roiCropping(IntPtr intPtr, int width, int height,int stride, double startX,double startY,double endX,double endY,int r,int g,int b,double angle);
        /// <summary>
        /// 矩形找边
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="stride"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <param name="angle">角度</param>
        /// <param name="edgeNum">边的数量</param>
        /// <returns></returns>
        [DllImport("demo.dll", EntryPoint = "findEdgeRectangle", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* findEdgeRectangle(IntPtr intPtr, int width, int height, int stride, double startX,double startY,double endX,double endY,double angle,int edgeNum);
        
        [DllImport("demo.dll", EntryPoint = "abba", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern  int abba(ref int x);

        [DllImport("demo.dll", EntryPoint = "abba", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern  int abbb();




    }
}
