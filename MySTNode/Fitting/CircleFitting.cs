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
using System.Threading;

namespace _305Vision.MySTNode.Fitting
{
    [STNode("/拟合", "在图像上画点")]
    public class CircleFitting : ImageBaseNode
    {

        private STNodeOption ArrInputOption;
        private STNodeOption CircleInfo_OutPut;
        private double jieRadis;
        private double centerX;
        private double centerY;
        private CircleInfo circleInfo;
        private int sizeOfSave;
        private int sizeOfThrow;

        #region 拟合参数

        [STNodeProperty("点集", "点集")]
        public int[] Array { get; set; }
        [STNodeProperty("点个数", "点个数")]
        public int ArrayLength { get; set; }
        [STNodeProperty("抛弃点数量", "抛弃点数量")]
        public int Discard { get; set; }
        [STNodeProperty("拟合半径", "拟合圆半径")]
        public double JieRadis { get => jieRadis; set => jieRadis = value; }
        [STNodeProperty("圆心X", "圆心X")]
        public double CenterX { get => centerX; set => centerX = value; }
        [STNodeProperty("圆心Y", "圆心Y")]
        public double CenterY { get => centerY; set => centerY = value; }
        [STNodeProperty("保留点的集", "点集")]
        public int[] ArrayOfSave { get; set; }
        [STNodeProperty("保留点集大小", "点集")]
        public int SizeOfSave { get => sizeOfSave; set => sizeOfSave = value; }
        [STNodeProperty("抛弃点的点集", "点集")]
        public int[] ArrayOfThrow { get; set; }
        [STNodeProperty("抛弃点集大小", "点集")]
        public int SizeOfThrow { get => sizeOfThrow; set => sizeOfThrow = value; }

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
            this.Title = "圆形拟合";
            ArrInputOption = InputOptions.Add("点集", typeof(int[]), true);
            CircleInfo_OutPut = OutputOptions.Add("输出圆信息", typeof(CircleInfo), false);

            this.AutoSize = false;
            this.Height += 50;
            //var selectButton = new STNodeButton
            //{
            //    Text = "选取",
            //    Location = new Point(42, 110+STNodeStyleSetting.COMMON_TOP)
            //};
            SizeOfSave = 0;
            SizeOfThrow = 0;
            CenterX = 0; 
            CenterY = 0;
            JieRadis = 0;

            //this.Controls.Add(selectButton);
            
            ArrInputOption.DataTransfer += ArrInputOption_DataTransfer;
            inOption.DataTransfer += OpImgInDataTransfer;
            this.Invalidate();

        }
        private Bitmap midImg;//数据传输的先后顺序，先执行的是OpImgInDataTransfer 再执行ArrInputOption_DataTransfer 所以要后处理ProcessImage(midImg);
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
            try
            {
                OpenCVSDK.circleFitting(Array, (int)Array.Length, (int)Discard, ref jieRadis, ref centerX, ref centerY, ref intPtrOfSave, ref sizeOfSave, ref intPtrOfThrow, ref sizeOfThrow);
                logger.Info("cccc半径：" + jieRadis);
                logger.Info("cccc圆心X：" + centerX);
            }
            catch (Exception ex)
            {
                MessageBox.Show("圆形拟合失败：" + ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            //读取点集
            this.ArrayOfSave = UtilsBLL.ReadIntPtrToArray(intPtrOfSave, sizeOfSave);
            this.ArrayOfThrow = UtilsBLL.ReadIntPtrToArray(intPtrOfThrow, sizeOfThrow);

            //经过OpenCVSDK算法处理后，算出了圆心和半径
            // 在图像上绘制圆
            //把图像信息放到结构体里
            circleInfo.Center = new Point((int)CenterX, (int)CenterY);
            circleInfo.Radius = JieRadis;

            Bitmap newImage = DrawBll.DrawCircleOnImage(img, circleInfo);            
            // 将新图像传递给输出
            CircleInfo_OutPut.TransferData(circleInfo);
            m_op_img_out.TransferData(newImage);
            m_img_draw = newImage;
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
