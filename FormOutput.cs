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
    public partial class FormOutput : DockContent
    {
        public FormOutput()
        {
            InitializeComponent();
        }
        private static FormOutput _instance;
        public static FormOutput Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                    _instance = new FormOutput();
                return _instance; 
            }
        }
    }
}
