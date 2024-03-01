namespace _305Vision
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.vS2015DarkTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSolutionExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPropertyWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemToolbox = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOutputWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTaskList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemToolBar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemLayoutByCode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLayoutByXml = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSchemaVS2015Light = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSchemaVS2015Blue = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSchemaVS2015Dark = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNewWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.toolBarButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBarButtonSolutionExplorer = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonPropertyWindow = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonToolbox = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonOutputWindow = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBarButtonLayoutByCode = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonLayoutByXml = new System.Windows.Forms.ToolStripButton();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vsToolStripExtender1 = new WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender(this.components);
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.mainMenu.SuspendLayout();
            this.toolBar.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemView,
            this.menuItemTools,
            this.menuItemWindow,
            this.menuItemHelp});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.MdiWindowListItem = this.menuItemWindow;
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1104, 28);
            this.mainMenu.TabIndex = 34;
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpen,
            this.menuItemClose,
            this.menuItem4,
            this.menuItemExit});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(53, 26);
            this.menuItemFile.Text = "&文件";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Size = new System.Drawing.Size(224, 26);
            this.menuItemOpen.Text = "&Open...";
            this.menuItemOpen.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // menuItemClose
            // 
            this.menuItemClose.Name = "menuItemClose";
            this.menuItemClose.Size = new System.Drawing.Size(224, 26);
            this.menuItemClose.Text = "&Save";
            this.menuItemClose.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Name = "menuItem4";
            this.menuItem4.Size = new System.Drawing.Size(221, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(224, 26);
            this.menuItemExit.Text = "&Exit";
            // 
            // menuItemView
            // 
            this.menuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSolutionExplorer,
            this.menuItemPropertyWindow,
            this.menuItemToolbox,
            this.menuItemOutputWindow,
            this.menuItemTaskList,
            this.menuItem1,
            this.menuItemToolBar,
            this.menuItemStatusBar,
            this.menuItem2,
            this.menuItemLayoutByCode,
            this.menuItemLayoutByXml});
            this.menuItemView.MergeIndex = 1;
            this.menuItemView.Name = "menuItemView";
            this.menuItemView.Size = new System.Drawing.Size(53, 26);
            this.menuItemView.Text = "&视图";
            // 
            // menuItemSolutionExplorer
            // 
            this.menuItemSolutionExplorer.Name = "menuItemSolutionExplorer";
            this.menuItemSolutionExplorer.Size = new System.Drawing.Size(246, 26);
            this.menuItemSolutionExplorer.Text = "&Solution Explorer";
            // 
            // menuItemPropertyWindow
            // 
            this.menuItemPropertyWindow.Name = "menuItemPropertyWindow";
            this.menuItemPropertyWindow.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.menuItemPropertyWindow.Size = new System.Drawing.Size(246, 26);
            this.menuItemPropertyWindow.Text = "&Property Window";
            // 
            // menuItemToolbox
            // 
            this.menuItemToolbox.Name = "menuItemToolbox";
            this.menuItemToolbox.Size = new System.Drawing.Size(246, 26);
            this.menuItemToolbox.Text = "&Toolbox";
            // 
            // menuItemOutputWindow
            // 
            this.menuItemOutputWindow.Name = "menuItemOutputWindow";
            this.menuItemOutputWindow.Size = new System.Drawing.Size(246, 26);
            this.menuItemOutputWindow.Text = "&Output Window";
            // 
            // menuItemTaskList
            // 
            this.menuItemTaskList.Name = "menuItemTaskList";
            this.menuItemTaskList.Size = new System.Drawing.Size(246, 26);
            this.menuItemTaskList.Text = "Task &List";
            // 
            // menuItem1
            // 
            this.menuItem1.Name = "menuItem1";
            this.menuItem1.Size = new System.Drawing.Size(243, 6);
            // 
            // menuItemToolBar
            // 
            this.menuItemToolBar.Checked = true;
            this.menuItemToolBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemToolBar.Name = "menuItemToolBar";
            this.menuItemToolBar.Size = new System.Drawing.Size(246, 26);
            this.menuItemToolBar.Text = "Tool &Bar";
            // 
            // menuItemStatusBar
            // 
            this.menuItemStatusBar.Checked = true;
            this.menuItemStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemStatusBar.Name = "menuItemStatusBar";
            this.menuItemStatusBar.Size = new System.Drawing.Size(246, 26);
            this.menuItemStatusBar.Text = "Status B&ar";
            // 
            // menuItem2
            // 
            this.menuItem2.Name = "menuItem2";
            this.menuItem2.Size = new System.Drawing.Size(243, 6);
            // 
            // menuItemLayoutByCode
            // 
            this.menuItemLayoutByCode.Name = "menuItemLayoutByCode";
            this.menuItemLayoutByCode.Size = new System.Drawing.Size(246, 26);
            this.menuItemLayoutByCode.Text = "Layout By &Code";
            // 
            // menuItemLayoutByXml
            // 
            this.menuItemLayoutByXml.Name = "menuItemLayoutByXml";
            this.menuItemLayoutByXml.Size = new System.Drawing.Size(246, 26);
            this.menuItemLayoutByXml.Text = "Layout By &XML";
            // 
            // menuItemTools
            // 
            this.menuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSchemaVS2015Light,
            this.menuItemSchemaVS2015Blue,
            this.menuItemSchemaVS2015Dark});
            this.menuItemTools.MergeIndex = 2;
            this.menuItemTools.Name = "menuItemTools";
            this.menuItemTools.Size = new System.Drawing.Size(53, 26);
            this.menuItemTools.Text = "&主题";
            // 
            // menuItemSchemaVS2015Light
            // 
            this.menuItemSchemaVS2015Light.Name = "menuItemSchemaVS2015Light";
            this.menuItemSchemaVS2015Light.Size = new System.Drawing.Size(224, 26);
            this.menuItemSchemaVS2015Light.Text = "浅色";
            this.menuItemSchemaVS2015Light.Click += new System.EventHandler(this.SetSchema);
            // 
            // menuItemSchemaVS2015Blue
            // 
            this.menuItemSchemaVS2015Blue.Name = "menuItemSchemaVS2015Blue";
            this.menuItemSchemaVS2015Blue.Size = new System.Drawing.Size(224, 26);
            this.menuItemSchemaVS2015Blue.Text = "蓝色";
            this.menuItemSchemaVS2015Blue.Click += new System.EventHandler(this.SetSchema);
            // 
            // menuItemSchemaVS2015Dark
            // 
            this.menuItemSchemaVS2015Dark.Checked = true;
            this.menuItemSchemaVS2015Dark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemSchemaVS2015Dark.Name = "menuItemSchemaVS2015Dark";
            this.menuItemSchemaVS2015Dark.Size = new System.Drawing.Size(224, 26);
            this.menuItemSchemaVS2015Dark.Text = "暗色";
            this.menuItemSchemaVS2015Dark.Click += new System.EventHandler(this.SetSchema);
            // 
            // menuItemWindow
            // 
            this.menuItemWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNewWindow});
            this.menuItemWindow.MergeIndex = 2;
            this.menuItemWindow.Name = "menuItemWindow";
            this.menuItemWindow.Size = new System.Drawing.Size(53, 26);
            this.menuItemWindow.Text = "&窗口";
            // 
            // menuItemNewWindow
            // 
            this.menuItemNewWindow.Name = "menuItemNewWindow";
            this.menuItemNewWindow.Size = new System.Drawing.Size(224, 26);
            this.menuItemNewWindow.Text = "&New Window";
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemAbout});
            this.menuItemHelp.MergeIndex = 3;
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(53, 24);
            this.menuItemHelp.Text = "&帮助";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(245, 26);
            this.menuItemAbout.Text = "&About DockSample...";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // toolBar
            // 
            this.toolBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarButtonNew,
            this.toolBarButtonSeparator1,
            this.toolBarButtonSolutionExplorer,
            this.toolBarButtonPropertyWindow,
            this.toolBarButtonToolbox,
            this.toolBarButtonOutputWindow,
            this.toolBarButtonOpen,
            this.toolBarButtonSeparator2,
            this.toolBarButtonLayoutByCode,
            this.toolBarButtonLayoutByXml});
            this.toolBar.Location = new System.Drawing.Point(0, 28);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(1104, 27);
            this.toolBar.TabIndex = 35;
            // 
            // toolBarButtonNew
            // 
            this.toolBarButtonNew.Image = global::_305Vision.Properties.Resources.play_button_outline_green_icon;
            this.toolBarButtonNew.Name = "toolBarButtonNew";
            this.toolBarButtonNew.Size = new System.Drawing.Size(29, 28);
            this.toolBarButtonNew.ToolTipText = "运行";
            this.toolBarButtonNew.Click += new System.EventHandler(this.Start_Click);
            // 
            // toolBarButtonSeparator1
            // 
            this.toolBarButtonSeparator1.Name = "toolBarButtonSeparator1";
            this.toolBarButtonSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolBarButtonSolutionExplorer
            // 
            this.toolBarButtonSolutionExplorer.Image = ((System.Drawing.Image)(resources.GetObject("toolBarButtonSolutionExplorer.Image")));
            this.toolBarButtonSolutionExplorer.Name = "toolBarButtonSolutionExplorer";
            this.toolBarButtonSolutionExplorer.Size = new System.Drawing.Size(29, 24);
            this.toolBarButtonSolutionExplorer.ToolTipText = "工具箱";
            this.toolBarButtonSolutionExplorer.Click += new System.EventHandler(this.ToolForm_Click);
            // 
            // toolBarButtonPropertyWindow
            // 
            this.toolBarButtonPropertyWindow.Image = global::_305Vision.Properties.Resources.timber_color_icon;
            this.toolBarButtonPropertyWindow.Name = "toolBarButtonPropertyWindow";
            this.toolBarButtonPropertyWindow.Size = new System.Drawing.Size(29, 28);
            this.toolBarButtonPropertyWindow.ToolTipText = "属性栏";
            this.toolBarButtonPropertyWindow.Click += new System.EventHandler(this.PropertyGrid_Click);
            // 
            // toolBarButtonToolbox
            // 
            this.toolBarButtonToolbox.Image = global::_305Vision.Properties.Resources.upload_round_color_red_icon;
            this.toolBarButtonToolbox.Name = "toolBarButtonToolbox";
            this.toolBarButtonToolbox.Size = new System.Drawing.Size(29, 28);
            this.toolBarButtonToolbox.ToolTipText = "输出栏";
            this.toolBarButtonToolbox.Click += new System.EventHandler(this.OutForm_Click);
            // 
            // toolBarButtonOutputWindow
            // 
            this.toolBarButtonOutputWindow.Image = global::_305Vision.Properties.Resources.creative_idea_icon;
            this.toolBarButtonOutputWindow.Name = "toolBarButtonOutputWindow";
            this.toolBarButtonOutputWindow.Size = new System.Drawing.Size(29, 28);
            this.toolBarButtonOutputWindow.ToolTipText = "流程画布";
            this.toolBarButtonOutputWindow.Click += new System.EventHandler(this.ProcessForm_Click);
            // 
            // toolBarButtonOpen
            // 
            this.toolBarButtonOpen.Image = global::_305Vision.Properties.Resources.study_icon;
            this.toolBarButtonOpen.Name = "toolBarButtonOpen";
            this.toolBarButtonOpen.Size = new System.Drawing.Size(29, 28);
            this.toolBarButtonOpen.ToolTipText = "图窗显示";
            this.toolBarButtonOpen.Click += new System.EventHandler(this.NewCountPlatform_Click);
            // 
            // toolBarButtonSeparator2
            // 
            this.toolBarButtonSeparator2.Name = "toolBarButtonSeparator2";
            this.toolBarButtonSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolBarButtonLayoutByCode
            // 
            this.toolBarButtonLayoutByCode.Image = global::_305Vision.Properties.Resources.painting_bucket_logo_icon;
            this.toolBarButtonLayoutByCode.Name = "toolBarButtonLayoutByCode";
            this.toolBarButtonLayoutByCode.Size = new System.Drawing.Size(29, 28);
            this.toolBarButtonLayoutByCode.ToolTipText = "测试1";
            this.toolBarButtonLayoutByCode.Click += new System.EventHandler(this.test1);
            // 
            // toolBarButtonLayoutByXml
            // 
            this.toolBarButtonLayoutByXml.Image = global::_305Vision.Properties.Resources.painting_bucket_logo_icon;
            this.toolBarButtonLayoutByXml.Name = "toolBarButtonLayoutByXml";
            this.toolBarButtonLayoutByXml.Size = new System.Drawing.Size(29, 28);
            this.toolBarButtonLayoutByXml.ToolTipText = "测试2";
            this.toolBarButtonLayoutByXml.Click += new System.EventHandler(this.test2);
            // 
            // dockPanel
            // 
            this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.dockPanel.DockBottomPortion = 0.15D;
            this.dockPanel.DockLeftPortion = 0.15D;
            this.dockPanel.DockRightPortion = 0.15D;
            this.dockPanel.DockTopPortion = 0.15D;
            this.dockPanel.Location = new System.Drawing.Point(0, 55);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel.ShowAutoHideContentOnHover = false;
            this.dockPanel.Size = new System.Drawing.Size(1104, 529);
            this.dockPanel.TabIndex = 38;
            this.dockPanel.Theme = this.vS2015DarkTheme1;
            // 
            // vsToolStripExtender1
            // 
            this.vsToolStripExtender1.DefaultRenderer = null;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(1089, 20);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "MyVision";
            // 
            // statusBar
            // 
            this.statusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusBar.Location = new System.Drawing.Point(0, 584);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1104, 26);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1104, 610);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.statusBar);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "305Vision";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme vS2015DarkTheme1;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemClose;
        private System.Windows.Forms.ToolStripSeparator menuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemView;
        private System.Windows.Forms.ToolStripMenuItem menuItemSolutionExplorer;
        private System.Windows.Forms.ToolStripMenuItem menuItemPropertyWindow;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolbox;
        private System.Windows.Forms.ToolStripMenuItem menuItemOutputWindow;
        private System.Windows.Forms.ToolStripMenuItem menuItemTaskList;
        private System.Windows.Forms.ToolStripSeparator menuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolBar;
        private System.Windows.Forms.ToolStripMenuItem menuItemStatusBar;
        private System.Windows.Forms.ToolStripSeparator menuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuItemLayoutByCode;
        private System.Windows.Forms.ToolStripMenuItem menuItemLayoutByXml;
        private System.Windows.Forms.ToolStripMenuItem menuItemTools;
        private System.Windows.Forms.ToolStripMenuItem menuItemSchemaVS2015Light;
        private System.Windows.Forms.ToolStripMenuItem menuItemSchemaVS2015Blue;
        private System.Windows.Forms.ToolStripMenuItem menuItemSchemaVS2015Dark;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindow;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewWindow;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton toolBarButtonNew;
        private System.Windows.Forms.ToolStripButton toolBarButtonOpen;
        private System.Windows.Forms.ToolStripSeparator toolBarButtonSeparator1;
        private System.Windows.Forms.ToolStripButton toolBarButtonSolutionExplorer;
        private System.Windows.Forms.ToolStripButton toolBarButtonPropertyWindow;
        private System.Windows.Forms.ToolStripButton toolBarButtonToolbox;
        private System.Windows.Forms.ToolStripButton toolBarButtonOutputWindow;
        private System.Windows.Forms.ToolStripSeparator toolBarButtonSeparator2;
        private System.Windows.Forms.ToolStripButton toolBarButtonLayoutByCode;
        private System.Windows.Forms.ToolStripButton toolBarButtonLayoutByXml;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private WeifenLuo.WinFormsUI.Docking.VisualStudioToolStripExtender vsToolStripExtender1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusBar;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme1;
    }
}

