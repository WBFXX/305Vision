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

namespace _305Vision.MySTNode.Operator
{
    [STNode("/算子", "在图像上画点")]
    public class FindEdgeCircular : ImageBaseNode
    {
        private STNodeOption inOption;
        private OWindows.FindEdgeCircularForm findEdgeCircular = new OWindows.FindEdgeCircularForm();

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
        [STNodeProperty("梯度变化阈值", "找边线数量")]
        public int gradientThreshold { get; set; }
        
        #endregion

        protected override void OnCreate()
        {
            base.OnCreate();

            this.Title = "圆形找边";
            inOption = this.InputOptions.Add("输入图像", typeof(Image), true);

            this.AutoSize = false;
            this.Height += 30;

            var selectButton = new STNodeButton
            {
                Text = "选取",
                Location = new Point(42, 110)
            };

            EdgeNum = 20;
            selectButton.MouseClick += SelectButton_Click;
            this.Controls.Add(selectButton);

            inOption.DataTransfer += OpImgInDataTransfer;
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            findEdgeCircular.InitializeParameters(pointX,pointY,radiusSmall,radiusBig,EdgeNum,gradientThreshold);

            DialogResult result = findEdgeCircular.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            pointX = findEdgeCircular.pointX;
            pointY = findEdgeCircular.pointY;
            radiusSmall = findEdgeCircular.radiusSmall;
            radiusBig = findEdgeCircular.radiusBig;
            gradientThreshold = findEdgeCircular.gradientThreshold;

            m_img_draw = findEdgeCircular.OverImage;
            m_op_img_out.TransferData(findEdgeCircular.OverImage);
            this.Invalidate();
        }

        private void OpImgInDataTransfer(object sender, STNodeOptionEventArgs e)
        {
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {
                m_op_img_out.TransferData(null);
                m_img_draw = null;
            }
            else if(m_img_draw == null)
            {
                Bitmap img = (Bitmap)e.TargetOption.Data;
                findEdgeCircular.ResouseImage = (Image)img;
                m_op_img_out.TransferData((Image)img);
                m_img_draw = (Image)img;
                this.Invalidate();
            }
            else
            {
                Bitmap img = (Bitmap)e.TargetOption.Data;
                ProcessImage(img);
            }
        }

        private void ProcessImage(Bitmap img)
        {
            Bitmap processedImage = ProcessImageBLL.ProcessImage(img, imageData =>
            {
                BasicImageInfo info = BasicImageInfo.NewMethod(imageData);
                unsafe
                {
                    byte* imageDataPtr = OpenCVSDK.findEdgeCircular(info.ImagePtr, (int)info.Width, (int)info.Height,
                        (int)info.Stride, pointX, pointY, radiusSmall, radiusBig, EdgeNum, gradientThreshold);
                    int size = imageData.Width * imageData.Height * 3;
                    byte[] imageByte = new byte[size];
                    Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                    OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);
                    return imageByte;
                }
            });

            findEdgeCircular.ResouseImage = (Image)processedImage;
            m_op_img_out.TransferData((Image)processedImage);
            m_img_draw = (Image)processedImage;
            this.Invalidate();
        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            STNodeBLL.DrawBody(dt, m_img_draw, this.Left, this.Top);
        }
    }
}
