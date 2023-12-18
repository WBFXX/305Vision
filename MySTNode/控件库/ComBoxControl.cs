using _305Vision.Blender;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.MySTNode.图片操作
{
    public class ComBoxControl : STNodeControl
    {
        private string pName;

        
        protected override void OnPaint(DrawingTools dt)
        {
            Graphics g = dt.Graphics;
            dt.SolidBrush.Color = Color.FromArgb(80, 0, 0, 0);
            //这行代码的作用是在窗体的客户区域内填充矩形，
            //颜色由 dt.SolidBrush 定义。这通常用于在绘制新的内容之前清空整个绘图区域，
            //确保绘图区域的背景色是指定的颜色。
            g.FillRectangle(dt.SolidBrush, this.ClientRectangle);
            //文本在指定的矩形区域内水平左对齐
            m_sf.Alignment = StringAlignment.Near;
            //文本、字体、颜色、所在矩形框、对齐方式
            g.DrawString(ComBoxCL.SelectPreText, this.Font, Brushes.White, this.ClientRectangle, m_sf); // 绘制枚举值文本

            // 绘制下拉箭头(灰色，三个点填充)
            g.FillPolygon(Brushes.Gray, new Point[]{
                new Point(this.Right - 25, 7),
                new Point(this.Right - 15, 7),
                new Point(this.Right - 20, 12)
            });
        }


        private List<PictureBox> m_ = new List<PictureBox>();

        public string PName { get => pName; set => pName = value; }

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            m_ = FormPlatform.PictureBoxes;
           
            // 计算弹出窗口的位置
            Point pt = new Point(this.Left + this.Owner.Left, this.Top + this.Owner.Top + this.Owner.TitleHeight);
            pt = this.Owner.Owner.CanvasToControl(pt);
            pt = this.Owner.Owner.PointToScreen(pt);

            // 创建并显示枚举选择窗口
            ComBoxCL frm = new ComBoxCL(m_ , pt, this.Width, this.Owner.Owner.CanvasScale);
            frm.ShowDialog();

            //// 如果用户确认选择
            //if (v == System.Windows.Forms.DialogResult.OK)
            //{

            //    this.OnValueChanged(new EventArgs());
            //}


        }
        //public event EventHandler ValueChanged; // 定义值改变事件
        //protected virtual void OnValueChanged(EventArgs e)
        //{
        //    if (this.ValueChanged != null)
        //        this.ValueChanged(this, e); // 触发值改变事件
        //}
    }
}
