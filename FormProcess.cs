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
    public partial class FormProcess : DockContent
    {
        private static FormProcess _instance;
        public static FormProcess Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new FormProcess();
                return _instance;
            }
        }
        
        public FormProcess()
        {
            InitializeComponent();
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            stNodeEditor1.LoadAssembly(Application.ExecutablePath);
            stNodeEditor1.ActiveChanged += (s, ea) => NodeService.STNodePropertyGrid.SetNode(stNodeEditor1.ActiveNode);
            stNodeEditor1.OptionConnected += (s, ea) => stNodeEditor1.ShowAlert(ea.Status.ToString(), Color.White, ea.Status == ConnectionStatus.Connected ? Color.FromArgb(125, Color.Green) : Color.FromArgb(125, Color.Red));
            stNodeEditor1.CanvasScaled += (s, ea) => stNodeEditor1.ShowAlert(stNodeEditor1.CanvasScale.ToString("F2"), Color.White, Color.FromArgb(125, Color.Yellow));
            //在这里使用 ContextMenuStrip 是为了定义一个与节点关联的上下文菜单。
            //当用户右键点击一个节点时，如果该节点有关联的 ContextMenuStrip，
            //那么这个菜单就会在右键点击时显示出来。
            stNodeEditor1.NodeAdded += (s, ea) => ea.Node.ContextMenuStrip = contextMenuStrip1;

        }

        

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stNodeEditor1.ActiveNode == null) return;
            RemoveSelectNode(stNodeEditor1.GetSelectedNode());
        }

        /// <summary>
        /// 删除所有被选中的节点
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void RemoveSelectNode(STNode[] sTNodes)
        {
            foreach (STNode node in sTNodes)
            {
                stNodeEditor1.Nodes.Remove(node);
            }
        }

        private void 位置锁定解除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stNodeEditor1.ActiveNode == null) return;
            stNodeEditor1.ActiveNode.LockLocation = !stNodeEditor1.ActiveNode.LockLocation;
        }

        private void 连接锁定解除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stNodeEditor1.ActiveNode.LockOption = !stNodeEditor1.ActiveNode.LockOption;
        }

        private void stNodeEditor1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    RemoveSelectNode(stNodeEditor1.GetSelectedNode());
                    break;
            }
        }
    }
}
