using _305Vision.BLL;
using _305Vision.SDK;
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
    [STNode("/算子/", "二值化")]
    public class Binaryzation : ImageBaseNode
    {
        private STNodeOption in_option;
        //private STNodeOption out_option;7


        private int doubleValue=150;
        private int Max=255;
        private int Min;
        [STNodeProperty("阈值", "阈值设置")]
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
                //OnDoubleValueChanged(EventArgs.Empty);

                this.Invalidate();
            }
        }

        [STNodeProperty("upper", "高于阈值时设置的像素值")]
        public int Max1
        {
            get => Max; set
            {
                
                Max = value;
                // 触发值改变事件
                //OnDoubleValueChanged(EventArgs.Empty);

            }
        }
        [STNodeProperty("lower", "低于阈值时设置的像素值")]
        public int Min1
        {
            get => Min; set
            {
                Min = value;
                // 触发值改变事件
                //OnDoubleValueChanged(EventArgs.Empty);
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
            //this.DoubleValueChanged += Binaryzation_DoubleValueChanged;
        }

        void Binaryzation_DoubleValueChanged(object sender, EventArgs e)
        {
            //判断改变值的大小是否在允许范围内
            if (DoubleValue > 255 || DoubleValue < 0 || Max>255||Max<0 || Min>255 || Min < 0)
            {
                logger.Error("参数输入错误,请规范输入(0-255)。");

                return;
            }

            //实例化自定义工具MyOption类
           if(STNodeOptionBLL.ReconnectOutputNodes(in_option))
            logger.Info(this.Title + "算子参数改变,已重新绘制图像");
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

                // 调用方法
                Bitmap processedImage = ProcessImageBLL.ProcessImage((Bitmap)e.TargetOption.Data, 
                    imageData =>
                {

                    // 具体的处理逻辑
                    unsafe
                    {
                        byte* imageDataPtr = OpenCVSDK.binaryzation(imageData.Scan0, imageData.Width, imageData.Height, imageData.Width*3, doubleValue, Max, Min);

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
            STNodeBLL.DrawBody(dt, m_img_draw, this.Left, this.Top);
        }

    }
}
