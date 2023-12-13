using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _305Vision.Blender
{
    /// <summary>
    /// 这是一个演示作为 MixRGB 节点的下拉选择框弹出菜单的窗体类
    /// </summary>
    public class FrmEnumSelect : Form
    {
        private Point m_pt; // 弹出窗口的位置
        private int m_nWidth; // 弹出窗口的宽度
        private float m_scale; // 缩放比例
        private List<object> m_lst = new List<object>(); // 存储枚举值的列表
        private StringFormat m_sf; // 字符串格式化对象

        public System.Enum Enum { get; set; } // 选定的枚举值属性

        private bool m_bClosed; // 标记窗口是否已关闭

        // 构造函数，接收枚举值、弹出位置、宽度和缩放比例等参数
        public FrmEnumSelect(System.Enum e, Point pt, int nWidth, float scale)
        {
            // 设置控件样式，启用双缓冲等
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            // 初始化窗口属性
            foreach (var v in System.Enum.GetValues(e.GetType()))
                m_lst.Add(v); // 将枚举值添加到列表中
            this.Enum = e; // 设置初始选定的枚举值
            m_pt = pt; // 设置弹出窗口的位置
            m_scale = scale; // 设置缩放比例
            m_nWidth = nWidth; // 设置弹出窗口的宽度
            m_sf = new StringFormat();
            m_sf.LineAlignment = StringAlignment.Center;

            // 设置窗口样式
            this.ShowInTaskbar = false;
            this.BackColor = Color.FromArgb(255, 34, 34, 34);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Location = m_pt;
            this.Width = (int)(m_nWidth * m_scale);
            this.Height = (int)(m_lst.Count * 20 * m_scale);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.ScaleTransform(m_scale, m_scale); // 缩放绘图
            Rectangle rect = new Rectangle(0, 0, this.Width, 20);

            foreach (var v in m_lst)
            {
                g.DrawString(v.ToString(), this.Font, Brushes.White, rect, m_sf); // 绘制枚举值文本
                rect.Y += rect.Height;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            int nIndex = e.Y / (int)(20 * m_scale); // 计算点击的位置对应的枚举值索引
            if (nIndex >= 0 && nIndex < m_lst.Count)
                this.Enum = (System.Enum)m_lst[nIndex]; // 更新选定的枚举值
            this.DialogResult = System.Windows.Forms.DialogResult.OK; // 设置对话框结果为确认
            m_bClosed = true; // 标记窗口已关闭
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (m_bClosed) return;
            //this.DialogResult = System.Windows.Forms.DialogResult.None; // 设置对话框结果为无
            this.Close(); // 关闭窗口
        }
    }
}
