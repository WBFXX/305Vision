using System;
using System.Drawing;
using ST.Library.UI.NodeEditor;

namespace _305Vision.Blender
{
    /// <summary>
    /// 这是一个演示作为 MixRGB 节点的复选框控件的类
    /// </summary>
    public class STNodeCheckBox : STNodeControl
    {
        private bool _Checked; // 存储复选框的选中状态

        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                this.Invalidate(); // 数据改变后，使控件无效，触发重绘
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
            this.Checked = !this.Checked; // 切换复选框的选中状态
            this.OnValueChanged(new EventArgs()); // 触发值改变事件
        }

        protected override void OnPaint(DrawingTools dt)
        {
            //base.OnPaint(dt); // 注释掉基类的 OnPaint 调用，以避免使用基类的绘制逻辑

            Graphics g = dt.Graphics;

            // 绘制复选框外框
            g.FillRectangle(Brushes.Gray, 0, 5, 10, 10);

            m_sf.Alignment = StringAlignment.Near;

            // 绘制复选框文本
            g.DrawString(this.Text, this.Font, Brushes.LightGray, new Rectangle(15, 0, this.Width - 20, 20), m_sf);

            // 如果复选框被选中，绘制选中状态
            if (this.Checked)
            {
                g.FillRectangle(Brushes.Black, 2, 7, 6, 6);
            }
        }
    }
}
