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

        // 属性用于存储所选 PictureBox 名称
        private string _selectedPictureBoxName;
        [STNodeProperty("选择图像", "选择一个图像")]
        public string SelectedPictureBoxName
        {
            get => _selectedPictureBoxName;
            set
            {
                _selectedPictureBoxName = value;
                // 根据选择的名称检索 PictureBox 对象
                PictureBox selectedPictureBox = GetPictureBoxByName(_selectedPictureBoxName);
                // 如果需要，可以在此处更新其他属性或执行其他操作
            }
        }

        // 方法用于根据名称检索 PictureBox 对象
        private PictureBox GetPictureBoxByName(string name)
        {
            return ST_GETpictureBoxes.FirstOrDefault(p => p.Name == name);
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            this.Title = "图像显示选择框";
            this.TitleColor = Color.FromArgb(20);
            this.AutoSize = false;
            this.Size = new Size(200, 200);

            // 创建并配置用于 PictureBox 选择的 ComboBox
            ComboBox pictureBoxComboBox = new ComboBox();
            pictureBoxComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            pictureBoxComboBox.Width = 150;
            pictureBoxComboBox.SelectedIndexChanged += PictureBoxComboBox_SelectedIndexChanged;

            // 将 PictureBox 名称添加到 ComboBox 中
            foreach (PictureBox pictureBox in ST_GETpictureBoxes)
            {
                pictureBoxComboBox.Items.Add(pictureBox.Name);
            }

            // 将 ComboBox 添加到节点的控件中
        }

        // ComboBox 选择更改事件处理程序
        private void PictureBoxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                // 当 ComboBox 选择更改时更新所选 PictureBox 名称
                SelectedPictureBoxName = comboBox.SelectedItem.ToString();
            }
        }


    }
}
