using ST.Library.UI.NodeEditor;
using System.Drawing;

namespace _305Vision.Utils
{
    /// <summary>
    /// 节点图像框
    /// </summary>
    public class MyDrawBody
    {
        /// <summary>
        /// 图像框BODY
        /// </summary>
        /// <param name="dt">传dt</param>
        /// <param name="m_img_draw">传m_img_draw</param>
        /// <param name="x">传this.Left</param>
        /// <param name="y">传this.Top</param>
        public void DrawBody(DrawingTools dt, Image m_img_draw, int x, int y)
        {
            Graphics g = dt.Graphics;
            Rectangle rect = new Rectangle(x + 10, y + 40, 140, 80);
            g.FillRectangle(Brushes.Gray, rect);
            if (m_img_draw != null) g.DrawImage(m_img_draw, rect);
        }
    }

}