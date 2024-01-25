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
    public class FindEdgeRectangle : ImageBaseNode
    {
        private STNodeOption inOption;
        private OWindows.FindEdgeRectangleForm findEdgeRectangleForm = new OWindows.FindEdgeRectangleForm();

        #region 找边参数
        [STNodeProperty("开始坐标", "开始坐标")]
        public Point Start { get; set; }
        [STNodeProperty("终点坐标", "终点坐标")]
        public Point End { get; set; }
        [STNodeProperty("旋转角度", "旋转角度")]
        public double Angle { get; set; }
        [STNodeProperty("找边线数量", "找边线数量")]
        public int EdgeNum { get; set; }
        #endregion

        protected override void OnCreate()
        {
            base.OnCreate();

            this.Title = "找边算法";
            inOption = this.InputOptions.Add("输入图像", typeof(Image), true);

            this.AutoSize = false;
            this.Height += 30;

            var selectButton = new STNodeButton
            {
                Text = "选取",
                Location = new Point(42, 110)
            };
            selectButton.MouseClick += SelectButton_Click;
            this.Controls.Add(selectButton);

            inOption.DataTransfer += OpImgInDataTransfer;
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            findEdgeRectangleForm.InitializeParameters(EdgeNum, Start, End);

            DialogResult result = findEdgeRectangleForm.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            Start = findEdgeRectangleForm.Start;
            End = findEdgeRectangleForm.End;
            Angle = findEdgeRectangleForm.Angle;

            m_img_draw = findEdgeRectangleForm.OverImage;
            m_op_img_out.TransferData(findEdgeRectangleForm.OverImage);
            this.Invalidate();
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
                    byte* imageDataPtr = OpenCVSDK.findEdgeRectangle(info.ImagePtr, (int)info.Width, (int)info.Height,
                        (int)info.Stride, Start.X, Start.Y, End.X, End.Y, Angle, EdgeNum);
                    int size = imageData.Width * imageData.Height * 3;
                    byte[] imageByte = new byte[size];
                    Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                    OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);
                    return imageByte;
                }
            });

            findEdgeRectangleForm.ResouseImage = (Image)processedImage;
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
