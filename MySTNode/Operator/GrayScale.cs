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
using _305Vision.BLL;

namespace _305Vision.MySTNode.Operator
{


    /// <summary>
    /// 灰度处理
    /// </summary>
    [STNode("/算子/", "对图像进行灰度处理")]
    public class GrayScale : ImageBaseNode
    {
        private STNodeOption in_option;
        //private STNodeOption out_option;

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "灰度化";
            in_option = this.InputOptions.Add("输入图像", typeof(Image), true);

            //当输入节点有数据输入时候
            in_option.DataTransfer += new STNodeOptionEventHandler(Op_img_in_DataTransfer);
        }


        void Op_img_in_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            
            //如果当前不是连接状态 或者 接受到的数据为空
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {
                
                m_op_img_out.TransferData(null);    //向所有输出节点输出空数据
                m_img_draw = null;                  //需要绘制显示的图片置为空
                
            }
            else
            {
                //否则将图像转bitmap形式
                Bitmap bmp = (Bitmap)e.TargetOption.Data;
                //获取图像
                BitmapData imgData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);
                int width = imgData.Width;
                int height = imgData.Height;
                int stride = imgData.Stride;
                try
                {
                    unsafe
                    {
                        //System.Windows.Forms.MessageBox.Show("a");
                        byte* imageDataPtr = OpenCVSDK.grayScale(imgData.Scan0, width, height, stride);
                        bmp.UnlockBits(imgData);
                        int size = width * height;
                        byte[] data = new byte[size];//创建同大小的数组

                        // 接收 C++ 的字节数据流后释放分配的内存空间
                        Marshal.Copy((IntPtr)imageDataPtr, data, 0, size);
                        OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);//释放

                        // 定义一个用于将字节数据流转换为 Bitmap 图片的 Bitmap 变量
                        Bitmap ImageBitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

                        // 设置颜色调色板
                        ColorPalette palette = ImageBitmap.Palette;
                        for (int i = 0; i < 256; i++)
                        {
                            palette.Entries[i] = Color.FromArgb(i, i, i);
                        }

                        ImageBitmap.Palette = palette;

                        //处理完的图像BitmapData
                        BitmapData bitmapData = ImageBitmap.LockBits(new Rectangle(0, 0, width, height),
                            ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                        //复制到要显示的
                        Marshal.Copy(data, 0, bitmapData.Scan0, data.Length);
                        ImageBitmap.UnlockBits(bitmapData);//释放bitmapData

                        m_op_img_out.TransferData((Image)ImageBitmap);//out选项 输出
                        m_img_draw = (Image)ImageBitmap;


                    }
                }
                catch (Exception ex) 
                {
                    logger.Error("这里是grayScale："+ex);
                }
                
            }
        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            //非static 需要先实例化
            STNodeBLL.DrawBody(dt, m_img_draw,this.Left,this.Top);
        }

    }
}
