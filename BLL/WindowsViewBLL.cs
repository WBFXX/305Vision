using _305Vision.DAL;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace _305Vision.BLL
{
    public class WindowsViewBLL
    {
        public static void ShowForm( DockContent form)
        {
            WindowsViewDAL.ShowForm(form);
        }

        public static byte[] showImage(IntPtr data, int width, int height, int stride)
        {
           return WindowsViewDAL.shouImage(data,width, height, stride);
        }


    }
}
