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

namespace _305Vision.MySTNode.Fitting
{
    [STNode("/拟合", "在图像上画点")]
    public class LineFitting : ImageBaseNode
    {

        private STNodeOption ArrInputOption;
        private double xielvK;
        private double pointX;
        private double pointY;

        #region 拟合参数

        [STNodeProperty("点集", "点集")]
        public int[] Array { get; set; }
        [STNodeProperty("点个数", "点个数")]
        public int ArrayLength { get; set; }
        [STNodeProperty("抛弃点数量", "抛弃点数量")]
        public int Discard { get; set; }
        [STNodeProperty("直线斜率", "直线斜率")]
        public double XielvK { get => xielvK; set => xielvK = value; }
        [STNodeProperty("点的X坐标", "点的X坐标")]
        public double PointX { get => pointX; set => pointX = value; }
        [STNodeProperty("点的Y坐标", "点的Y坐标")]
        public double PointY { get => pointY; set => pointY = value; }

        //
        //public double XielvK { get; set; }
        //
        //public double PointX { get; set; }
        //
        //public double PointY { get; set; }

        #endregion

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "直线拟合";
            ArrInputOption = InputOptions.Add("点集", typeof(int[]), true);

            this.AutoSize = false;
            this.Height += 30;
            //var selectButton = new STNodeButton
            //{
            //    Text = "选取",
            //    Location = new Point(42, 130)
            //};
            PointX = 0; 
            PointY = 0;
            XielvK = 0;

            //this.Controls.Add(selectButton);
            inOption.DataTransfer += OpImgInDataTransfer;
            ArrInputOption.DataTransfer += ArrInputOption_DataTransfer;
            this.Invalidate();

        }

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
                Bitmap img = (Bitmap)e.TargetOption.Data;
                if (inOption.ConnectionCount != 0 && ArrInputOption.ConnectionCount!=0) 
                {
                    m_op_img_out.TransferData((Image)img);
                    isSecond = true;
                    ProcessImage(img);
                }
            }
        }

        private void ProcessImage(Bitmap img)
        {
            OpenCVSDK.lineFitting(Array, (int)Array.Length, (int)Discard,ref xielvK, ref pointX, ref pointY);

            //经过OpenCVSDK算法处理后，算出了圆心和半径
            // 在图像上绘制直线
            Bitmap newImage = DrawBll.DrawLineOnImage(img, PointX, PointY, XielvK);

            // 将新图像传递给输出
            m_img_draw = newImage;
            m_op_img_out.TransferData((Image)newImage);
            this.Invalidate();

        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            STNodeBLL.DrawBody(dt, m_img_draw, this.Left, this.Top + STNodeStyleSetting.COMMON_TOP);
        }

        


    }
}
