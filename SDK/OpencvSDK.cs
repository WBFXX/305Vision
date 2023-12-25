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
        [DllImport("demo.dll", EntryPoint = "grayScale", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* grayScale(IntPtr data, int width, int height, int stride);
        [DllImport("demo.dll", EntryPoint = "releaseBuffer", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* releaseBuffer(IntPtr intPtr);

        [DllImport("demo.dll", EntryPoint = "binaryzation", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* binaryzation(IntPtr intPtr, int width, int height, int stride, int thresh, int max, int min);

        [DllImport("demo.dll", EntryPoint = "eroding", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* eroding(IntPtr intPtr, int width, int height, int stride, int kerSize, int kerStr, int pointX, int pointY, int iterations);

        [DllImport("demo.dll", EntryPoint = "expansion", CallingConvention = CallingConvention.Cdecl/*, CallingConvention = CallingConvention.Cdecl*/)]
        public static extern unsafe byte* expansion(IntPtr intPtr, int width, int height, int stride, int kerSize, int kerStr, int pointX, int pointY, int iterations);





        //[DllImport("demo.dll")]
        //public extern static void toCV();
    }
}
