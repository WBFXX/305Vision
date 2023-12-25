using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 设置应用程序只允许运行一个实例
            System.Threading.Mutex mutex = new System.Threading.Mutex();
            bool flag;
            mutex = new System.Threading.Mutex(true, "305Vision", out flag);
            if (!flag)
            {
                MessageBox.Show("程序已启动");
                Application.Exit();
                return;
            }
            #endregion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
