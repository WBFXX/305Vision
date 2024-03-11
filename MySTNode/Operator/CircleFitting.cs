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

namespace _305Vision.MySTNode.Operator
{
    [STNode("/拟合", "在图像上画点")]
    public class CircleFitting : ImageBaseNode
    {

        private STNodeOption ArrInputOption;
        private double jieRadis;
        private double centerX;
        private double centerY;

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

        //
        //public double JieRadis { get; set; }
        //
        //public double CenterX { get; set; }
        //
        //public double CenterY { get; set; }

        #endregion

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "圆形拟合";
            ArrInputOption = InputOptions.Add("点集", typeof(int[]), true);

            this.AutoSize = false;
            this.Height += 50;
            var selectButton = new STNodeButton
            {
                Text = "选取",
                Location = new Point(42, 130)
            };
            CenterX = 0; 
            CenterY = 0;
            JieRadis = 0;

            this.Controls.Add(selectButton);
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

            OpenCVSDK.circleFitting(Array, (int)Array.Length, (int)Discard,ref jieRadis, ref centerX, ref centerY);

            logger.Info(jieRadis);
            //经过OpenCVSDK算法处理后，算出了圆心和半径
            // 在图像上绘制圆
            Bitmap newImage = DrawCircleOnImage(img, CenterX, CenterY, JieRadis);

            // 将新图像传递给输出
            m_op_img_out.TransferData(newImage);
            m_img_draw = newImage;
            m_op_img_out.TransferData((Image)newImage);
            this.Invalidate();

        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            STNodeBLL.DrawBody(dt, m_img_draw, this.Left, this.Top + 20);
        }

        private Bitmap DrawCircleOnImage(Bitmap image, double centerX, double centerY, double radius)
        {
            // 创建一个新的图像副本，以免修改原始图像
            Bitmap newImage = new Bitmap(image);

            // 在图像上创建Graphics对象
            using (Graphics g = Graphics.FromImage(newImage))
            {
                // 创建一个画笔
                using (Pen pen = new Pen(Color.Red, 2)) // 这里使用红色笔绘制圆形，可以根据需要进行调整
                {
                    // 计算圆的边界矩形
                    int x = (int)(centerX - radius);
                    int y = (int)(centerY - radius);
                    int diameter = (int)(2 * radius);

                    // 在图像上绘制圆
                    g.DrawEllipse(pen, x, y, diameter, diameter);
                }
            }

            return newImage;
        }


    }
}
