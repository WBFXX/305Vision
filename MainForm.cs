using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using _305Vision.Utils;
using NLog;


namespace _305Vision
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 引入日志 logger
        /// </summary>
        private static  Logger logger = null;

        //单例
        private static MainForm _instance;

        //加载侧边工具栏
        ToolsBox toolBox = ToolsBox.Instance;
        //加载主平台
        FormPlatform platform = FormPlatform.Instance;
        FormOutput FormOut = FormOutput.Instance;
        FormProcess formProcess = FormProcess.Instance;

        

        public MainForm()
        {
            InitializeComponent();
        }

        public static MainForm Instance
        {
            get 
            {
                if(_instance == null || _instance.IsDisposed)
                    _instance = new MainForm();
                return _instance; 
            }
        }
        
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            

            // 调整左侧停靠区域的宽度比例
            dockPanel1.DockLeftPortion = 0.15;  // 例如，将宽度设置为整个 DockPanel 宽度的 20%
            

            platform.Show(dockPanel1);//没第二个参数 默认为主窗体 中间
            //加载流程框架,在platform的左边 占比30%
            formProcess.Show(platform.Pane, DockAlignment.Left, 0.5);
            //加载输出栏,在platform的下方 占比30%
            FormOut.Show(platform.Pane, DockAlignment.Bottom, 0.3);

            //加载侧边栏，并设置侧边栏的宽度
            toolBox.Show(dockPanel1, DockState.DockLeft);
            toolBox.DockPanel.DockLeftPortion = 0.15;




            ////创建主窗口2
            //platform2.Text =  "窗口2";
            //platform2.Show(dockPanel1);

            //在FormOut后开启logger
            if (logger == null)
            {
                logger = LogManager.GetCurrentClassLogger();
            }

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
                //System.Windows.Forms.MessageBox.Show("工具栏已打开");
                logger.Warn("工具栏已打开");
            }
        }

        /// <summary>
        /// 根据数量改变窗口数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, EventArgs e)
        {

            string userInput = InputBox.Show("请输入图像块的数量：", "输入框");

            // 处理用户输入
            if (!string.IsNullOrEmpty(userInput))
            {
                // 将用户输入的值转换为需要的类型
                // 这里可以添加逻辑根据需要处理用户输入的值
                int inputValue;
                if (int.TryParse(userInput, out inputValue))
                {
                    // 检查规则
                    if (inputValue > 49 || inputValue <= 0)
                    {
                        MessageBox.Show("输入无效，请输入一个介于1和49之间的数字。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // 输入无效，关闭窗口
                    }

                    // 创建新的 FormPlatform 实例
                    FormPlatform newInstance = new FormPlatform(inputValue);

                    // 显示新窗口
                    newInstance.Show(FormOut.Pane, DockAlignment.Top, 0.7);

                    // 关闭旧窗口
                    FormPlatform.Instance.Close();
                    //FormPlatform.Instance.Dispose();

                    // 更新 FormPlatform.Instance 单例的引用
                    FormPlatform.SetPlatformInstance(newInstance);

                    logger.Info("当前窗口数量：" + FormPlatform.Instance.PictureBoxes.Count + ";" + "隐藏状态："+FormPlatform.Instance.IsHidden);
                }
                else
                {
                    MessageBox.Show("无效的输入，请输入一个整数。", "警告",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        



        private void button2_Click(object sender, EventArgs e)
        {
            //FormPlatform platform = new FormPlatform();
            //platform.Show();
            //TreeView treeView = new TreeView();
            //treeView.Show();

            //FrmEnumSelect frmEnumSelect = new FrmEnumSelect(a,new System.Drawing.Point(Left),100,1);

            FormPlatform.Instance.Close();
            FormPlatform formPlatform =  new FormPlatform(9);
            //logger.Info(FormPlatform.PictureBoxes.Count);
            formPlatform.Show(FormOut.Pane,DockAlignment.Top,0.7);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (toolBox.IsHidden)
            {
                ToolsBox toolBox = new ToolsBox();
                toolBox.Show(dockPanel1, DockState.DockLeft);
                this.toolBox = toolBox;
                //FormOut.ReadLog("打开工具栏", this.Name);
            }
            else
            {
                logger.Info("打开工具栏失败，原因是已经有工具栏。" );
                
                //输出日志
                //FormOut.ReadLog("工具栏已打开",this.Name,LogClass.Warning);
                //System.Windows.Forms.MessageBox.Show("工具栏已打开");
            }
            //FormOut.ReadLog("这是"+ this.Name + "窗口的按钮",this.Name ,LogClass.Warning);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (formProcess.IsHidden)
            {
                FormProcess formProcess = new FormProcess();
                //如果 主窗口在 且 输出窗口在
                if (!platform.IsHidden && !FormOut.IsHidden)
                {
                    FormOut.IsHidden = true;
                    formProcess.Show(platform.Pane, DockAlignment.Left, 0.5);
                    FormOut.Show(platform.Pane, DockAlignment.Bottom, 0.3);
                }
                //如果 主窗口在 且 输出窗口不在
                else if (!platform.IsHidden && FormOut.IsHidden)
                {
                    formProcess.Show(platform.Pane, DockAlignment.Left, 0.5);
                }
                else formProcess.Show(dockPanel1);
                this.formProcess = formProcess;
                logger.Info("打开窗口成功");
            }
            else
            {
                logger.Info("当前点击窗口" + formProcess.Name + "已存在");

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
             if (FormOut.IsHidden)
            {
                FormOutput FormOut = FormOutput.Instance;
                if (!platform.IsHidden)
                {
                    FormOut.Show(platform.Pane, DockAlignment.Bottom, 0.3);
                }
                else
                {
                    FormOut.Show(dockPanel1,DockState.DockBottom);
                }
                logger.Info("打开窗口成功");
            }
            else
            {
                logger.Info("当前点击窗口" + FormOut.Name + "已存在");

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormPlatform formPlatform = FormPlatform.Instance;
            try
            {
                logger.Info(platform.IsHidden);
                if (platform.IsHidden)
                {
                    formPlatform.Text = "图像窗口";
                    formPlatform.Show(dockPanel1);
                }
                else
                {

                    logger.Warn("窗口已存在,窗口名称为：" + formPlatform.Text +"  隐藏代替关闭："+ formPlatform.HideOnClose);
                }
                
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            
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
