using _305Vision.SDK;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace _305Vision.DAL
{
    public class WindowsViewDAL
    {
        public static void ShowForm(DockContent form)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            if (form.IsHidden)
            {
                form.IsHidden = false;
            }
            else
            {
                logger.Info("当前点击窗口(" + form.Text + "窗口)已存在");
            }
        }

        public static byte[] shouImage(IntPtr intPtr,  int width,  int height, int stride)
        {
            //int newWidth = (width + 3) / 4 * 4;
            //stride = newWidth * 3;
            unsafe
            {
                byte* imgDataPtr = OpenCVSDK.showImage(intPtr, width, height, stride);
                int size = (width+3)/4*4 * 3 * height;
                byte[] imgBytes = new byte[size];
                Marshal.Copy((IntPtr)imgDataPtr, imgBytes, 0, size);
                return imgBytes;
            }
        }
    }
}
