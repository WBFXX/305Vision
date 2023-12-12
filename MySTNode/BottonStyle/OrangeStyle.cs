using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.BottonStyle
{
    /// <summary>
    /// 节点基类，暗橘色
    /// </summary>

    public class OrangeStyle : TypeColorStyle
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            this.AutoSize = false;
            this.Width = 100;
            this.Height = 100;
            this.TitleColor = Color.FromArgb(200, Color.DarkOrange);
        }
    }
}
