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
using NLog;

namespace _305Vision
{
    /// <summary>
    /// 日志输出
    /// </summary>

    public partial class FormOutput : DockContent
    {
        private static FormOutput _instance;

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

        private void 删除全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// 删除全部文本
            logBox.Text = string.Empty;
        }

    }
}
