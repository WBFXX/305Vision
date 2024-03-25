using _305Vision.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using static _305Vision.MySTNode.Operator.SpotSearch;

namespace _305Vision.SDK
{
    class OpenCVSDK
    {
        /// <summary>
        /// 图像宽度预处理（读图片补全字节）
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
        /// 举行找边
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="stride"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <param name="angle"></param>
        /// <param name="edgeNum"></param>
        /// <param name="listPoints"></param>
        /// <returns></returns>
        [DllImport("demo.dll", EntryPoint = "findEdgeRectangle", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* findEdgeRectangle(IntPtr intPtr, int width, int height, int stride, double startX,double startY,double endX,double endY,double angle, _305Enum.EdgeDetectionType EdgeDetectionType, int edgeNum,int gradientThreshold, ref IntPtr listPoints, ref int size);

        /// <summary>
        /// 圆形找边
        /// </summary>
        /// <param name="intPtr"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="stride"></param>
        /// <param name="pointX">圆心X</param>
        /// <param name="pointY">圆心Y</param>
        /// <param name="radiusSmall">小圆半径</param>
        /// <param name="radiusBig">大圆半径</param>
        /// <param name="edgeNum">找边数量</param>
        /// <param name="gradientThreshold">梯度变化阈值</param>
        /// <returns></returns>
        [DllImport("demo.dll", EntryPoint = "findEdgeCircular", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* findEdgeCircular(IntPtr intPtr, int width, int height, int stride, int pointX, int pointY, int radiusSmall, int radiusBig, _305Enum.EdgeDetectionType EdgeDetectionType, int edgeNum, int gradientThreshold ,ref IntPtr listPoints,ref int size);
        /// <summary>
        /// 圆形拟合
        /// </summary>
        /// <param name="Array">数组</param>
        /// <param name="num">数组长度</param>
        /// <param name="discard">抛弃点数量</param>
        /// <param name="jieradis">结果半径</param>
        /// <param name="centerX">结果圆心X</param>
        /// <param name="centerY">结果圆心Y</param>
        /// <returns></returns>
        [DllImport("demo.dll", EntryPoint = "circleFitting", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe void circleFitting(int[] Array, int num, int discard, ref double jieradis,ref double centerX,ref double centerY , ref IntPtr savePoints , ref int saveSize, ref IntPtr paoPoints, ref int paoSize);
        /// <summary>
        /// 直线拟合
        /// </summary>
        /// <param name="Array"></param>
        /// <param name="num"></param>
        /// <param name="discard">抛弃点</param>
        /// <param name="xielvK">斜率</param>
        /// <param name="pointX"></param>
        /// <param name="pointY"></param>
        [DllImport("demo.dll", EntryPoint = "lineFitting", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe void lineFitting(int[] Array, int num, int discard, ref double xielvK,ref double pointX,ref double pointY, ref IntPtr savePoints, ref int saveSize, ref IntPtr paoPoints, ref int paoSize);

        /// <summary>
        /// 斑点查找  要求输入灰度图像
        /// </summary>
        /// <param name="data">图像数据指针</param>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <param name="stride">图像步长</param>
        /// <param name="backgroundFlag">背景：黑底白点为1，白底黑点为0</param>
        /// <param name="areaMax">面积最大约束，无为-1</param>
        /// <param name="areaMin">面积最小约束，无为-1</param>
        /// <param name="widthMax">宽度最大约束，无为-1</param>
        /// <param name="widthMin">宽度最小约束，无为-1</param>
        /// <param name="heightMax">长度最大约束，无为-1</param>
        /// <param name="heightMin">长度最小约束，无为-1</param>
        /// <param name="arrayX">存储斑点X坐标数组</param>
        /// <param name="arrayY">存储斑点Y坐标数组</param>
        /// <param name="arrayW">存储斑点宽度数组</param>
        /// <param name="arrayH">存储斑点高度数组</param>
        /// <param name="arrayA">存储斑点面积数组</param>
        /// <param name="size">存储斑点数组长度</param>
        /// <returns>unsigned char* imageBuffer	//图像数组</returns>
        [DllImport("demo.dll", EntryPoint = "spotSearch", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* spotSearch(
            IntPtr data, int width, int height, int stride, _305Enum.BlackGround backgroundFlag, 
            int areaMax, int areaMin, int widthMax, int widthMin, int heightMax, 
            int heightMin, ref IntPtr arrayX, ref IntPtr arrayY, ref IntPtr arrayW, ref IntPtr arrayH,
            ref IntPtr arrayA, ref int size);
        /// <summary>
        /// 打开并初始化相机
        /// </summary>
        /// <param name="ip1">地址1</param>
        /// <param name="ip2">地址2</param>
        /// <param name="ip3">地址3</param>
        /// <param name="ip4">地址4</param>
        /// <param name="outExposureTime">相机曝光时间</param>
        /// <param name="outGain">相机增益参数</param>
        /// <returns>0-->打开相机失败
        /// 1-->设置相机曝光时间参数失败
        /// 2-->设置相机增益参数失败
        /// 3-->开始采集失败
        /// 4-->成功</returns>
        [DllImport("demo.dll", EntryPoint = "openAndInitCamera", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern int openAndInitCamera(int ip1, int ip2, int ip3, int ip4, float outExposureTime, float outGain);
        /// <summary>
        /// 采集图像数据
        /// </summary>
        /// <param name="width">输出图像宽度</param>
        /// <param name="height">输出图像高度</param>
        /// <param name="stride">输出图像步长</param>
        /// <returns>unsigned char* imageBuffer	//图像数组</returns>
        [DllImport("demo.dll", EntryPoint = "readCameraImage", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* readCameraImage(ref int width, ref int height, ref int stride);
        /// <summary>
        /// 设置相机参数
        /// </summary>
        /// <param name="outExposureTime">相机曝光时间参数</param>
        /// <param name="outGain">相机增益参数</param>
        /// <returns></returns>
        [DllImport("demo.dll", EntryPoint = "setCameraParameter", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern int setCameraParameter( ref float outExposureTime, ref float outGain);
        /// <summary>
        /// 停止抓取并关闭相机
        /// </summary>
        /// <returns>0-->停止抓取失败
        /// 1-->关闭相机失败
        /// 2-->成功</returns>
        [DllImport("demo.dll", EntryPoint = "stopAndcloseCamera", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern int stopAndcloseCamera();





    }
}
