using _305Vision.BLL;
using _305Vision.Common;
using _305Vision.MySTNode.控件库;
using _305Vision.SDK;
using _305Vision.Utils;
using Newtonsoft.Json;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Ink;
using System.Windows.Media.Media3D;
using static _305Vision.Common._305Enum;

namespace _305Vision.图片操作测试
{
    [STNode("图像源", "Wubofan", "1157712386@qq.com", " ", "Image Node")]
    public class CameraImgInputNode : ImageBaseNode
    {
        private string _FileName;//默认的DescriptorType不支持文件路径的选择 所以需要扩展
        [STNodeProperty("相机操作", "点击配置相机s", DescriptorType = typeof(OpenFileCamere))]
        public string FileName
        {
            get { return _FileName; }
            set
            {
                Image img = null;  //当文件名被设置时 加载图片并 向输出节点输出
                if (!string.IsNullOrEmpty(value))
                {
                    img = Image.FromFile(value);
                    isSecond=true;
                }
                //判断输入图片的宽度
                if(img.Width * 3 %4!=0 )
                {
                    Bitmap imgBit = (Bitmap)img.Clone();
                    BitmapData bitmapData = imgBit.LockBits(new Rectangle(0,0,img.Width,img.Height),ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    //获取处理完的图像
                    byte[] processedData = WindowsViewBLL.showImage(bitmapData.Scan0, bitmapData.Width, bitmapData.Height, bitmapData.Stride);
                    //创建新宽度的图像
                    Bitmap bitmap = new Bitmap((img.Width + 3) / 4 * 4, img.Height, PixelFormat.Format24bppRgb);
                    BitmapData processedImageData = bitmap.LockBits(new Rectangle(0, 0, (img.Width + 3) / 4 * 4, img.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                    // 将处理后的数据流复制到新图像
                    Marshal.Copy(processedData, 0, processedImageData.Scan0, processedData.Length);
                    imgBit.UnlockBits(bitmapData);
                    bitmap.UnlockBits(processedImageData);
                    img = bitmap;
                    logger.Info("图像宽度不规范，已自动处理。");
                }
                if (m_img_draw != null) m_img_draw.Dispose();
                m_img_draw = img;
                _FileName = value;
                m_op_img_out.TransferData(m_img_draw, true);
                this.Invalidate();
            }
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Height += 30;
            this.Title = "相机操作";
            var ConfigButton = new STNodeButton();
            var PhotographedButton = new STNodeButton();
            ConfigButton = new STNodeButton
            {
                Width = CommonStyleSetting.STNODE_BUTTON_WIDTH,
                Text = "关闭",
                Location = new Point(CommonStyleSetting.COMMON_LEFT_OR_RIGHT,this.Height-this.TitleHeight - ConfigButton.Height - 7)
            };
            PhotographedButton = new STNodeButton
            {
                Width = CommonStyleSetting.STNODE_BUTTON_WIDTH,
                Text = "连接",
                Location = new Point(this.Width - CommonStyleSetting.STNODE_BUTTON_WIDTH - CommonStyleSetting.COMMON_LEFT_OR_RIGHT, this.Height - this.TitleHeight - ConfigButton.Height - 7)
            };
            ConfigButton.MouseClick += ConfigButton_Click;
            PhotographedButton.MouseClick += PhotographedButton_MouseClick;
            this.Controls.Add(ConfigButton);
            this.Controls.Add(PhotographedButton);
        }
      
        private bool isCameraClose = false;

        private void PhotographedButton_MouseClick(object sender, MouseEventArgs e)
        {
            if (OpenCVSDK.openAndInitCamera(192, 168, 10, 2, 80000, 20) == 0)
            {
                logger.Info("连接相机失败");
                return;
            }
            else
            {
                isSecond=startTimer1();
                //while (!isCameraClose)
                //{
                //    int Cwidth = 0;
                //    int Cheight = 0;
                //    int Cstride = 0;
                //    unsafe
                //    {
                //        //System.Windows.Forms.MessageBox.Show("a");
                //        byte* imageDataPtr = OpenCVSDK.readCameraImage(ref Cwidth, ref Cheight, ref Cstride);
                //        int size = Cwidth * Cheight;
                //        byte[] data = new byte[size];//创建同大小的数组
                //                                     // 接收 C++ 的字节数据流后释放分配的内存空间
                //        Marshal.Copy((IntPtr)imageDataPtr, data, 0, size);
                //        OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);//释放
                //                                                      // 定义一个用于将字节数据流转换为 Bitmap 图片的 Bitmap 变量
                //        Bitmap ImageBitmap = new Bitmap(Cwidth, Cheight, PixelFormat.Format8bppIndexed);
                //        // 设置颜色调色板
                //        ColorPalette palette = ImageBitmap.Palette;
                //        for (int i = 0; i < 256; i++)
                //        {
                //            palette.Entries[i] = Color.FromArgb(255, i, i, i);
                //        }

                //        ImageBitmap.Palette = palette;
                //        //处理完的图像BitmapData
                //        BitmapData bitmapData = ImageBitmap.LockBits(new Rectangle(0, 0, Cwidth, Cheight),
                //            ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                //        //复制到要显示的
                //        Marshal.Copy(data, 0, bitmapData.Scan0, data.Length);
                //        ImageBitmap.UnlockBits(bitmapData);//释放bitmapData

                //        m_op_img_out.TransferData((Image)ImageBitmap);//out选项 输出
                //        m_img_draw = (Image)ImageBitmap;
                //    }
                //}

            }
            
        }
        //获取相机byte数据帧，将其转换为bmp类型后输出到pictureBox中
        public static Bitmap bitmap1;//定义一个公共的bitmap变量用来根据PLC信号获得抓拍到的图像
        public void showCamera1Frame()
        {
            try
            {
                object locker = new object();
                lock (locker)
                {
                    unsafe
                    {

                    
                    int width =0, height=0, stride = 0;
                    //获取mat格式图片的宽、高以及通道数OpenCVSDK.readCameraImage(ref width, ref height, ref channels) 
                    if (OpenCVSDK.readCameraImage(ref width, ref height, ref stride) == null)
                    {
                        stopTimer1();
                        MessageBox.Show("获取1号相机图像失败!");
                        return;
                    }
                        //System.Windows.Forms.MessageBox.Show("a");
                        byte* imageDataPtr = OpenCVSDK.readCameraImage(ref width, ref height, ref stride);
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
                            palette.Entries[i] = Color.FromArgb(255, i, i, i);
                        }

                        ImageBitmap.Palette = palette;
                        //处理完的图像BitmapData
                        BitmapData bitmapData = ImageBitmap.LockBits(new Rectangle(0, 0, width, height),
                            ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                        //复制到要显示的
                        Marshal.Copy(data, 0, bitmapData.Scan0, data.Length);
                        ImageBitmap.UnlockBits(bitmapData);//释放bitmapData
                        if (m_img_draw != null)
                        {
                            m_img_draw.Dispose();
                        }
                        m_op_img_out.TransferData((Image)ImageBitmap.Clone());//out选项 输出
                        m_img_draw = (Bitmap)ImageBitmap.Clone();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Timer类绑定的事件
        public void timer1Event(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                
                    //加载时 取消跨线程检查
                    Control.CheckForIllegalCrossThreadCalls = false;
                    //将相机的数据帧绘制到pictureBox1控件中
                    showCamera1Frame();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //定义Timer类
        static System.Timers.Timer timer1;
        //初始化Timer1
        public bool startTimer1()
        {
            try
            {
                    //设置定时间隔(毫秒为单位)
                    //int interval = 200;
                    int interval = 500;
                    timer1 = new System.Timers.Timer(interval);
                    //设置执行一次（false）还是一直执行(true)
                    timer1.AutoReset = true;
                    //设置是否执行System.Timers.Timer.Elapsed事件
                    timer1.Enabled = true;
                    //绑定Elapsed事件
                    timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1Event);
                    this.Invalidate();
                    return true;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        //关闭Timer1
        public bool stopTimer1()
        {
            try
            {

                    //关闭计时器
                    timer1.Enabled = false;
                    timer1.Stop();
                    //将对应的picture控件中的图像清空
                    return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }











































        private void ConfigButton_Click(object sender, MouseEventArgs e)
        {
            if (OpenCVSDK.stopAndcloseCamera()==2)
            {
                stopTimer1();
                logger.Info("关闭相机成功");
                isCameraClose = true;
                return;
            }
            else
            {
                logger.Info("未检测到相机");
                return;
            }

        }

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            STNodeBLL.DrawBody(dt,m_img_draw,this.Left,this.Top);
        }
    }
    /// <summary>
    /// 对默认Descriptor进行扩展 使得支持文件路径选择
    /// </summary>
    public class OpenFileCamere : STNodePropertyDescriptor
    {
        private Rectangle m_rect_open;  //需要绘制"打开"按钮的区域
        private StringFormat m_sf;

        public OpenFileCamere()
        {
            m_sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
        }

        protected override void OnSetItemLocation()
        {   //当在STNodePropertyGrid上确定此属性需要显示的区域时候
            base.OnSetItemLocation();                   //计算出"打开"按钮需要绘制的区域
            m_rect_open = new Rectangle(
                this.RectangleR.Right - 20,
                this.RectangleR.Top,
                20,
                this.RectangleR.Height);
        }

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            if (m_rect_open.Contains(e.Location))
            {     //点击在"打开"区域 则弹出文件选择框
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "*.jpg|*.jpg|*.png|*.png"
                };
                if (ofd.ShowDialog() != DialogResult.OK) return;
                this.SetValue(ofd.FileName);
            }
            else base.OnMouseClick(e);                //否则默认处理方式 弹出文本输入框
        }

        protected override void OnDrawValueRectangle(DrawingTools dt)
        {
            base.OnDrawValueRectangle(dt);              
            //在STNodePropertyGrid绘制此属性区域时候将"打开"按钮绘制上去
            dt.Graphics.FillRectangle(Brushes.Gray, m_rect_open);
            dt.Graphics.DrawString("+", this.Control.Font, Brushes.White, m_rect_open, m_sf);
        }
    }
}
