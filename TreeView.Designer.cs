namespace _305Vision
{
    partial class TreeView
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
            this.stNodeEditorPannel1 = new ST.Library.UI.NodeEditor.STNodeEditorPannel();
            this.SuspendLayout();
            // 
            // stNodeEditorPannel1
            // 
            this.stNodeEditorPannel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.stNodeEditorPannel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stNodeEditorPannel1.Location = new System.Drawing.Point(0, 0);
            this.stNodeEditorPannel1.MinimumSize = new System.Drawing.Size(250, 250);
            this.stNodeEditorPannel1.Name = "stNodeEditorPannel1";
            this.stNodeEditorPannel1.Size = new System.Drawing.Size(803, 508);
            this.stNodeEditorPannel1.TabIndex = 0;
            this.stNodeEditorPannel1.Text = "stNodeEditorPannel1";
            this.stNodeEditorPannel1.Y = 250;
            // 
            // TreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 508);
            this.Controls.Add(this.stNodeEditorPannel1);
            this.Name = "TreeView";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ST.Library.UI.NodeEditor.STNodeEditorPannel stNodeEditorPannel1;
    }
}