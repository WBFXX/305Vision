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
using System.Collections.Generic;
using _305Vision.Common;
using static _305Vision.Common._305Enum;

namespace _305Vision.MySTNode.Operator
{
    [STNode("/算子", "在图像上画点")]
    public class FindEdgeRectangle : ImageBaseNode
    {

        private STNodeOption outDataOption;
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
        [STNodeProperty("阈值", "阈值")]
        public int GradientThreshold { get; set; }
        //[STNodeProperty("点集", "点集")]
        [STNodeProperty("点集", "点集")]
        public int[] Array { get; set; }
        [STNodeProperty("找边方向", "找边方向")]
        public _305Enum.EdgeDetectionType EdgeDetectionType { get; set; }

        #endregion



        protected override void OnCreate()
        {
            base.OnCreate();
            outDataOption = this.OutputOptions.Add("输出点集", typeof(int[]),false);
            this.Title = "矩形找边";
            
            this.AutoSize = false;
            this.Height += 50;
            var selectButton = new STNodeButton
            {
                Text = "选取",
                Location = new Point(42, 130)
            };
            EdgeDetectionType = EdgeDetectionType.黑到白;
            GradientThreshold = 20;
            EdgeNum = 20;
            selectButton.MouseClick += SelectButton_Click;
            this.Controls.Add(selectButton);
            inOption.DataTransfer += OpImgInDataTransfer;
            this.Invalidate();

        }

        


        /// <summary>
        /// "选取"按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectButton_Click(object sender, EventArgs e)
        {
            if(this.inOption.ConnectionCount==0)return;

            findEdgeRectangleForm.InitializeParameters(EdgeNum, Start, End, Array, GradientThreshold, EdgeDetectionType);

            DialogResult result = findEdgeRectangleForm.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            Start = findEdgeRectangleForm.Start;
            End = findEdgeRectangleForm.End;
            Angle = findEdgeRectangleForm.Angle;
            Array = findEdgeRectangleForm.Array;
            GradientThreshold = findEdgeRectangleForm.GradientThreshold;
            EdgeDetectionType=findEdgeRectangleForm.EdgeDetectionType;

            //m_img_draw = findEdgeRectangleForm.OverImage;
            m_op_img_out.TransferData(findEdgeRectangleForm.OverImage);
            
            this.Invalidate();
        }

        protected override void InOption_DisConnected(object sender, STNodeOptionEventArgs e)
        {
            base.InOption_DisConnected (sender, e);
            findEdgeRectangleForm.OverImage = null;

        }
        //有数据传进来 建立连接的时候
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
                    List<Point> listPoints = UtilsBLL.ConvertArrayToPointList(Array);
                    //打印点集
                    int listI = 1;
                    foreach (Point point in listPoints)
                    {
                        //logger.Info("xxxxxxxxxxxxxxxxxx点 " + listI + $"坐标：({point.X}, {point.Y})\n");
                        listI++;
                    }
                    outDataOption.TransferData(Array);
                }
                else
                {
                    findEdgeRectangleForm.ResouseImage = (Image)img;
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
                    IntPtr Points  = IntPtr.Zero;
                    int sizee = 0;
                    byte* imageDataPtr = OpenCVSDK.findEdgeRectangle(info.ImagePtr, (int)info.Width, (int)info.Height,
                        (int)info.Stride, Start.X, Start.Y, End.X, End.Y, Angle, EdgeDetectionType , EdgeNum, GradientThreshold,ref Points, ref sizee);

                    //读取点集
                    //byte* arrayPtr = (byte*)Points;//读取点集
                    int[] array = new int[sizee];//读取点集
                    Marshal.Copy(Points, array, 0, sizee);//复制点集数组
                    this.Array = array;
                    OpenCVSDK.releaseBuffer(Points);
                    //返回复制好的图像
                    return UtilsBLL.GetImageBytes((IntPtr)imageDataPtr, imageData.Width, imageData.Height, 3);
                }
            });

            findEdgeRectangleForm.OverImage = (Image)processedImage;
            m_op_img_out.TransferData((Image)processedImage);
            m_img_draw = (Image)processedImage;
            this.Invalidate();
            
        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            STNodeBLL.DrawBody(dt, m_img_draw, this.Left, this.Top+20);
        }
        


    }
}
