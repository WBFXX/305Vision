using _305Vision.Blender;
using _305Vision.BottonStyle;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.通用
{
    [STNode("","测试")]
    public class MyNode : STNode
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "MyNode";
            this.TitleColor = Color.FromArgb(200, Color.Goldenrod);
            //需要先设置AutoSize=false 才能设置STNode大小
            this.AutoSize = false;
            this.Size = new Size(100, 100);

            //var ctrl = new STNodeCheckBox();
            //ctrl.Text = "Button";
            //ctrl.Location = new Point(10, 10);
            //this.Controls.Add(ctrl);
            //ctrl.MouseClick += new MouseEventHandler(ctrl_MouseClick);
            var ctr = new textBox();
            ctr.Location = new Point(10,10);
            this.Controls.Add(ctr);
        }

        void ctrl_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("MouseClick");
        }
    }
}
