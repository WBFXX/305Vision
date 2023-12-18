namespace _305Vision
{
    partial class FormPlatform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.图像数量1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像数量2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像数量4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像数量9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图像数量60ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.SizeChanged += new System.EventHandler(this.flowLayoutPanel1_SizeChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图像数量1ToolStripMenuItem,
            this.图像数量2ToolStripMenuItem,
            this.图像数量4ToolStripMenuItem,
            this.图像数量9ToolStripMenuItem,
            this.图像数量60ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(172, 124);
            // 
            // 图像数量1ToolStripMenuItem
            // 
            this.图像数量1ToolStripMenuItem.Name = "图像数量1ToolStripMenuItem";
            this.图像数量1ToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.图像数量1ToolStripMenuItem.Text = "图像数量：1";
            // 
            // 图像数量2ToolStripMenuItem
            // 
            this.图像数量2ToolStripMenuItem.Name = "图像数量2ToolStripMenuItem";
            this.图像数量2ToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.图像数量2ToolStripMenuItem.Text = "图像数量：2";
            // 
            // 图像数量4ToolStripMenuItem
            // 
            this.图像数量4ToolStripMenuItem.Name = "图像数量4ToolStripMenuItem";
            this.图像数量4ToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.图像数量4ToolStripMenuItem.Text = "图像数量：4";
            // 
            // 图像数量9ToolStripMenuItem
            // 
            this.图像数量9ToolStripMenuItem.Name = "图像数量9ToolStripMenuItem";
            this.图像数量9ToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.图像数量9ToolStripMenuItem.Text = "图像数量：9";
            // 
            // 图像数量60ToolStripMenuItem
            // 
            this.图像数量60ToolStripMenuItem.Name = "图像数量60ToolStripMenuItem";
            this.图像数量60ToolStripMenuItem.Size = new System.Drawing.Size(171, 24);
            this.图像数量60ToolStripMenuItem.Text = "图像数量：60";
            // 
            // FormPlatform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowLayoutPanel1);
            this.HideOnClose = true;
            this.Name = "FormPlatform";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "主窗口";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 图像数量1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像数量2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像数量4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像数量9ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图像数量60ToolStripMenuItem;
    }
}