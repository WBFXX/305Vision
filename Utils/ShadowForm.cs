using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace _305Vision.Utils

{
    public partial class ShadowForm : Form
    {
        private Bitmap shadowBitmap;
        private int shadowOpacity = 60;
        private int shadowSpread = 6;
        private int HitSplit = 4;  //鼠标拖动窗体大小时判断可操作范围
        private Rectangle RecInner = Rectangle.Empty;
        private Rectangle RecBorderLeft = Rectangle.Empty;
        private Rectangle RecBorderLeftTop = Rectangle.Empty;
        private Rectangle RecBorderLeftBottom = Rectangle.Empty;
        private Rectangle RecBorderRight = Rectangle.Empty;
        private Rectangle RecBorderRightTop = Rectangle.Empty;
        private Rectangle RecBorderRightBottom = Rectangle.Empty;
        private Rectangle RecBorderTop = Rectangle.Empty;
        private Rectangle RecBorderBottom = Rectangle.Empty;

        #region 属性
        /// <summary>
        /// 阴影图片
        /// </summary>
        public Bitmap ShadowBitmap
        {
            get { return shadowBitmap; }
            set
            {
                shadowBitmap = value;
                SetBitmap(shadowBitmap);
            }
        }

        /// <summary>
        /// 阴影颜色
        /// </summary>
        public Color ShadowColor { get; set; } = Color.Black;

        /// <summary>
        /// 阴影透明度
        /// </summary>
        public int ShadowOpacity
        {
            get { return shadowOpacity; }
            set
            {
                shadowOpacity = Math.Max(Math.Min(value, 255), 0);
                SetBitmap(ShadowBitmap);
            }
        }

        /// <summary>
        /// 阴影横向偏移值
        /// </summary>
        public int ShadowH { get; set; } = 0;

        /// <summary>
        /// 阴影纵向偏移值
        /// </summary>
        public int ShadowV { get; set; } = 0;

        /// <summary>
        /// 阴影模糊值
        /// </summary>
        public int ShadowBlur { get; set; } = 0;

        /// <summary>
        /// 阴影扩展值
        /// </summary>
        public int ShadowSpread
        {
            get { return shadowSpread; }
            set
            {
                shadowSpread = Math.Max(value, 0);
            }
        }

        /// <summary>
        /// 窗体圆角度
        /// </summary>
        public int CornerRound { get; set; } = 0;

        /// <summary>
        /// 主子窗体X方向偏移量
        /// </summary>
        public int OffsetX
        {
            get { return (ShadowH > 0 ? 0 : ShadowH) - ShadowSpread; }
        }

        /// <summary>
        /// 主子窗体Y方向偏移量
        /// </summary>
        public int OffsetY
        {
            get { return (ShadowV > 0 ? 0 : ShadowV) - ShadowSpread; }
        }

        public bool CanResize { get; set; } = true;

        #endregion

        //public ShadowForm(fmBase master)
        //{
        //    Owner = master; //设置主子窗口  

        //    #region 设置影子窗口的属性参数
        //    AutoScaleMode = AutoScaleMode.None;
        //    FormBorderStyle = FormBorderStyle.None;
        //    MaximizeBox = false;
        //    MinimizeBox = false;
        //    this.Name = "ShadowForm";
        //    ShowInTaskbar = false;
        //    #endregion

        //    #region 减少闪烁
        //    this.DoubleBuffered = true;
        //    SetStyle(ControlStyles.UserPaint |
        //        ControlStyles.AllPaintingInWmPaint |
        //        ControlStyles.OptimizedDoubleBuffer |
        //        ControlStyles.ResizeRedraw |
        //        ControlStyles.DoubleBuffer, true);
        //    //强制分配样式重新应用到控件上
        //    UpdateStyles();
        //    #endregion

        //    if (Owner != null)
        //    {
        //        //跟随主子移动位置
        //        Owner.LocationChanged += (sender, e) =>
        //        {
        //            Shadow_LocationChanged(sender, e);
        //        };

        //        //主子关闭时，跟着关闭并且释放
        //        Owner.FormClosed += (sender, e) =>
        //        {
        //            Close();
        //            this.Dispose();
        //        };

        //        //窗体移动
        //        Owner.MouseMove += (sender, e) =>
        //        {
        //            if (e.Button == MouseButtons.Left)
        //            {
        //                ReleaseCapture();
        //                SendMessage(Owner.Handle, WM_SYSCOMMAND, new IntPtr(0xF010 + HTCAPTION), new IntPtr(0));
        //            }
        //        };

        //        //窗体激活时，让主子窗体和影子窗体实现形影不离（多个窗口重叠交叉显示时尤为重要）
        //        Owner.Activated += FormActivated;
        //        this.Activated += FormActivated;
        //    }
        //}

        private void FormActivated(object sender, EventArgs e)
        {
            if (Owner == null) return;
            IntPtr CurWinHandle = GetForegroundWindow();
            IntPtr NextWinHandle = GetWindow(CurWinHandle, GW_HWNDNEXT);

            if (CurWinHandle == this.Handle)
            {
                if (NextWinHandle == Owner.Handle) return;
                IntPtr other = FindOther(CurWinHandle, Owner.Handle);
                if (other != IntPtr.Zero)
                {
                    SetForegroundWindow(Owner.Handle);
                }
                return;
            }

            if (CurWinHandle == Owner.Handle)
            {
                if (NextWinHandle == this.Handle) return;
                IntPtr other = FindOther(CurWinHandle, this.Handle);
                if (other != IntPtr.Zero)
                {
                    SetForegroundWindow(this.Handle);
                }
                return;
            }
        }

        private IntPtr FindOther(IntPtr win, IntPtr other)
        {
            IntPtr ptr = GetWindow(win, GW_HWNDNEXT);
            while (ptr != IntPtr.Zero)
            {
                if (ptr == other)
                {
                    return ptr;
                }
                ptr = GetWindow(ptr, GW_HWNDNEXT);
            }
            return IntPtr.Zero;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED;
                return cp;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        public void Shadow_LocationChanged(Object sender, EventArgs eventArgs)
        {
            Point pos = Owner.Location;
            pos.Offset(OffsetX, OffsetY);
            Location = pos;
        }

        private void Shadow_SizeChanged(object sender, EventArgs e)
        {
            Point pos = Location;
            pos.Offset(-OffsetX, -OffsetY);
            Owner.Location = pos;
            Size size = new Size(this.Width - (ShadowSpread + ShadowBlur + CornerRound) * 2, this.Height - (ShadowSpread + ShadowBlur + CornerRound) * 2);
            Owner.Size = size;
            RefreshShadow(true, true);
        }

        /// <summary>
        ///  重绘阴影
        /// </summary>
        /// <param name="redraw">是否重绘</param>
        /// <param name="reLoaction">是否重定位</param>
        public void RefreshShadow(bool redraw = true, bool reLoaction = true)
        {
            this.SizeChanged -= Shadow_SizeChanged;
            this.Size = new Size(Owner.Width + ShadowSpread * 2 + Math.Abs(ShadowH), Owner.Height + ShadowSpread * 2 + Math.Abs(ShadowV));
            if (redraw)
            {
                ShadowBitmap = DrawShadowBitmap();
            }

            if (reLoaction)
            {
                Shadow_LocationChanged(null, null);
            }

            // 设置显示区域
            Region r = new Region(this.ClientRectangle);  //(1, 1, Width - 1, Height - 1));
            if (CornerRound > 0)
            {
                r = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, ShadowSpread + CornerRound, ShadowSpread + CornerRound));
            }
            Region or;
            if (Owner.Region == null)
                or = new Region(RecInner);
            else
                or = Owner.Region.Clone();
            r.Exclude(or);
            Region = r;
            Owner.Refresh();
            this.SizeChanged += Shadow_SizeChanged;
        }

        #region 绘制阴影图像
        private Bitmap DrawShadowBitmap()
        {
            var bitmap = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bitmap);

            //必要设置，当ShadowSpread设置为1或2时，需要高质量绘制才能显示
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            #region 计算边界 
            RecInner = new Rectangle(
                ShadowSpread - (ShadowH < 0 ? ShadowH : 0),
                ShadowSpread - (ShadowV < 0 ? ShadowV : 0),
                Owner.Width,
                Owner.Height);
            RecBorderLeft = new Rectangle(
                Math.Max(RecInner.X - 2, 0),
                RecInner.Y,
                2,
                RecInner.Height);
            RecBorderLeftTop = new Rectangle(
                Math.Max(RecInner.X - 2, 0),
                Math.Max(RecInner.Y - 2, 0),
                2,
                2);
            RecBorderLeftBottom = new Rectangle(
                Math.Max(RecInner.X - 2, 0),
                RecInner.Bottom,
                2,
                2);
            RecBorderTop = new Rectangle(
                RecInner.X,
                Math.Max(RecInner.Y - 2, 0),
                RecInner.Width,
                2);
            RecBorderRight = new Rectangle(
                RecInner.Right,
                RecInner.Y,
                2,
                RecInner.Height);
            RecBorderRightTop = new Rectangle(
                RecInner.Right,
                Math.Max(RecInner.Y - 2, 0),
                2,
                2);
            RecBorderRightBottom = new Rectangle(
                RecInner.Right,
                RecInner.Bottom,
                2,
                2);
            RecBorderBottom = new Rectangle(
                RecInner.X,
                RecInner.Bottom,
                RecInner.Width,
                2);

            /*   g.FillRectangle(Brushes.Yellow, 0, 0, this.Width, this.Height);
               g.FillRectangle(Brushes.Green, RecInner);
               Brush br = new SolidBrush(Color.FromArgb(1, Color.Gray));
               g.FillRectangle(br, RecBorderLeft);
               g.FillRectangle(br, RecBorderTop);
               g.FillRectangle(br, RecBorderLeftTop);
               g.FillRectangle(br, RecBorderLeftBottom);
               g.FillRectangle(br, RecBorderRight);
               g.FillRectangle(br, RecBorderRightTop);
               g.FillRectangle(br, RecBorderRightBottom);
               g.FillRectangle(br, RecBorderBottom);*/
            #endregion

            if (ShadowSpread > 0)
            {
                #region 计算四边矩形
                Rectangle RecLeft = new Rectangle(0, ShadowSpread, ShadowSpread, Owner.Height);
                Rectangle RecTop = new Rectangle(ShadowSpread, 0, Owner.Width, ShadowSpread);
                Rectangle RecRight = new Rectangle(Owner.Width + RecLeft.Right, ShadowSpread, ShadowSpread, Owner.Height);
                Rectangle RecBottom = new Rectangle(ShadowSpread, Owner.Height + RecTop.Bottom, Owner.Width, ShadowSpread);

                #region 方案一：整个阴影部分根据ShadowH、ShadowV平移
                /* if (ShadowH > 0)
                 {
                     RecLeft.Offset(ShadowH, 0);
                     RecTop.Offset(ShadowH, 0);
                     RecBottom.Offset(ShadowH, 0);
                     RecLeft.Width = ShadowSpread - ShadowH;
                     RecRight.Width += ShadowH;
                     RecTop.Width -= ShadowH;
                     RecBottom.Width -= ShadowH;
                 }
                 else if (ShadowH < 0)
                 {
                     RecTop.Offset(-ShadowH, 0);
                     RecRight.Offset(ShadowH, 0);
                     RecBottom.Offset(-ShadowH, 0);
                     RecLeft.Width -= ShadowH;
                     RecTop.Width += ShadowH * 2;
                     RecRight.Width = ShadowSpread + ShadowH;
                     RecBottom.Width += ShadowH * 2;
                 }
                 if (ShadowV > 0)
                 {
                     RecLeft.Offset(0, ShadowV);
                     RecTop.Offset(0, ShadowV);
                     RecRight.Offset(0, ShadowV);
                     RecTop.Height = ShadowSpread - ShadowV;
                     RecBottom.Height += ShadowV;
                     RecLeft.Height -= ShadowV;
                     RecRight.Height -= ShadowV;
                 }
                 else if (ShadowV < 0)
                 {
                     RecLeft.Offset(0, -ShadowV);
                     RecRight.Offset(0, -ShadowV);
                     RecBottom.Offset(0, ShadowV);
                     RecTop.Height -= ShadowV;
                     RecLeft.Height += ShadowV * 2;
                     RecRight.Height += ShadowV * 2;
                     RecBottom.Height = ShadowSpread + ShadowV;
                 }*/
                #endregion

                #region 方案二：四周阴影部分不变，根据ShadowH、ShadowV延伸阴影
                if (ShadowH > 0)
                {
                    RecRight.Width += ShadowH;
                }
                else if (ShadowH < 0)
                {
                    RecTop.Offset(-ShadowH, 0);
                    RecBottom.Offset(-ShadowH, 0);
                    RecRight.Offset(-ShadowH, 0);
                    RecLeft.Width -= ShadowH;
                }

                if (ShadowV > 0)
                {
                    RecBottom.Height += ShadowV;
                }
                else if (ShadowV < 0)
                {
                    RecLeft.Offset(0, -ShadowV);
                    RecRight.Offset(0, -ShadowV);
                    RecBottom.Offset(0, -ShadowV);
                    RecTop.Height -= ShadowV;
                }
                #endregion

                /*g.FillRectangle(Brushes.Black, RecLeft);
                g.FillRectangle(Brushes.Black, RecTop);
                g.FillRectangle(Brushes.Black, RecRight);
                g.FillRectangle(Brushes.Black, RecBottom);  */
                #endregion

                #region 绘制四条边 
                LinearGradientBrush brush;
                // left 
                if (RecLeft.Width > 0)
                {
                    brush = new LinearGradientBrush(new Point(RecLeft.X, RecLeft.Y), new Point(RecLeft.Right, RecLeft.Y), Color.Transparent, ShadowColor);
                    g.FillRectangle(brush, RecLeft);
                }

                // top 
                if (RecTop.Height > 0)
                {
                    // brush = new LinearGradientBrush(new Point(RecTop.X, RecTop.Bottom), new Point(RecTop.X, RecTop.Y), ShadowColor, Color.Transparent);
                    brush = new LinearGradientBrush(new Point(RecTop.X, RecTop.Y), new Point(RecTop.X, RecTop.Bottom), Color.Transparent, ShadowColor);
                    g.FillRectangle(brush, RecTop);
                    //  g.FillRectangle(Brushes.Red, RecTop);
                }

                // right  
                if (RecRight.Width > 0)
                {
                    brush = new LinearGradientBrush(new Point(RecRight.X, RecRight.Y), new Point(RecRight.Right, RecRight.Y), ShadowColor, Color.Transparent);
                    g.FillRectangle(brush, RecRight);
                }

                // down  
                if (RecBottom.Height > 0)
                {
                    brush = new LinearGradientBrush(new Point(RecBottom.X, RecBottom.Y), new Point(RecBottom.X, RecBottom.Bottom), ShadowColor, Color.Transparent);
                    g.FillRectangle(brush, RecBottom);
                }
                #endregion

                #region  绘制四个角 
                // lt
                FillPie(g, new Rectangle(RecLeft.Left, RecTop.Top, RecLeft.Width * 2, RecTop.Height * 2), 180, 90);

                // rt
                FillPie(g, new Rectangle(RecRight.Left - RecRight.Width, RecTop.Top, RecRight.Width * 2, RecTop.Height * 2), 270, 90);

                // rb
                FillPie(g, new Rectangle(RecRight.Left - RecRight.Width, RecBottom.Top - RecBottom.Height, RecRight.Width * 2, RecBottom.Height * 2), 0, 90);

                // lb
                FillPie(g, new Rectangle(RecLeft.Left, RecBottom.Top - RecBottom.Height, RecLeft.Width * 2, RecBottom.Height * 2), 90, 90);
                #endregion
            }
            return bitmap;
        }

        private void FillPie(Graphics g, Rectangle rec, int startAngle, int sweepAngle)
        {
            if ((rec.Width <= 0) || (rec.Height < 0)) return;
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(rec);
            PathGradientBrush pgb = new PathGradientBrush(gp);
            pgb.CenterColor = ShadowColor;
            pgb.SurroundColors = new[] { Color.Transparent };
            pgb.CenterPoint = new PointF(rec.Left + rec.Width / 2, rec.Top + rec.Height / 2);
            //g.FillPie(Brushes.Black, rec, 0, 360);
            g.FillPie(pgb, rec, startAngle, sweepAngle);
        }
        #endregion

        #region 绘制圆角
        public static void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.DrawPath(pen, path);
            }
        }

        public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }

        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }
        #endregion

        #region 设置图片透明度
        public void SetBitmap(Bitmap bitmap)
        {
            if (bitmap == null) return;
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb) return;
            IntPtr screenDc = GetDC(IntPtr.Zero);
            IntPtr memDc = CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;
            try
            {
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBitmap = SelectObject(memDc, hBitmap);
                var size = new Size(bitmap.Width, bitmap.Height);
                var pointSource = new Point(0, 0);
                var topPos = new Point(Left, Top);
                var blend = new BLENDFUNCTION();
                blend.BlendOp = AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = (byte)ShadowOpacity;
                blend.AlphaFormat = AC_SRC_ALPHA;
                UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, ULW_ALPHA);
            }
            finally
            {
                ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    SelectObject(memDc, oldBitmap);
                    DeleteObject(hBitmap);
                }
                DeleteDC(memDc);
            }
        }
        #endregion

        #region 拖动无边框窗体 改变无边框窗体尺寸  
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    if ((Owner != null) && (CanResize))
                    {
                        Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);
                        base.WndProc(ref m);
                        if (RecBorderLeft.Contains(vPoint))                 //左
                            m.Result = (IntPtr)HIT_LEFT;
                        else if (RecBorderLeftTop.Contains(vPoint))         //左上
                            m.Result = (IntPtr)HIT_TOPLEFT;
                        else if (RecBorderLeftBottom.Contains(vPoint))      //左下
                            m.Result = (IntPtr)HIT_BOTTOMLEFT;
                        else if (RecBorderTop.Contains(vPoint))             //上
                            m.Result = (IntPtr)HIT_TOP;
                        else if (RecBorderRight.Contains(vPoint))           //右
                            m.Result = (IntPtr)HIT_RIGHT;
                        else if (RecBorderRightTop.Contains(vPoint))        //右上
                            m.Result = (IntPtr)HIT_TOPRIGHT;
                        else if (RecBorderRightBottom.Contains(vPoint))     //右下
                            m.Result = (IntPtr)HIT_BOTTOMRIGHT;
                        else if (RecBorderBottom.Contains(vPoint))          //下
                            m.Result = (IntPtr)HIT_BOTTOM;
                        return;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region 引用系统函数
        const int WS_EX_LAYERED = 0x00080000;
        const int WM_NCHITTEST = 0x0084;
        const int HIT_LEFT = 10;
        const int HIT_RIGHT = 11;
        const int HIT_TOP = 12;
        const int HIT_TOPLEFT = 13;
        const int HIT_TOPRIGHT = 14;
        const int HIT_BOTTOM = 15;
        const int HIT_BOTTOMLEFT = 16;
        const int HIT_BOTTOMRIGHT = 17;
        const int AC_SRC_OVER = 0x00;
        const int AC_SRC_ALPHA = 0x01;
        const int ULW_ALPHA = 0x00000002;
        const int GW_HWNDNEXT = 2;
        const int WM_SYSCOMMAND = 0x0112;
        const int HTCAPTION = 2;

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize,
            IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("user32.dll", ExactSpelling = true)]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        static extern Bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetWindow(IntPtr hWnd, uint wCmd);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.dll")]
        static extern bool ReleaseCapture();
        [DllImport("USER32.DLL", EntryPoint = "SendMessage")]
        static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        public enum Bool
        {
            False = 0,
            True
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }
        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ShadowForm
            // 
            this.ClientSize = new System.Drawing.Size(644, 587);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShadowForm";
            this.ResumeLayout(false);

        }
    }

}
