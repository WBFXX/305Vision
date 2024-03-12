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
using System.Collections.Generic;

namespace _305Vision.MySTNode.Operator
{
    [STNode("/算子", "在图像上画点")]
    public class FindEdgeCircular : ImageBaseNode
    {
        private OWindows.FindEdgeCircularForm findEdgeCircular = new OWindows.FindEdgeCircularForm();
        private STNodeOption outDataOption;

        #region 找边参数
        [STNodeProperty("圆心X坐标", "开始坐标")]
        public int pointX { get; set; }
        [STNodeProperty("圆心Y坐标", "终点坐标")]
        public int pointY { get; set; }
        [STNodeProperty("小圆半径", "旋转角度")]
        public int radiusSmall { get; set; }
        [STNodeProperty("大圆半径", "旋转角度")]
        public int radiusBig { get; set; }
        [STNodeProperty("找边线数量", "找边线数量")]
        public int EdgeNum { get; set; }
        [STNodeProperty("阈值", "阈值")]
        public int GradientThreshold { get; set; }
        [STNodeProperty("点集", "点集")]
        public int[] Array { get; set; }
        #endregion

        protected override void OnCreate()
        {
            base.OnCreate();
            outDataOption = this.OutputOptions.Add("输出点集", typeof(int[]), false);
            this.Title = "圆形找边";

            this.AutoSize = false;
            this.Height += 50;

            var selectButton = new STNodeButton
            {
                Text = "选取",
                Location = new Point(42, 130)
            };
            GradientThreshold = 20;
            EdgeNum = 20;
            selectButton.MouseClick += SelectButton_Click;
            this.Controls.Add(selectButton);

            inOption.DataTransfer += OpImgInDataTransfer;
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            findEdgeCircular.InitializeParameters(pointX,pointY,radiusSmall,radiusBig,EdgeNum,GradientThreshold);

            DialogResult result = findEdgeCircular.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            pointX = findEdgeCircular.pointX;
            pointY = findEdgeCircular.pointY;
            radiusSmall = findEdgeCircular.radiusSmall;
            radiusBig = findEdgeCircular.radiusBig;
            GradientThreshold = findEdgeCircular.gradientThreshold;

            m_img_draw = findEdgeCircular.OverImage;
            m_op_img_out.TransferData(findEdgeCircular.OverImage);
            this.Invalidate();
        }

        private void OpImgInDataTransfer(object sender, STNodeOptionEventArgs e)
        {
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {
                m_op_img_out.TransferData(null);
                outDataOption.TransferData(null);
                m_img_draw = null;
            }
            else
            {
                Bitmap img = (Bitmap)e.TargetOption.Data;
                if (isSecond)
                {
                    ProcessImage(img);
                    outDataOption.TransferData(Array);//点集增加
                }
                else
                {
                    findEdgeCircular.ResouseImage = (Image)img;
                    m_op_img_out.TransferData((Image)img);
                    isSecond = true;
                }
            }
        }

        private void ProcessImage(Bitmap img)
        {
            Bitmap processedImage = ProcessImageBLL.ProcessImage(img, imageData =>
            {
                BasicImageInfo info = BasicImageInfo.GetImgInfo(imageData);
                unsafe
                {
                    IntPtr Points = IntPtr.Zero;
                    int sizee=0;
                    byte* imageDataPtr = OpenCVSDK.findEdgeCircular(info.ImagePtr, (int)info.Width, (int)info.Height,
                        (int)info.Stride, pointX, pointY, radiusSmall, radiusBig, EdgeNum, GradientThreshold, ref Points,ref sizee);

                    //读取点集
                    int[] array = new int[sizee];//读取点集
                    Marshal.Copy(Points, array, 0, sizee);//复制点集数组
                    this.Array = array;
                    OpenCVSDK.releaseBuffer(Points);
                    //返回复制好的图像
                    return UtilsBLL.GetImageBytes((IntPtr)imageDataPtr, imageData.Width, imageData.Height, 3);
                }
            });

            findEdgeCircular.OverImage = (Image)processedImage;
            m_op_img_out.TransferData((Image)processedImage);
            m_img_draw = (Image)processedImage;
            this.Invalidate();
        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            STNodeBLL.DrawBody(dt, m_img_draw, this.Left, this.Top + 20);
        }
    }
}
