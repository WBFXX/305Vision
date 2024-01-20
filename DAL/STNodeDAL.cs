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

                if (m_img_draw != null)
                {
                    // 计算按比例缩放后的宽度和高度
                    int scaledWidth, scaledHeight;
                    ScaleToFit(m_img_draw.Width, m_img_draw.Height, rect.Width, rect.Height, out scaledWidth, out scaledHeight);

                    // 计算绘制图像的位置
                    int drawX = x + 10 + (rect.Width - scaledWidth) / 2;
                    int drawY = y + 40 + (rect.Height - scaledHeight) / 2;

                    // 绘制图像
                    g.DrawImage(m_img_draw, drawX, drawY, scaledWidth, scaledHeight);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 按比例缩放图像
        /// </summary>
        /// <param name="originalWidth"></param>
        /// <param name="originalHeight"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="scaledWidth"></param>
        /// <param name="scaledHeight"></param>
        private static void ScaleToFit(int originalWidth, int originalHeight, int maxWidth, int maxHeight, out int scaledWidth, out int scaledHeight)
        {
            double widthRatio = (double)maxWidth / originalWidth;
            double heightRatio = (double)maxHeight / originalHeight;
            double ratio = Math.Min(widthRatio, heightRatio);

            scaledWidth = (int)(originalWidth * ratio);
            scaledHeight = (int)(originalHeight * ratio);
        }


    }

}
