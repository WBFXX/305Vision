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
            g.DrawString(PName, this.Font, Brushes.White, this.ClientRectangle, m_sf); // 绘制枚举值文本

            // 绘制下拉箭头(灰色，三个点填充)
            g.FillPolygon(Brushes.Gray, new Point[]{
                new Point(this.Right - 25, 7),
                new Point(this.Right - 15, 7),
                new Point(this.Right - 20, 12)
            });
        }


        private List<PictureBox> m_ = new List<PictureBox>();

        public string PName
        {
            get => pName; set
            {
                pName = value;
                this.Invalidate();
            }
        }
        public event EventHandler ValueChanged; // 定义值改变事件
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, e); // 触发值改变事件
        }

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            //MessageBox.Show(e.ToString());
            if (FormPlatform.Instance.IsHidden)
            {
                m_ = null;
            }else m_ = FormPlatform.Instance.PictureBoxes;

            // 计算弹出窗口的位置
            /*这一行是用来计算弹出窗口的位置 pt 的。让我解释一下这里各个参数的含义：

            this.Left: 控件（ComBoxControl）在其容器（通常是 Form 或其他控件）中的左边缘相对于容器左边缘的距离。

            this.Owner: 获取或设置此节点控件的所有者窗体或控件。在你的代码中，ComBoxControl 有一个所有者，即 STNodeControl。

            this.Owner.Left: STNodeControl 的左边缘相对于其容器（通常是 Form）左边缘的距离。

            this.Top: 控件的上边缘相对于其容器上边缘的距离。

            this.Owner.Top: STNodeControl 的上边缘相对于其容器上边缘的距离。

            this.Owner.TitleHeight: STNodeControl 的标题栏高度。这个值通常是标题栏的高度，可能包括任何标题栏上的边框或其他装饰。

            总体来说，pt 的计算是为了将 ComBoxControl 在其容器中的位置转换为屏幕坐标系中的位置。这将被用作弹出窗口的位置，确保弹出窗口显示在正确的位置上。

            关于 Owner 的解释：Owner 属性指定 STNodeControl 的所有者窗体或控件。它指定了该控件在视觉上所属的容器。如果控件是在 Form 中，那么 Form 就是它的所有者。这有助于控件协调在窗体中的位置。
            如果没有所有者，Owner 可能是 null。
             */
            Point pt = new Point(this.Left + this.Owner.Left, this.Top + this.Owner.Top + this.Owner.TitleHeight + this.Owner.TitleHeight);
            pt = this.Owner.Owner.CanvasToControl(pt);
            pt = this.Owner.Owner.PointToScreen(pt);

            // 创建并显示枚举选择窗口
            ComBoxCL frm = new ComBoxCL(m_ , pt, this.Width, this.Owner.Owner.CanvasScale ,pName);
            this.pName = frm.SelectPreText;

            var v = frm.ShowDialog();

            // 如果用户确认选择
            if (v == System.Windows.Forms.DialogResult.OK)
            {
                this.PName = frm.SelectPreText;
                this.OnValueChanged(new EventArgs());
            }

            
        }
        //public event EventHandler ValueChanged; // 定义值改变事件
        //protected virtual void OnValueChanged(EventArgs e)
        //{
        //    if (this.ValueChanged != null)
        //        this.ValueChanged(this, e); // 触发值改变事件
        //}
    }
}
