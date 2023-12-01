using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.通用
{
    [STNode("/","接受数据")]
    public class Input_demo : STNode
    {
        private int _id = 0;

        [STNodeProperty("number", "接受来的数字")]
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                in_op_1.TransferData(value);
                this.Invalidate();
            }
        }

        private STNodeOption in_op_1;

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "输入";
            in_op_1 = this.OutputOptions.Add("输出",typeof(int),false);
        }


    }

    
}
