using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using _305Vision.Enum;
using NLog;

namespace _305Vision
{
    /// <summary>
    /// 日志输出
    /// </summary>

    public partial class FormOutput : DockContent
    {
        private static FormOutput _instance;

        //private static FormOutput instance;  // 用于共享的单例实例
        //private static readonly object lockObject = new object();  // 用于确保线程安全
        private static Logger logger = null;


        private FormOutput()
        {
            
            InitializeComponent();
            //ReadLog("窗口初始化成功");
        }

        public static FormOutput Instance
        {

            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new FormOutput();
                return _instance;
            }
        }

        //public static void ShowInstance()
        //{
        //    Instance.Show();
        //    Instance.Focus();
        //}

        /// <summary>
        /// Info 普通信息,Warning 警告,Error 错误,如果不提供，它将默认使用 LogLevel.Info。
        /// </summary>
        /// <param name="log"></param>
        /// <param name="formName"></param>
        /// <param name="level"></param>
        /// 
        //ReadLog 函数已经定义了一个可选参数 LogClass level = LogClass.Info，这使得调用者可以选择提供或者不提供第二个参数。如果不提供，它将默认使用 LogClass.Info。
        public void ReadLog(string log,String formName , LogClass level = LogClass.Info)
        {
            string time = Convert.ToString(DateTime.Now);
            if (!this.Visible)
            {
                this.Show();
            }
            
            switch (level)
            {
                case LogClass.Info:
                    logBox.SelectionColor = System.Drawing.Color.White; // 默认黑色
                    break;
                case LogClass.Warning:
                    logBox.SelectionColor = System.Drawing.Color.Orange; // 警告颜色
                    break;
                case LogClass.Error:
                    logBox.SelectionColor = System.Drawing.Color.Red; // 错误颜色
                    break;
                default:
                    logBox.SelectionColor = System.Drawing.Color.White;
                    break;
            }

            logBox.AppendText(time + "  " + "<" + formName +"> " +level + ":" + log + "\n");

            // 恢复默认颜色
            logBox.SelectionColor = logBox.ForeColor;
        }


        private void 删除全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// 删除全部文本
            logBox.Text = string.Empty;
        }

    }
}
