using _305Vision.BLL;
using _305Vision.SDK;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace _305Vision.图片操作测试
{
    [STNode("图像源", "Wubofan", "1157712386@qq.com", " ", "Image Node")]
    public class ImageInputNode : ImageBaseNode
    {
        private string _FileName;//默认的DescriptorType不支持文件路径的选择 所以需要扩展
        [STNodeProperty("输入图片", "点击选择图片", DescriptorType = typeof(OpenFileDescriptor))]
        public string FileName
        {
            get { return _FileName; }
            set
            {
                Image img = null;  //当文件名被设置时 加载图片并 向输出节点输出
                if (!string.IsNullOrEmpty(value))
                {
                    img = Image.FromFile(value);
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
            this.Title = "ImageInput";
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
    public class OpenFileDescriptor : STNodePropertyDescriptor
    {
        private Rectangle m_rect_open;  //需要绘制"打开"按钮的区域
        private StringFormat m_sf;

        public OpenFileDescriptor()
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
