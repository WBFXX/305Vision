using _305Vision.BLL;
using _305Vision.OWindows;
using _305Vision.Model;
using _305Vision.SDK;
using NLog;
using ST.Library.UI.NodeEditor;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using _305Vision.MySTNode.控件库;
using _305Vision.图片操作测试;
using System.Drawing.Drawing2D;
using Newtonsoft.Json.Linq;
using _305Vision.Common;
using System.Data.SqlTypes;
using System.IO;

namespace _305Vision.MySTNode.Fitting
{
    [STNode("/拟合", "在图像上画点")]
    public class LineFitting : ImageBaseNode
    {

        private STNodeOption ArrInputOption;
        private STNodeOption LineArray_OutPut;
        private LineInfo lineInfo;
        private double xielvK;
        private double pointX;
        private double pointY;
        private int sizeOfSave;
        private int sizeOfThrow;

        #region 拟合参数

        [STNodeProperty("点集", "点集")]
        public int[] Array { get; set; }
        [STNodeProperty("点个数", "点个数")]
        public int ArrayLength { get; set; }
        [STNodeProperty("抛弃点数量", "抛弃点数量")]
        public int Discard { get; set; }
        [STNodeProperty("直线长度", "直线长度")]
        public int LineLength { get; set; }
        [STNodeProperty("直线斜率", "直线斜率")]
        public double XielvK { get => xielvK; set => xielvK = value; }
        [STNodeProperty("点的X坐标", "点的X坐标")]
        public double PointX { get => pointX; set => pointX = value; }
        [STNodeProperty("点的Y坐标", "点的Y坐标")]
        public double PointY { get => pointY; set => pointY = value; }
        [STNodeProperty("保留点的集", "点集")]
        public int[] ArrayOfSave { get; set; }
        [STNodeProperty("保留点集大小", "点集")]
        public int SizeOfSave { get => sizeOfSave; set => sizeOfSave = value; }
        [STNodeProperty("抛弃点的点集", "点集")]
        public int[] ArrayOfThrow { get; set; }
        [STNodeProperty("抛弃点集大小", "点集")]
        public int SizeOfThrow { get => sizeOfThrow; set => sizeOfThrow = value; }

        #endregion

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "直线拟合";
            ArrInputOption = InputOptions.Add("点集", typeof(int[]), true);
            LineArray_OutPut = OutputOptions.Add("输出直线", typeof(LineInfo), false);

            this.AutoSize = false;
            this.Height += 30;
            //var selectButton = new STNodeButton
            //{
            //    Text = "选取",
            //    Location = new Point(42, 130)
            //};
            Discard = 1;
            SizeOfSave = 0;
            SizeOfThrow = 0;
            PointX = 0; 
            PointY = 0;
            XielvK = 0;
            LineLength = 500;

            //this.Controls.Add(selectButton);
            inOption.DataTransfer += OpImgInDataTransfer;
            ArrInputOption.DataTransfer += ArrInputOption_DataTransfer;
            this.Invalidate();

        }

        private Bitmap midImg;//数据传输的先后顺序，先执行的是OpImgInDataTransfer 再执行ArrInputOption_DataTransfer 所以要后处理ProcessImage(midImg
        private void ArrInputOption_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {
                m_op_img_out.TransferData(null);
                m_img_draw = null;
            }
            else
            {
                Array = (int[])e.TargetOption.Data;
                ArrayLength = Array.Length/2;

                if (inOption.ConnectionCount != 0 && ArrInputOption.ConnectionCount != 0)
                {
                    m_op_img_out.TransferData((Image)midImg);
                    isSecond = true;
                    ProcessImage(midImg);
                }
            }
        }
            private void OpImgInDataTransfer(object sender, STNodeOptionEventArgs e)
        {
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {
                m_op_img_out.TransferData(null);
                m_img_draw = null;
            }
            else
            {
                midImg = (Bitmap)e.TargetOption.Data;

            }
        }

        private void ProcessImage(Bitmap img)
        {
            IntPtr intPtrOfSave = IntPtr.Zero;
            IntPtr intPtrOfThrow = IntPtr.Zero;
            OpenCVSDK.lineFitting(Array, (int)Array.Length, (int)Discard,ref xielvK, ref pointX, ref pointY,ref intPtrOfSave ,ref sizeOfSave,ref intPtrOfThrow,ref sizeOfThrow);
            //读取点集
            this.ArrayOfSave = UtilsBLL.ReadIntPtrToArray(intPtrOfSave, sizeOfSave);
            this.ArrayOfThrow= UtilsBLL.ReadIntPtrToArray(intPtrOfThrow, sizeOfThrow);
            //经过OpenCVSDK算法处理后，算出了圆心和半径
            //在图像上绘制直线
            lineInfo = new LineInfo
            {
                PointOnLine = new Point((int)PointX, (int)PointY),
                Slope = XielvK,
            };
            Bitmap newImage = DrawBll.DrawLineOnImage(img, lineInfo, LineLength) ;

            // 将新图像传递给输出
            m_img_draw = newImage;
            LineArray_OutPut.TransferData(lineInfo);
            m_op_img_out.TransferData((Image)newImage);
            this.Invalidate();

        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            STNodeBLL.DrawBody(dt, m_img_draw, this.Left, this.Top + CommonStyleSetting.COMMON_TOP);
        }

        


    }
}
