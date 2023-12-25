using _305Vision.Service;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace _305Vision
{
    public partial class ToolsBox : DockContent
    {
        private static ToolsBox _instance;
        public static ToolsBox Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new ToolsBox();
                return _instance;
            }
        }
        public ToolsBox()
        {
            InitializeComponent(); 
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            stNodeTreeView1.LoadAssembly(Application.ExecutablePath);
            //stNodeTreeView1.AutoScroll = true;
            stNodePropertyGrid1.Text = "属性栏";
            stNodePropertyGrid1.SetInfoKey("作者", "邮箱", "联系","帮助");
            NodeService.STNodePropertyGrid = stNodePropertyGrid1;
            
        }
    }
}
