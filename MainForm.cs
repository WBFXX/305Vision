using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using NLog;
using _305Vision.BLL;
using _305Vision.DAL;
using _305Vision.Utils;

namespace _305Vision
{
    public partial class MainForm : Form
    {
        #region 防止闪屏
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        #endregion
        /// <summary>
        /// 引入日志 logger
        /// </summary>
        private static  Logger logger = null;
        private static MainForm _instance;//单例程

        //加载实例
        ToolsBox toolBox = ToolsBox.Instance;
        FormPlatform platform = FormPlatform.Instance;
        FormOutput FormOut = FormOutput.Instance;
        FormProcess formProcess = FormProcess.Instance;
        PropertyGrid propertyGrid = PropertyGrid.Instance;



        public MainForm()
        {
            InitializeComponent();
            menuStrip1.Renderer = new BlackRender(); //引用Utils自创继承颜色
            //menuStrip1.Renderer = new ToolStripRendererEx(); //引用Utils自创继承颜色
            ////防闪屏,设置控件风格
            //SetStyle(
            //    ControlStyles.AllPaintingInWmPaint |  //全部在窗口绘制消息中绘图
            //    ControlStyles.OptimizedDoubleBuffer, //使用双缓冲
            //    true);
            //this.TransparencyKey = System.Drawing.Color.LightGray;
        }

        public static MainForm Instance
        {
            get 
            {
                if(_instance == null || _instance.IsDisposed)
                    _instance = new MainForm();
                else
                {
                    MessageBox.Show("软件已启动!");
                }
                return _instance;
            }
        }
        
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            

            // 调整左侧停靠区域的宽度比例
            dockPanel1.DockLeftPortion = 0.15;  // 例如，将宽度设置为整个 DockPanel 宽度的 20%
            // 调整左侧停靠区域的宽度比例
            dockPanel1.DockRightPortion = 0.15;  // 例如，将宽度设置为整个 DockPanel 宽度的 20%
            platform.Show(dockPanel1);//没第二个参数 默认为主窗体 中间
            //加载流程框架,在platform的左边 占比30%
            formProcess.Show(platform.Pane, DockAlignment.Left, 0.5);
            //加载输出栏,在platform的下方 占比30%
            FormOut.Show(platform.Pane, DockAlignment.Bottom, 0.3);
            //加载侧边栏，并设置侧边栏的宽度
            toolBox.Show(dockPanel1, DockState.DockLeft);
            toolBox.DockPanel.DockLeftPortion = 0.15;
            //加载属性栏,并设置侧边栏宽度
            propertyGrid.Show(dockPanel1,DockState.DockRight);
            propertyGrid.DockPanel.DockLeftPortion = 0.15;



            //在FormOut后开启logger
            if (logger == null)
            {
                logger = LogManager.GetCurrentClassLogger();
            }

        }



        /// <summary>
        /// 根据数量改变窗口数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void NewCountPlatform_Click(object sender, EventArgs e)
        {

            int userInput = UtilsBLL.InputBox("请输入图像块的数量：", "输入框");
            // 处理用户输入
            if (userInput > 0)
            {
                // 创建新的 FormPlatform 实例
                FormPlatform newInstance = new FormPlatform(userInput);
                // 显示新窗口
                newInstance.Show(FormOut.Pane, DockAlignment.Top, 0.7);
                // 关闭旧窗口
                FormPlatform.Instance.Close();
                // 更新 FormPlatform.Instance 单例的引用
                FormPlatform.SetPlatformInstance(newInstance);
                logger.Info("创建主窗口成功，当前窗口数量：" + FormPlatform.Instance.PictureBoxes.Count + ";");
            }

        }

        /// <summary>
        /// 打开工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolForm_Click(object sender, EventArgs e)
        {
            WindowsViewBLL.ShowForm(toolBox);
            //FormOut.ReadLog("这是"+ this.Name + "窗口的按钮",this.Name ,LogClass.Warning);

        }
        /// <summary>
        /// 打开画布窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessForm_Click(object sender, EventArgs e)
        {
            WindowsViewBLL.ShowForm(formProcess);
        }
        /// <summary>
        /// 输出窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutForm_Click(object sender, EventArgs e)
        {
            WindowsViewBLL.ShowForm(FormOut);
            // if (FormOut.IsHidden)
            //{
            //    FormOut.IsHidden = false;
            //    //FormOutput FormOut = FormOutput.Instance;
            //    //if (!platform.IsHidden)
            //    //{
            //    //    FormOut.Show(platform.Pane, DockAlignment.Bottom, 0.3);
            //    //}
            //    //else
            //    //{
            //    //    FormOut.Show(dockPanel1,DockState.DockBottom);
            //    //}
            //    //logger.Info("打开窗口成功");
            //}
            //else
            //{
            //    logger.Info("当前点击窗口(" + FormOut.Text + "窗口)已存在");

            //}
        }

        /// <summary>
        /// 图像展示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlatForm_Click(object sender, EventArgs e)
        {
            WindowsViewBLL.ShowForm(FormPlatform.Instance);
        }

        /// <summary>
        /// 属性框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyGrid_Click(object sender, EventArgs e)
        {
            WindowsViewBLL.ShowForm(PropertyGrid.Instance);
        }

        /// <summary>
        /// 测试按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Test_Click(object sender, EventArgs e)
        {
            TestForm treeView = new TestForm();
            treeView.Show();
        }

        
    }

}
