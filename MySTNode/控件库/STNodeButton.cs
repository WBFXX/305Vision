using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.MySTNode.控件库
{
    public class STNodeButton : STNodeControl
    {

        private bool m_b_enter;
        private bool m_b_down;

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            m_b_enter = true;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            m_b_enter = false;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_b_down = true;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_b_down = false;
            this.Invalidate();
        }

        protected override void OnPaint(DrawingTools dt)
        {
            //base.OnPaint(dt);
            Graphics g = dt.Graphics;
            SolidBrush brush = dt.SolidBrush;
            brush.Color = base.BackColor;
            if (m_b_down) brush.Color = Color.SkyBlue;
            else if (m_b_enter) brush.Color = Color.DodgerBlue;
            g.FillRectangle(brush, 0, 0, this.Width, this.Height);
            g.DrawString(this.Text, this.Font, Brushes.White, this.ClientRectangle, base.m_sf);
        }
    }
}
