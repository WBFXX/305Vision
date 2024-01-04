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
    public class CustomToolStripRenderer : ToolStripProfessionalRenderer
    {
        // 自定义底部分隔线颜色
        private Color separatorColor = Color.FromArgb(255, 0, 0); // 使用 RGB 颜色

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBorder(e);
            // 绘制底部分隔线
            Rectangle borderRect = new Rectangle(0, e.ToolStrip.Height - 1, e.ToolStrip.Width, 1);

            using (SolidBrush brush = new SolidBrush(separatorColor))
            {
                e.Graphics.FillRectangle(brush, borderRect);
            }
        }
    }
}
