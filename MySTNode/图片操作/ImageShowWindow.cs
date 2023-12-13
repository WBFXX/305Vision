using _305Vision.Blender;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace _305Vision.MySTNode.图片操作
{
    [STNode("Image", "111111111111111111111111111111111111111")]
    public class ImageShowWindow : STNode
    {
        private List<PictureBox> ST_GETpictureBoxes = FormPlatform.PictureBoxes;
        private Bitmap _bitmap;

        public Bitmap Bitmap { get => _bitmap; set => _bitmap = value; }

        private STNodeOption IN_option;
        protected override void OnCreate()
        {
            base.OnCreate();
            this.AutoSize = false;
            this.Size = new Size(100,50);
            IN_option =  this.InputOptions.Add("图片输入",typeof(Image),true);
            IN_option.DataTransfer += new STNodeOptionEventHandler(IN_option_DataTransfer);
        }

        void IN_option_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            if(e.Status == ConnectionStatus.Connected)
            {
                if (e.TargetOption.Data != null)
                {
                    MessageBox.Show("连接成功");
                    FormPlatform.PictureBoxes[0].Image = (Image)e.TargetOption.Data;
                }
                else FormPlatform.PictureBoxes[0].Image = null;
            }
            else
            {
                MessageBox.Show("断开连接");
                FormPlatform.PictureBoxes[0].Image = null;
            }
        }
    }
    
}
