using _305Vision.DAL;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.BLL
{
    /// <summary>
    /// 节点界面操作
    /// </summary>
    public class STNodeBLL
    {
        STNodeDAL STNodeDAL = new STNodeDAL();

        /// <summary>
        /// 图像框BODY
        /// </summary>
        /// <param name="dt">传dt</param>
        /// <param name="m_img_draw">传m_img_draw</param>
        /// <param name="x">传this.Left</param>
        /// <param name="y">传this.Top</param>
        public static void DrawBody(DrawingTools dt, Image m_img_draw, int x, int y)
        {
            STNodeDAL.DrawBody(dt, m_img_draw, x, y);
        }
    }
}
