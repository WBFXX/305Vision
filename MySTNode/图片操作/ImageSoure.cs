using _305Vision.图片操作测试;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.MySTNode.图片操作
{
    [STNode("Image","111111111111111111111111111111111111111")]
    public class ImageSoure : ImageBaseNode
    {
        private string _FileName;//默认的DescriptorType不支持文件路径的选择 所以需要扩展
        [STNodeProperty("输入图片","选择图片")]
        public string FileName
        {
            get { return _FileName; }
            set
            {
                Image img = null; //当文件名被设置时 加载图片并 向输出节点输出
                if (!string.IsNullOrEmpty(value))
                {
                    img = Image.FromFile(value);
                }
                if (m_img_draw != null) m_img_draw.Dispose();
                m_img_draw = img;
                _FileName = value;
                m_op_img_out.TransferData(m_img_draw, true);
                this.Invalidate();
            }
        }

    }
}
