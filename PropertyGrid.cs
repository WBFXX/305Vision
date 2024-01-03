using _305Vision.Model;
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
    public partial class PropertyGrid : DockContent
    {
        private static PropertyGrid _instance;
        public static PropertyGrid Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new PropertyGrid();
                return _instance;
            }
        }

        public PropertyGrid()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            //stNodeTreeView1.AutoScroll = true;
            stNodePropertyGrid1.Text = "属性栏";
            stNodePropertyGrid1.SetInfoKey("作者", "邮箱", "联系", "帮助");
            NodePropertyGridInfo.STNodePropertyGrid = stNodePropertyGrid1;

        }
    }
}
