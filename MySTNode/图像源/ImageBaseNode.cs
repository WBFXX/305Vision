using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using NLog;
using System.Drawing.Drawing2D;
using _305Vision.Model;

namespace _305Vision.图片操作测试
{
    /// <summary>
    /// 图片节点基类 用于确定节点风格 标题颜色 以及 数据类型颜色
    /// </summary>
    public class ImageBaseNode : STNode
    {


        /// <summary>
        /// 需要作为显示绘制的图片
        /// </summary>
        protected Image m_img_draw;
        /// <summary>
        /// 输出节点
        /// </summary>
        protected STNodeOption m_op_img_out;
        protected STNodeOption inOption;
        protected Logger logger = LogManager.GetCurrentClassLogger();
        protected Boolean isSecond = false;


        protected override void OnCreate()
        {
            base.OnCreate();
            m_op_img_out = this.OutputOptions.Add("输出图像", typeof(Image), false);
            inOption = this.InputOptions.Add("输入图像", typeof(Image), true);

            this.AutoSize = false;          //此节点需要定制UI 所以无需AutoSize
                                            //this.Size = new Size(320,240);
            this.Width = 160;               //手动设置节点大小
            this.Height = 130;
            


            inOption.DisConnected += InOption_DisConnected;
        }

        protected override void OnOwnerChanged()
        {  //向编辑器提交数据类型颜色
            base.OnOwnerChanged();
            if (this.Owner == null) return;
            this.Owner.SetTypeColor(typeof(Image), Color.DarkCyan);
            this.Owner.SetTypeColor(typeof(int[]), Color.Aquamarine);
            this.Owner.SetTypeColor(typeof(CircleInfo), Color.BlueViolet);
            this.Owner.SetTypeColor(typeof(LineInfo), Color.CornflowerBlue);
        }

        /// <summary>
        /// 当输入被断开时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>

        protected virtual void InOption_DisConnected(object sender, STNodeOptionEventArgs e)
        {
            isSecond = false;
            this.Owner.Invalidate();
        }

        /// <summary>
        /// 重绘标题颜色
        /// </summary>
        /// <param name="dt"></param>
        protected override void OnDrawTitle(DrawingTools dt)
        {
            #region 改变颜色部分的代码
            Color _TitleColor;
            if (isSecond)
            {
                _TitleColor = Color.FromArgb(200, Color.DarkCyan);
            }
            else _TitleColor = Color.FromArgb(200, Color.Red);
            #endregion




            Color _ForeColor = this.ForeColor;
            int _Top = this.Top;
            int _Left = this.Left;
            int _TitleHeight = this.TitleHeight;
            string _Title = this.Title;
            bool _LockOption = this.LockOption;
            bool _LockLocation = this.LockLocation;
            Font _Font = this.Font;

            m_sf.Alignment = StringAlignment.Center;
            m_sf.LineAlignment = StringAlignment.Center;
            Graphics graphics = dt.Graphics;
            SolidBrush solidBrush = dt.SolidBrush;
            if (_TitleColor.A != 0)
            {
                solidBrush.Color = _TitleColor;
                graphics.FillRectangle(solidBrush, TitleRectangle);
            }
            if (_LockOption)
            {
                solidBrush.Color = _ForeColor;
                int num = _Top + _TitleHeight / 2 - 5;
                graphics.FillRectangle(dt.SolidBrush, _Left + 4, num, 2, 4);
                graphics.FillRectangle(dt.SolidBrush, _Left + 6, num, 2, 2);
                graphics.FillRectangle(dt.SolidBrush, _Left + 8, num, 2, 4);
                graphics.FillRectangle(dt.SolidBrush, _Left + 3, num + 4, 8, 6);
            }

            if (_LockLocation)
            {
                solidBrush.Color = _ForeColor;
                int num2 = _Top + _TitleHeight / 2 - 5;
                graphics.FillRectangle(solidBrush, Right - 9, num2, 4, 4);
                graphics.FillRectangle(solidBrush, Right - 11, num2 + 4, 8, 2);
                graphics.FillRectangle(solidBrush, Right - 8, num2 + 6, 2, 4);
            }

            if (!string.IsNullOrEmpty(_Title) && _ForeColor.A != 0)
            {
                solidBrush.Color = _ForeColor;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawString(_Title, _Font, solidBrush, TitleRectangle, m_sf);
            }
        }


    }
}

