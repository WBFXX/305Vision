using _305Vision.Utils;
using _305Vision.图片操作测试;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.MySTNode.Operator
{
    [STNode("/算子","图像腐蚀")]
    public class Eroding : ImageBaseNode
    {

        private STNodeOption in_option;

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "图像腐蚀";

            in_option = this.InputOptions.Add("输入图像", typeof(Image), true);
            in_option.DataTransfer += In_option_DataTransfer;
        }

        private void In_option_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            
        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            MyDrawBody myDrawBody = new MyDrawBody();
            myDrawBody.DrawBody(dt, m_img_draw,this.Left,this.Right);
        }
    }
}
