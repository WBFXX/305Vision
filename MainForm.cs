using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using _305Vision.Enum;

namespace _305Vision
{
    public partial class MainForm : Form
    {

        //加载侧边工具栏
        ToolsBox toolBox = new ToolsBox();
        //加载主平台
        FormPlatform platform = new FormPlatform();
        FormPlatform platform2 = new FormPlatform();
        FormOutput FormOut = FormOutput.Instance;
        FormProcess FormProcess = new FormProcess();

        

        public MainForm()
        {
            InitializeComponent();
        }

        
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            
            // 调整左侧停靠区域的宽度比例
            dockPanel1.DockLeftPortion = 0.1;  // 例如，将宽度设置为整个 DockPanel 宽度的 20%
            platform.Show(dockPanel1);//没第二个参数 默认为主窗体 中间
            //加载流程框架,在platform的左边 占比30%
            FormProcess.Show(platform.Pane, DockAlignment.Left, 0.5);
            //加载输出栏,在platform的下方 占比30%
            FormOut.Show(platform.Pane, DockAlignment.Bottom, 0.3);
            //加载侧边栏，并设置侧边栏的宽度
            toolBox.Show(dockPanel1,DockState.DockLeft);
            toolBox.DockPanel.DockLeftPortion = 0.1;
            //创建主窗口2
            platform2.Text =  "窗口2";
            platform2.Show(dockPanel1);
            
        }



















        /// <summary>
        /// 打开工具栏（如果工具栏不存在）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 工具栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolBox.DockState == DockState.Hidden || toolBox.DockState == DockState.Unknown) { 
            ToolsBox toolBox = new ToolsBox();
            toolBox.Show(dockPanel1, DockState.DockLeft);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("工具栏已打开");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            STNodeEdit sTNodeEdit = new STNodeEdit();
            sTNodeEdit.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormPlatform platform = new FormPlatform();
            platform.Show();
            //TreeView treeView = new TreeView();
            //treeView.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (toolBox.DockState == DockState.Hidden || toolBox.DockState == DockState.Unknown)
            {
                ToolsBox toolBox = new ToolsBox();
                toolBox.Show(dockPanel1, DockState.DockLeft);
            }
            else
            {
                //输出日志
                FormOut.ReadLog("工具栏已打开。",this.Name );
                //System.Windows.Forms.MessageBox.Show("工具栏已打开");
            }
            FormOut.ReadLog("这是"+ this.Name + "窗口的按钮",this.Name ,LogLevel.Warning);

        }


        //#region 防止闪屏
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}
        //#endregion










    }

}
