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
using System.Collections.Generic;

namespace _305Vision.MySTNode.Operator
{
    [STNode("/算子/", "输入图像与数组")]
    public class SpotSearch : ImageBaseNode
    {

        #region 拟合参数

        [STNodeProperty("背景", "默认为0")]
        public int BackGroundFlag { get; set; }
        [STNodeProperty("面积最大约束", "“无”为-1")]
        public int AreaMax { get; set; }
        [STNodeProperty("面积最小约束", "“无”为-1")]
        public int AreaMin { get; set; }
        [STNodeProperty("宽度最大约束", "“无”为-1")]
        public int WidthMax { get; set; }
        [STNodeProperty("宽度最小约束", "“无”为-1")]
        public int WidthMin { get; set; }
        [STNodeProperty("高度最大约束", "“无”为-1")]
        public int HeightMax { get; set; }
        [STNodeProperty("高度最大约束", "“无”为-1")]
        public int HeightMin { get; set; }
        [STNodeProperty("斑点X坐标数组", "斑点X坐标数组")]
        public int[] ArrayX { get; set; }

        [STNodeProperty("斑点Y坐标数组", "斑点Y坐标数组")]
        public int[] ArrayY { get; set; }
        [STNodeProperty("斑点宽度数组", "斑点宽度数组")]
        public int[] ArrayW { get; set; }
        [STNodeProperty("斑点高度数组", "斑点高度数组")]
        public int[] ArrayH { get; set; }
        [STNodeProperty("斑点面积数组", "斑点面积数组")]
        public int[] ArrayA { get; set; }
        [STNodeProperty("数组长度", "数组长度")]
        public int ArraySize { get; set; }


        #endregion

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "斑点查找";
            #region 参数初始化默认值
            BackGroundFlag = 1;
            AreaMax = -1;
            AreaMin = -1;
            WidthMax = -1;
            WidthMin = -1;
            HeightMax = -1;
            HeightMin = -1;
            #endregion
            this.AutoSize = false;
            this.Height += 50;
            var selectButton = new STNodeButton
            {
                Text = "备用",
                Location = new Point(42, 110 + STNodeStyleSetting.COMMON_TOP)
            };
            

            this.Controls.Add(selectButton);
            inOption.DataTransfer += OpImgInDataTransfer;
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
                if (inOption.ConnectionCount != 0 )
                {
                    m_op_img_out.TransferData((Image)img);
                    isSecond = true;
                    try
                    {
                        ProcessImage(img);
                    }
                    catch (Exception ex)
                    {
                    MessageBox.Show("斑点查找超界：" + ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                
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
                    //初始化指针数值，
                    IntPtr PointsX = IntPtr.Zero;
                    IntPtr PointsY = IntPtr.Zero;
                    IntPtr PointsW = IntPtr.Zero;
                    IntPtr PointsH = IntPtr.Zero;
                    IntPtr PointsA = IntPtr.Zero;

                    int sizee = 0;

                    byte* imageDataPtr = OpenCVSDK.spotSearch(info.ImagePtr, (int)info.Width, (int)info.Height, (int)info.Stride,
                    BackGroundFlag, AreaMax, AreaMin, WidthMax, WidthMin, HeightMax, HeightMin,
                    ref PointsX, ref PointsY, ref PointsW, ref PointsH, ref PointsA, ref sizee);



                    this.ArraySize = sizee;
                    #region 读取点集
                    int[] array = new int[sizee];//读取点集
                    Marshal.Copy(PointsX, array, 0, sizee);//复制点集数组
                    ArrayX = array;

                    array = new int[sizee];//重置数组
                    Marshal.Copy(PointsY, array, 0, sizee);//复制点集数组
                    this.ArrayY = array;
                    array = new int[sizee];//重置数组
                    Marshal.Copy(PointsW, array, 0, sizee);//复制点集数组
                    this.ArrayW = array;
                    array = new int[sizee];//重置数组
                    Marshal.Copy(PointsH, array, 0, sizee);//复制点集数组
                    this.ArrayH = array;
                    array = new int[sizee];//重置数组
                    Marshal.Copy(PointsA, array, 0, sizee);//复制点集数组
                    this.ArrayA = array;

                    OpenCVSDK.releaseBuffer(PointsX);
                    OpenCVSDK.releaseBuffer(PointsY);
                    OpenCVSDK.releaseBuffer(PointsW);
                    OpenCVSDK.releaseBuffer(PointsH);
                    OpenCVSDK.releaseBuffer(PointsA);
                    //OpenCVSDK.releaseBuffer(PointsX);
                    //this.ArrayX = arrayx;
                    //this.ArrayX = UtilsBLL.ReadPoints(PointsX, sizee);
                    //this.ArrayY = UtilsBLL.ReadPoints(PointsY, sizee);
                    //this.ArrayW = UtilsBLL.ReadPoints(PointsW, sizee);
                    //this.ArrayH = UtilsBLL.ReadPoints(PointsH, sizee);
                    //this.ArrayA = UtilsBLL.ReadPoints(PointsA, sizee);
                    #endregion

                    return UtilsBLL.GetImageBytes((IntPtr)imageDataPtr, imageData.Width, imageData.Height, 3);

                }
            });


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
