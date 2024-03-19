namespace _305Vision.OWindows
{
    partial class RecROIForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureWindow1 = new PictureWindowControl.PictureWindow();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(211, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(430, 415);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "依框裁剪";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnCrop_Click);
            // 
            // pictureWindow1
            // 
            this.pictureWindow1.BackColor = System.Drawing.Color.Black;
            this.pictureWindow1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureWindow1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureWindow1.Image = null;
            this.pictureWindow1.Init = true;
            this.pictureWindow1.Location = new System.Drawing.Point(0, 0);
            this.pictureWindow1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureWindow1.Name = "pictureWindow1";
            this.pictureWindow1.Size = new System.Drawing.Size(752, 408);
            this.pictureWindow1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureWindow1.TabIndex = 7;
            // 
            // RecROIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 453);
            this.Controls.Add(this.pictureWindow1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecROIForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RetangelROI";
            this.Load += new System.EventHandler(this.RecROIForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private PictureWindowControl.PictureWindow pictureWindow1;
    }
}