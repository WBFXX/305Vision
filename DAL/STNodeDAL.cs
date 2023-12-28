using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.DAL
{
    public class STNodeDAL
    {


        /// <summary>
        /// 图像框BODY
        /// </summary>
        /// <param name="dt">传dt</param>
        /// <param name="m_img_draw">传m_img_draw</param>
        /// <param name="x">传this.Left</param>
        /// <param name="y">传this.Top</param>
        public static bool DrawBody(DrawingTools dt, Image m_img_draw, int x, int y)
        {
            try
            {
                Graphics g = dt.Graphics;
                Rectangle rect = new Rectangle(x + 10, y + 40, 140, 80);
                g.FillRectangle(Brushes.Gray, rect);
                if (m_img_draw != null) g.DrawImage(m_img_draw, rect);

                return true;

            }catch (Exception ex)
            {
                return false;
            }
            

            
        }

    }
    
}
