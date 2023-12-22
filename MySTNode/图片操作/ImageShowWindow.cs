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
        private Logger logger = LogManager.GetCurrentClassLogger();
        
        public string PName
        {
            get => pName; set
            {
                pName = value;
                ComboBox.PName = value;
                //this.Invalidate();
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





            try
            {
                //下拉选择框
                ComboBox = new ComBoxControl();
                ComboBox.Top = 5;
                //logger.Info(ComboBox.Size.ToString());
                ComboBox.Left = 12;
                //ComboBox.Location = new Point(20,20);
                ComboBox.Width = 76;
                // 手动设置一次文本 初始显示文本
                ComboBox.PName = "选择窗口";
                //ComboBox.PName = this.PName;
                //m_ctrl_select.ValueChanged += (s, e) =>
                //{
                //    this._MixType = (ColorMixType)m_ctrl_select.Enum;
                //};
                ComboBox.ValueChanged += (s, e) =>
                {
                    //MessageBox.Show(this.PName + "CPNAME：" + ComboBox.PName);
                    this.PName = this.ComboBox.PName;
                };
                this.Controls.Add(ComboBox);
                this.Invalidate();
            }catch (Exception ex)
            {
                logger.Error(ex);
            }


        }
        

        private ComBoxControl ComboBox;//自定义控件
        private String pName ;
        private Dictionary<String, PictureBox> pNameDictionary = FormPlatform.PictureBoxName;
        


        void IN_option_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            //问题出在被选中的节点，节点被选中且被链接后，会重新加载ComBoxCL
            //经过测试 原生控件也有这个问题，会记录上一次点击的内容，在重新被连线后 会再次触发上次点击过的按钮。
            //MessageBox.Show(PName);

            if (e.Status == ConnectionStatus.Connected)
            {
                if (e.TargetOption.Data != null)
                {
                    //MessageBox.Show(this.PName);
                    try
                    {
                        pNameDictionary[this.PName].Image = (Image)e.TargetOption.Data;

                    }
                    catch (Exception ex)
                    {

                        logger.Error(ex);
                    }
                    logger.Info("图像显示连接成功");
                }
                else pNameDictionary[PName].Image = null;
            }
            else
            {
                pNameDictionary[PName].Image = null;
            }



        }
        
    }
    
}
