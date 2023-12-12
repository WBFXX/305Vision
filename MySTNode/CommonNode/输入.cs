using _305Vision.BottonStyle;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.通用
{
    [STNode("/","输入数字")]
    public class 输入 : CommonStyle
    {
        private STNodeOption option1;
        private STNodeOption outOption1;
        private int _Number;
        [STNodeProperty("数字", "输入阿拉伯数字")]
        public int Number { 
            get => _Number; 
            set  {
                _Number = value;
                outOption1.TransferData(value);
                }
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "通用测试";
            option1 = this.InputOptions.Add("输入", typeof(int), true);
            outOption1 = this.OutputOptions.Add("输出", typeof(int), false);
            this.AutoSize = false;
            this.Height = 100;
            this.Width = 100;
            //outOption1.TransferData(_Number);
            option1.DataTransfer += new STNodeOptionEventHandler(option_in_Datatransfer);

        }
        void option_in_Datatransfer(object sender, STNodeOptionEventArgs e)
        {
            if (e.Status == ConnectionStatus.Connected)
            {
                if (sender == option1)
                {
                    if (e.TargetOption.Data != null) Number = (int)e.TargetOption.Data;//TargetOption为触发此事件的Option
                    option1.Data = Number;
                    
                }
            }
            else
            {
                if (sender == option1) Number= 0; else Number = 0;
            }
            //向输出选项上的所有连线传输数据 输出选项上的所有连线都会触发 DataTransfer 事件
            outOption1.TransferData(Number); //m_out_num.Data 将被自动设置
        }


    }
}
