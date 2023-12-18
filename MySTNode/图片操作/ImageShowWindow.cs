using _305Vision.Blender;
using NLog;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private List<PictureBox> ST_GETpictureBoxes = FormPlatform.PictureBoxes;
        private Bitmap _bitmap;

        public Bitmap Bitmap { get => _bitmap; set => _bitmap = value; }
        public string PName
        {
            get => pName; set
            {
                pName = value;
                this.Invalidate();
            }
        }

         



private STNodeOption IN_option;
        protected override void OnCreate()
        {
            base.OnCreate();
            this.AutoSize = false;
            this.Size = new Size(100,80);
            this.Title = "选择显示窗口";
            IN_option =  this.InputOptions.Add("",typeof(Image),true);
            IN_option.DataTransfer += new STNodeOptionEventHandler(IN_option_DataTransfer);
            




            //下拉选择框
            ComboBox = new ComBoxControl();
            ComboBox.Top = 5;
            //logger.Info(ComboBox.Size.ToString());
            ComboBox.Left = 12;
            //ComboBox.Location = new Point(20,20);
            ComboBox.Width = 76;
            this.Controls.Add(ComboBox);

        }
        

        private static ComBoxControl ComboBox;
        private static String pName ;
        private Dictionary<String, PictureBox> pNameDictionary = FormPlatform.PictureBoxName;
        


        void IN_option_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            pName = ComBoxCL.SelectPreText;


            if (e.Status == ConnectionStatus.Connected)
            {
                if (e.TargetOption.Data != null)
                {
                    //MessageBox.Show("连接成功");
                    try
                    {
                        pNameDictionary[pName].Image = (Image)e.TargetOption.Data;

                    }catch(Exception ex)
                    {
                        logger.Error(ex);
                    }
                    logger.Info("图像显示连接成功");
                }
                else pNameDictionary[pName].Image = null; 
            }
            else
            {
                    pNameDictionary[pName].Image = null;
            }

            

        }
    }
    
}
