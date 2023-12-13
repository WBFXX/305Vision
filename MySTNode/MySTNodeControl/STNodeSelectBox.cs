using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using ST.Library.UI.NodeEditor;

namespace _305Vision.Blender
{
    /// <summary>
    /// 这是一个演示作为 MixRGB 节点的下拉框控件的类
    /// </summary>
    public class STNodeSelectEnumBox : STNodeControl
    {
        private System.Enum _Enum; // 存储枚举值的字段
        public System.Enum Enum
        {
            get { return _Enum; }
            set
            {
                _Enum = value;
                this.Invalidate(); // 数据改变后，使控件无效，触发重绘
            }
        }

        public event EventHandler ValueChanged; // 定义值改变事件
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, e); // 触发值改变事件
        }

        protected override void OnPaint(DrawingTools dt)
        {
            Graphics g = dt.Graphics;
            dt.SolidBrush.Color = Color.FromArgb(80, 0, 0, 0);
            g.FillRectangle(dt.SolidBrush, this.ClientRectangle);

            m_sf.Alignment = StringAlignment.Near;
            g.DrawString(this.Enum.ToString(), this.Font, Brushes.White, this.ClientRectangle, m_sf); // 绘制枚举值文本

            // 绘制下拉箭头
            g.FillPolygon(Brushes.Gray, new Point[]{
                new Point(this.Right - 25, 7),
                new Point(this.Right - 15, 7),
                new Point(this.Right - 20, 12)
            });
        }

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // 计算弹出窗口的位置
            Point pt = new Point(this.Left + this.Owner.Left, this.Top + this.Owner.Top + this.Owner.TitleHeight);
            pt = this.Owner.Owner.CanvasToControl(pt);
            pt = this.Owner.Owner.PointToScreen(pt);

            // 创建并显示枚举选择窗口
            FrmEnumSelect frm = new FrmEnumSelect(this.Enum, pt, this.Width, this.Owner.Owner.CanvasScale);
            var v = frm.ShowDialog();

            // 如果用户确认选择，则更新枚举值并触发值改变事件
            if (v == System.Windows.Forms.DialogResult.OK)
            {
                this.Enum = frm.Enum;
                this.OnValueChanged(new EventArgs());
            }
        }
    }
}
