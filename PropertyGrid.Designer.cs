namespace _305Vision
{
    partial class PropertyGrid
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
            this.stNodePropertyGrid1 = new ST.Library.UI.NodeEditor.STNodePropertyGrid();
            this.SuspendLayout();
            // 
            // stNodePropertyGrid1
            // 
            this.stNodePropertyGrid1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.stNodePropertyGrid1.DescriptionColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(184)))), ((int)(((byte)(134)))), ((int)(((byte)(11)))));
            this.stNodePropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stNodePropertyGrid1.ErrorColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(165)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.stNodePropertyGrid1.ForeColor = System.Drawing.Color.White;
            this.stNodePropertyGrid1.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))), ((int)(((byte)(125)))));
            this.stNodePropertyGrid1.ItemValueBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.stNodePropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.stNodePropertyGrid1.MinimumSize = new System.Drawing.Size(120, 50);
            this.stNodePropertyGrid1.Name = "stNodePropertyGrid1";
            this.stNodePropertyGrid1.ShowTitle = true;
            this.stNodePropertyGrid1.Size = new System.Drawing.Size(238, 645);
            this.stNodePropertyGrid1.TabIndex = 1;
            this.stNodePropertyGrid1.Text = "属性";
            this.stNodePropertyGrid1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            // 
            // PropertyGrid
            // 
            this.AutoHidePortion = 0.15D;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 645);
            this.ControlBox = false;
            this.Controls.Add(this.stNodePropertyGrid1);
            this.HideOnClose = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PropertyGrid";
            this.ShowIcon = false;
            this.Text = "属性";
            this.ResumeLayout(false);

        }

        #endregion

        private ST.Library.UI.NodeEditor.STNodePropertyGrid stNodePropertyGrid1;
    }
}