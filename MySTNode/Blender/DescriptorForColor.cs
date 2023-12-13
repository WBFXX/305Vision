using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision
{
    /// <summary>
    /// 因为属性编辑器默认并不支持Color类型数据 所以这里重写一个描述器
    /// </summary>
    public class DescriptorForColor : STNodePropertyDescriptor
    {
        private Rectangle m_rect;//此区域用作 属性窗口上绘制颜色预览
        //当此属性在属性窗口中被确定位置时候发生
        protected override void OnSetItemLocation()
        {
            base.OnSetItemLocation();
            Rectangle rect = base.RectangleR;
            m_rect = new Rectangle(rect.Right - 25, rect.Top + 5, 19, 12);
        }
        //将属性值转换为字符串 属性窗口值绘制时将采用此字符串
        protected override string GetStringFromValue()
        {
            Color clr = (Color)this.GetValue(null);
            return clr.A + "," + clr.R + "," + clr.G + "," + clr.B;
        }
        //将属性窗口中输入的字符串转化为Color属性 当属性窗口中用户确认输入时调用
        protected override object GetValueFromString(string strText)
        {
            string[] strClr = strText.Split(',');
            return Color.FromArgb(
                int.Parse(strClr[0]),   //A
                int.Parse(strClr[1]),   //R
                int.Parse(strClr[2]),   //G
                int.Parse(strClr[3]));  //B
        }
        //绘制属性窗口值区域时候调用
        protected override void OnDrawValueRectangle(DrawingTools dt)
        {
            base.OnDrawValueRectangle(dt);//先采用默认的绘制 并再绘制颜色预览
            dt.SolidBrush.Color = (Color)this.GetValue(null);
            dt.Graphics.FillRectangle(dt.SolidBrush, m_rect);//填充颜色
            dt.Graphics.DrawRectangle(Pens.Black, m_rect);   //绘制边框
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //如果用户点击在 颜色预览区域 则弹出系统颜色对话框
            if (m_rect.Contains(e.Location))
            {
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() != DialogResult.OK) return;
                this.SetValue(cd.Color, null);
                this.Invalidate();
                return;
            }
            //否则其他区域将采用默认处理方式 弹出字符串输入框
            base.OnMouseClick(e);
        }
    }
}
