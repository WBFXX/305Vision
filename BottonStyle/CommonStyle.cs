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
    /// 节点基类，藏青色
    /// </summary>

    public class CommonStyle : TypeColorStyle
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            this.TitleColor = Color.FromArgb(200, Color.CornflowerBlue);
        }
        
    }
}
