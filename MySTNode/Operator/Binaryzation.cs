using _305Vision.SDK;
using _305Vision.Utils;
using _305Vision.图片操作测试;
using NLog;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace _305Vision.MySTNode.Operator
{
    [STNode("/算子/", "对图像进行灰度处")]
    public class Binaryzation : ImageBaseNode
    {
        private STNodeOption in_option;
        //private STNodeOption out_option;7
        private int doubleValue;
        [STNodeProperty("阈值设置", "输入一个0-255的数值")]
        public int DoubleValue
        {
            get
            {
                return doubleValue;
            }

            set
            {
                doubleValue = value;
                // 触发值改变事件
                OnDoubleValueChanged(EventArgs.Empty);

                this.Invalidate();
            }
        }

        // 定义值改变事件
        public event EventHandler DoubleValueChanged;

        // 触发值改变事件的方法
        protected virtual void OnDoubleValueChanged(EventArgs e)
        {
            DoubleValueChanged?.Invoke(this, e);
        }


        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "二值化";

            in_option = this.InputOptions.Add("输入图像", typeof(Image), true);

            //当输入节点有数据输入时候
            in_option.DataTransfer += new STNodeOptionEventHandler(op_img_in_DataTransfer);
        }

        

        void op_img_in_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            //如果当前不是连接状态 或者 接受到的数据为空
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {

                m_op_img_out.TransferData(null);    //向所有输出节点输出空数据
                m_img_draw = null;                  //需要绘制显示的图片置为空
                
            }
            else
            {

                // 创建一个 ProssesImage 实例,动态方法必须创建实例
                ProssesImage processImageInstance = new ProssesImage();

                // 然后调用实例方法
                Bitmap processedImage = processImageInstance.ProcessImage((Bitmap)e.TargetOption.Data, 
                    imageData =>
                {

                    // 具体的处理逻辑
                    unsafe
                    {
                        byte* imageDataPtr = OpenCVSDK.binaryzation(imageData.Scan0, imageData.Width, imageData.Height, imageData.Stride, doubleValue, 255, 0);

                        // 处理后的数据流复制到托管数组
                        int size = imageData.Width * imageData.Height * 3;
                        byte[] imageByte = new byte[size];
                        Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                        OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);

                        return imageByte;
                    }
                });
                this.logger.Info("图像" +  this.Title + "处理完成");
                m_op_img_out.TransferData((Image)processedImage);//out选项 输出
                m_img_draw = (Image)processedImage;

            }
        }
        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            MyDrawBody myDrawBodyInstance = new MyDrawBody();
            myDrawBodyInstance.DrawBody(dt, m_img_draw, this.Left, this.Top);
        }

    }
}
