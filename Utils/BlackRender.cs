using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.Utils
{
    /// <summary>
    /// menuStrip1按钮背景框渲染
    /// </summary>
    public class BlackRender : ToolStripProfessionalRenderer
    {
        //Font textFont = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private Color menuItemSelectedColor = Color.Gray;
        private Color menuItemBorderColor = Color.Black;
        /// <summary>
        /// 初始化一个新的 BlackRender 实例
        /// </summary>
        public BlackRender()
            : base(new MenuBarColor())
        {
            // 设置选项卡颜色和选项卡边框颜色
            this.menuItemSelectedColor = Color.FromArgb(61,61,61);//选项卡颜色
            this.menuItemBorderColor = Color.FromArgb(112,112,112);//选项卡边框颜色

        }



        /// <summary>
        /// 修改字体
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            //e.TextFont = textFont;

            base.OnRenderItemText(e);
        }

        #region Backgrounds
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.IsOnDropDown)
            {
                if (e.Item.Selected == true && e.Item.Enabled)
                {
                    
                    DrawMenuDropDownItemHighlight(e);
                }

            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;//抗锯齿

            using (SolidBrush sb = new SolidBrush(Color.FromArgb(45,45,48)))
            {
                e.Graphics.FillRectangle(sb, e.AffectedBounds);
            }

            //base.OnRenderToolStripBackground(e);
        }
        ///+圆角
        //protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        //{
        //    ToolStrip toolStrip = e.ToolStrip;
        //    Graphics g = e.Graphics;
        //    g.SmoothingMode = SmoothingMode.HighQuality;//抗锯齿
        //    Rectangle bounds = e.AffectedBounds;



        //    //下拉框渐变色
        //    LinearGradientBrush lgbrush = new LinearGradientBrush(new Point(0, 0), new Point(0, toolStrip.Height), Color.FromArgb(200, Color.FromArgb(46,46,46)), Color.FromArgb(200, Color.FromArgb(46,46,46)));
        //    if (toolStrip is MenuStrip)
        //    {
        //        //由menuStrip的Paint方法定义 这里不做操作

        //    }
        //    else if (toolStrip is ToolStripDropDown)
        //    {
        //        //圆角 圆角直径
        //        int diameter = 1;//直径
        //        GraphicsPath path = new GraphicsPath();
        //        Rectangle rect = new Rectangle(Point.Empty, toolStrip.Size);
        //        Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));

        //        path.AddLine(0, 0, 10, 0);
        //        // 右上角
        //        arcRect.X = rect.Right - diameter;
        //        path.AddArc(arcRect, 270, 90);

        //        // 右下角
        //        arcRect.Y = rect.Bottom - diameter;
        //        path.AddArc(arcRect, 0, 90);

        //        // 左下角
        //        arcRect.X = rect.Left;
        //        path.AddArc(arcRect, 90, 90);
        //        path.CloseFigure();
        //        toolStrip.Region = new Region(path);
        //        g.FillPath(lgbrush, path);
        //    }
        //    else
        //    {
        //        base.OnRenderToolStripBackground(e);
        //    }
        //}

        /// <summary>
        /// 修改下拉菜单和文件按钮之间有一条白色的横线（苦恼的地方）
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // 取消基类的边框绘制
            // base.OnRenderToolStripBorder(e);

            // 绘制透明边框或与背景相同颜色的边框
            using (Pen pen = new Pen(Color.FromArgb(45, 45, 48), 1))  // 使用与背景相同的颜色，也可以设置为Color.Transparent
            {
                e.Graphics.DrawRectangle(pen, e.AffectedBounds.Left, e.AffectedBounds.Top, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1);
            }
        }


        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
             //base.OnRenderImageMargin(e);
            
        }
        #endregion
        #region DrawMenuDropDownItemHighlight
        private void DrawMenuDropDownItemHighlight(ToolStripItemRenderEventArgs e)
        {

            Rectangle rect = new Rectangle();
            rect = new Rectangle(2, 0, (int)e.Graphics.VisibleClipBounds.Width - 4, (int)e.Graphics.VisibleClipBounds.Height - 1);
            using (SolidBrush brush = new SolidBrush(menuItemSelectedColor))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            using (Pen pen = new Pen(menuItemBorderColor))
            {
                e.Graphics.DrawRectangle(pen, rect);
            }
        }
        #endregion
    }
    public class MenuBarColor : ProfessionalColorTable
    {
        Color ManuBarCommonColor = Color.FromArgb(61, 61, 61);
        //Color SelectedHighlightColor = Color.Red;
        Color MenuBorderColor = Color.FromArgb(100,100,100);//点开后所有边框颜色
        Color MenuItemBorderColor = Color.FromArgb(112,112,112);//鼠标放到按钮上的边框颜色
        Color MenuStripbg = Color.FromArgb(112, 112, 112);//按下栏中按钮后改变的底色
        /// <summary>
        /// 初始化一个新的 MenuBarColor 实例
        /// </summary>
        public MenuBarColor()
        {
        }


        #region
        ///// <summary>
        /////  获取选中除顶级 System.Windows.Forms.ToolStripMenuItem 之外的 System.Windows.Forms.ToolStripMenuItem 时，要使用的纯色。
        ///// </summary>
        //public override Color MenuItemSelected
        //{
        //    get
        //    {
        //        return SelectedHighlightColor;
        //    }
        //}
        /// <summary>
        ///  被按下时使用的渐变的开始颜色
        /// </summary>
        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return MenuStripbg;
            }
        }
        /// <summary>
        /// 被按下时使用的渐变的结束颜色
        /// </summary>
        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return MenuStripbg;
            }
        }
        /// <summary>
        /// 被按下时使用的渐变的中间颜色
        /// </summary>
        public override Color MenuItemPressedGradientMiddle
        {
            get
            {
                return MenuStripbg;
            }
        }
        /// <summary>
        /// 获取按钮被选定时使用的纯色
        /// </summary>
        public override Color ButtonSelectedHighlight
        {
            get
            {
                return ManuBarCommonColor;

            }
        }
        /// <summary>
        ///  被选定时使用的渐变的开始颜色
        /// </summary>
        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return ManuBarCommonColor;
            }
        }
        /// <summary>
        /// 被选定时使用的渐变的结束颜色
        /// </summary>
        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return ManuBarCommonColor;
            }
        }
        /// <summary>
        /// MenuStrip 上使用的边框颜色
        /// </summary>
        public override Color MenuBorder
        {
            get
            {
                return MenuBorderColor;
            }
        }
        /// <summary>
        /// ToolStripMenuItem 的边框颜色
        /// </summary>
        public override Color MenuItemBorder
        {
            get
            {
                return MenuItemBorderColor;
            }
        }
        #endregion
    }
}
