using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
namespace _305Vision
{
    public partial class FormPlatform : DockContent
    {
        
        private int cameraCount = 4;//为了测试效果，将相机数量设置为5
        public FormPlatform()
        {

                InitializeComponent();
                InitializeCamera();
                AdjustImageArea();
        
        }
        FormOutput FormOutput = FormOutput.Instance;

        private List<PictureBox> pictureBoxes = new List<PictureBox>();

        private Dictionary<PictureBox, Size> pictureBoxSizes = new Dictionary<PictureBox, Size>(); //添加了一个字典，用于存储PictureBox的大小
        private Dictionary<PictureBox, Point> pictureBoxLocations = new Dictionary<PictureBox, Point>(); //添加了一个字典，用于存储PictureBox的位置
        private Dictionary<PictureBox, bool> pictureBoxStates = new Dictionary<PictureBox, bool>(); //添加一个字典，用于存储Picturebox是否被点击
        private Dictionary<Object, bool> pictureBoxTag = new Dictionary<Object, bool>(); //添加一个字典，用于存储Picturebox的Tag是否被点击
        //private Dictionary<Object, PictureBox> tagToPictureBox = new Dictionary<Object, PictureBox>(); //添加一个字典，用于查找tag对应的pictureBox

        //public Form1()
        //{
        //    InitializeComponent();
        //    InitializeCamera();
        //    AdjustImageArea();
        //}

        private void InitializeCamera()
        {
            
            
            //根据相机数量生成PictureBox
            for (int i = 0; i < cameraCount; i++)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Name = "pictureBox" + (i + 1),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Black,
                    Margin = new Padding(1),
                    Dock = DockStyle.Fill,
                    Anchor = AnchorStyles.None,

                    Tag = i + 1,
                    
                    //可以设置其他属性
                };

                pictureBox.Click += PictureBox_Click;
                pictureBoxes.Add(pictureBox);
                string imgePath = "e:/1.jpg";
                Bitmap bitmap = new Bitmap(imgePath);
                pictureBoxes[0].Image = bitmap;
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }


        private void AdjustImageArea()
        {
            int rows = (int)Math.Ceiling(Math.Sqrt(cameraCount));
            int cols = (int)Math.Ceiling((double)cameraCount / rows);

            foreach (PictureBox pictureBox in pictureBoxes)
            {
                int tag;
                if (int.TryParse(pictureBox.Tag.ToString(), out tag))
                {
                    pictureBox.Size = CalculatePictureBoxSize(rows, cols);
                    pictureBox.Location = CalculatePictureBoxLocation(tag, rows, cols);
                }
            }
        }
        private Object tagToPictureBox;
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;
            

                // 确保字典中包含当前 PictureBox 的键
                if (!pictureBoxTag.ContainsKey(pictureBoxes[0].Tag))
                {
                    // 如果没有，可以将其添加到字典中,记录第一个窗口是否被点击
                    pictureBoxTag[pictureBoxes[0].Tag] = false;
                }


                //根据pictureBox状态来还原位置,如果已经被点击过
                if (pictureBoxTag[pictureBoxes[0].Tag])
                {

                    // 如果是放大状态，再次点击还原
                    //
                    pictureBoxes[0].Size = pictureBoxSizes[pictureBoxes[0]];
                    pictureBoxes[0].Location = pictureBoxLocations[pictureBoxes[0]];
                    pictureBoxes[0].Margin = new Padding(1);
                    pictureBoxTag[pictureBoxes[0].Tag] = false;

                    exChangePictureBox(clickedPictureBox, pictureBoxes[(int)tagToPictureBox - 1]);
                }
                else
                {
                    // 如果是还原状态，点击放大
                    //先存第一个pic的大小和位置

                    pictureBoxSizes[pictureBoxes[0]] = pictureBoxes[0].Size;
                    pictureBoxLocations[pictureBoxes[0]] = pictureBoxes[0].Location;
                    //exChangePictureBox(clickedPictureBox, pictureBoxes[0]);

                    //0为主显示框,点击后，直接把第一个放大，并且交换显示的图片
                    pictureBoxes[0].Size = new Size(flowLayoutPanel1.Width, flowLayoutPanel1.Height);

                    //clickedPictureBox.Location = new Point(0, 0);
                    //放到最满 然后取消边距
                    pictureBoxes[0].Margin = new Padding(0);
                    pictureBoxTag[pictureBoxes[0].Tag] = true;
                    tagToPictureBox = clickedPictureBox.Tag;
                    exChangePictureBox(clickedPictureBox, pictureBoxes[0]);

                }
            
        }

        /// <summary>
        /// 图像显示区域大小
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private Size CalculatePictureBoxSize(int rows, int cols)
        {
            int width = (flowLayoutPanel1.Width - cols*2) / cols;
            int height = (flowLayoutPanel1.Height - rows*2) / rows;

            ////确保每个PictureBox的宽高都接近正方形
            //int size = Math.Min(width, height);
            return new Size(width, height);
        }
        /// <summary>
        /// 计算图像显示区域的位置
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private Point CalculatePictureBoxLocation(int tag, int rows, int cols)
        {
            int row = (tag - 1) / cols;
            int col = (tag - 1) % cols;

            int xOffset = (flowLayoutPanel1.Width / cols - CalculatePictureBoxSize(rows, cols).Width);
            int yOffset = (flowLayoutPanel1.Height / rows - CalculatePictureBoxSize(rows, cols).Height);

            // 计算每个 PictureBox 在其行内的居中偏移量
            int centeringOffsetX = (flowLayoutPanel1.Width / cols - CalculatePictureBoxSize(rows, cols).Width) / 2;

            return new Point(col * (flowLayoutPanel1.Width / cols) + xOffset + centeringOffsetX, row * (flowLayoutPanel1.Height / rows) + yOffset);
        }

        /**
         * 交换算法
         * */
        private void exChangePictureBox(PictureBox pictureBox_Sourse, PictureBox pictureBox_Mudi)
        {
            if (pictureBox_Sourse == pictureBox_Mudi) return;
            // 保存源控件的属性
            Image imageTemp = pictureBox_Sourse.Image;
            //Object TagTemp = pictureBox_Sourse.Tag;

            // 将目标控件的属性赋给源控件
            pictureBox_Sourse.Image = pictureBox_Mudi.Image;
            // pictureBox_Sourse.Tag = pictureBox_Mudi.Tag; 
            // 将源控件的属性赋给目标控件
            pictureBox_Mudi.Image = imageTemp;
            // pictureBox_Mudi.Tag = TagTemp;
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            // 当窗体大小发生变化时更新 PictureBox 的大小和位置
            AdjustImageArea();
        }
    }
}
