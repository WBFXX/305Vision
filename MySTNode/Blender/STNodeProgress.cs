using System;
using ST.Library.UI.NodeEditor;
using System.Drawing;

namespace _305Vision.Blender
{
    /// <summary>
    /// 这是一个演示作为 MixRGB 节点的进度条控件的类
    /// </summary>
    public class STNodeProgress : STNodeControl
    {
        private int _Value = 50; // 进度条的值，范围在 0 到 100 之间

        public int Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                this.Invalidate(); // 数据改变后，使控件无效，触发重绘
            }
        }

        private bool m_bMouseDown; // 标记鼠标是否按下

        public event EventHandler ValueChanged; // 定义值改变事件
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
                this.ValueChanged(this, e); // 触发值改变事件
        }

        protected override void OnPaint(DrawingTools dt)
        {
            base.OnPaint(dt);
            Graphics g = dt.Graphics;

            // 绘制进度条底部
            g.FillRectangle(Brushes.Gray, this.ClientRectangle);

            // 绘制进度条的实际值
            g.FillRectangle(Brushes.CornflowerBlue, 0, 0, (int)((float)this._Value / 100 * this.Width), this.Height);

            m_sf.Alignment = StringAlignment.Near;

            // 绘制文本
            g.DrawString(this.Text, this.Font, Brushes.White, this.ClientRectangle, m_sf);

            m_sf.Alignment = StringAlignment.Far;

            // 绘制百分比文本
            g.DrawString(((float)this._Value / 100).ToString("F2"), this.Font, Brushes.White, this.ClientRectangle, m_sf);
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_bMouseDown = true; // 鼠标按下，设置标记为 true
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_bMouseDown = false; // 鼠标释放，设置标记为 false
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!m_bMouseDown) return;

            // 计算鼠标相对于控件宽度的百分比值
            int v = (int)((float)e.X / this.Width * 100);

            // 限制值在 0 到 100 之间
            if (v < 0) v = 0;
            if (v > 100) v = 100;

            this._Value = v; // 更新进度条的值
            this.OnValueChanged(new EventArgs()); // 触发值改变事件
            this.Invalidate(); // 使控件无效，触发重绘
        }
    }
}
