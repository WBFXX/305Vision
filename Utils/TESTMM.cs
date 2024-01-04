using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.Utils
{
    public class TESTMM : ToolStripProfessionalRenderer
    {

       
        

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (SolidBrush sb = new SolidBrush(Color.FromArgb(34, 34, 34)))
            {
                e.Graphics.FillRectangle(sb, e.AffectedBounds);
            }
            base.OnRenderToolStripBackground(e);
        }
        }
}
