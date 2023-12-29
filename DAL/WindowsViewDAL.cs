using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
