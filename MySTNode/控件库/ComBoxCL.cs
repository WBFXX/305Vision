using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.MySTNode.图片操作
{
    public partial class ComBoxCL : Form
    {

        private Point m_pt; // 弹出窗口的位置
        private int m_nWidth; // 弹出窗口的宽度
        private float m_scale; // 缩放比例
        private List<object> m_lst = new List<object>(); // 存储枚举值的列表

        private StringFormat m_sf; // 字符串格式化对象

       



        private bool m_bClosed; // 标记窗口是否已关闭

        public string SelectPreText { get;set; }

        /// <summary>
        /// 下拉框控件
        /// </summary>
        /// <param name="e">图像窗口列表</param>
        /// <param name="pt">位置</param>
        /// <param name="nWidth">宽</param>
        /// <param name="scale">缩放比例</param>
        /// <param name="pName">当前框中名</param>
        private string pName;
        public ComBoxCL(List<PictureBox> e, Point pt, int nWidth, float scale ,string pName)
        {
            // 设置控件样式，启用双缓冲等
            //双缓冲技术用于减少绘图时的闪烁和提高绘图性能。
            //启用这个样式可以在内存中创建一个缓冲区，绘制到这个缓冲区，
            //然后一次性将整个缓冲区的内容复制到屏幕上，减少闪烁。
            //true: 启用了上述样式，表示启用了优化的双缓冲
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            if (e != null)
            {
                // 初始化窗口属性
                foreach (var v in e)
                    m_lst.Add(v.Name); // 将枚举值添加到列表中

            }
            else m_lst.Add("null");
            if (this.SelectPreText == null) this.SelectPreText = pName;
            m_pt = pt; // 设置弹出窗口的位置
            m_scale = scale; // 设置缩放比例
            m_nWidth = nWidth; // 设置弹出窗口的宽度
            m_sf = new StringFormat();
            m_sf.LineAlignment = StringAlignment.Center;

            // 设置窗口样式
            this.ShowInTaskbar = false;
            this.BackColor = Color.FromArgb(255, 34, 34, 34);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.pName = pName;

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
            {
                this.SelectPreText = (String)m_lst[nIndex]; // 更新选定的枚举值
                
            }

            //MessageBox.Show(SelectPreText);
            this.DialogResult = System.Windows.Forms.DialogResult.OK; // 设置对话框结果为确认

            m_bClosed = true; // 标记窗口已关闭
        }



        Boolean isClose = false;
        protected override void OnMouseLeave(EventArgs e)
        {

            base.OnMouseLeave(e);
            if (m_bClosed) return;
            //this.DialogResult = System.Windows.Forms.DialogResult.None; // 设置对话框结果为无
            // 添加延迟关闭窗口，以确保文本修改生效
            Task.Delay(200).ContinueWith(_ =>
            {
                if (!m_bClosed && !isClose )
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        if (this.pName != null)
                        {
                            this.SelectPreText = this.pName;
                        }
                        this.Close(); // 关闭窗口
                        isClose = true;
                    }));
                }
            });
        }

       

    }
}
