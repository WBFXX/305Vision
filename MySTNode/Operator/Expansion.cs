using _305Vision.BLL;
using _305Vision.SDK;
using _305Vision.图片操作测试;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static _305Vision.MySTNode.Operator.Eroding;

namespace _305Vision.MySTNode.Operator
{
    [STNode("/算子","图像膨胀")]
    public class Expansion : ImageBaseNode
    {
        #region 算法膨胀参数
        private int kerSizeX = 3;
        private int kerSizeY = 3;
        //private int _KerStr = 3;
        private KerStrEnum _KerStr = KerStrEnum.交叉形;
        private int pointX=-1;
        private int pointY=-1;
        private int iterations = 1;

        [STNodeProperty("内核长","内核大小，默认为3")]
        public int KerSizeX { get => kerSizeX; set => kerSizeX = value; }
        [STNodeProperty("内核宽", "内核大小，默认为3")]
        public int KerSizeY { get => kerSizeY; set => kerSizeY = value; }
        [STNodeProperty("内核结构", "1/2/3")]
        public KerStrEnum KerStr
        {
            get { return _KerStr; }
            set
            {
                _KerStr = value;
            }
        }
        [STNodeProperty("锚点横坐标", "默认-1为中心点")]
        public int PointX { get => pointX; set => pointX = value; }
        [STNodeProperty("锚点纵坐标", "默认-1为中心点")]
        public int PointY { get => pointY; set => pointY = value; }
        [STNodeProperty("操作次数", "默认1")]
        public int Iterations { get => iterations; set => iterations = value; }
        #endregion


        private STNodeOption in_option;
        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "图像膨胀";

            in_option = this.InputOptions.Add("输入图像", typeof(Image), true);
            in_option.DataTransfer += In_option_DataTransfer;
        }

        private void In_option_DataTransfer(object sender, STNodeOptionEventArgs e)
        {

            //如果当前不是连接状态 或者 接受到的数据为空
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {

                m_op_img_out.TransferData(null);    //向所有输出节点输出空数据
                m_img_draw = null;                  //需要绘制显示的图片置为空

            }
            else
            {

                // 调用方法
                Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)e.TargetOption.Data,
                    imageData =>
                    {

                        // 具体的处理逻辑
                        unsafe
                        {
                            byte* imageDataPtr = OpenCVSDK.expansion(imageData.Scan0, imageData.Width, imageData.Height, imageData.Width*3, KerSizeX, KerSizeY, (int)KerStr, PointX, PointY, Iterations);

                            // 处理后的数据流复制到托管数组
                            int size = imageData.Width * imageData.Height * 3;
                            byte[] imageByte = new byte[size];
                            Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                            OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);

                            return imageByte;
                        }
                    });
                this.logger.Info("图像" + this.Title + "处理完成");
                m_op_img_out.TransferData((Image)processedImage);//out选项 输出
                m_img_draw = (Image)processedImage;
            }
        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            //非static 需要先实例化
            STNodeBLL.DrawBody(dt, m_img_draw, this.Left, this.Top);
        }
    }
}
