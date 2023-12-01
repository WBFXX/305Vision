using _305Vision.BottonStyle;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.通用
{
    [STNode("","测试")]
    public class test1 : OrangeStyle
    {
        STNodeOption in_op_1;
        private int a;

        public int A
        {
            get => a; set
            {
                a = value;
                in_op_1.TransferData(value);
                this.Invalidate();

            }
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "测试";
            in_op_1 = OutputOptions.Add("测试",typeof(int),false);
        }
    }
}
