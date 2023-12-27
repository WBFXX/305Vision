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
        private static FormPlatform _instance;
        public static FormPlatform Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed )
                {
                    _instance = new FormPlatform();
                    
                }

                return _instance;
            }
        }

        

        public List<PictureBox> PictureBoxes
        {
            get {
                //当窗口被隐藏时（也就是假关闭后，应该获取不到PictureBoxes的）
                return pictureBoxes; 
            }
            private set
            {
                pictureBoxes = value;
            }
        }

        public int CameraCount { get => cameraCount; set => cameraCount = value; }
        public static Dictionary<string, PictureBox> PictureBoxName { get => pictureBoxName; set => pictureBoxName = value; }

        private int cameraCount;

        public  FormPlatform(int cameraCount)
        {
            PictureBoxes = new List<PictureBox>();
            this.cameraCount = cameraCount;
            InitializeComponent();
            InitializeCamera();
            AdjustImageArea();
            this.Text = "带数量的窗口";
        }

        public FormPlatform()
        {
            if (CameraCount == 0)
                 {
                      cameraCount = 4;//为了测试效果，将相机数量设置为5
                 }

                InitializeComponent();
                InitializeCamera();
                AdjustImageArea();
        
        }

        /// <summary>
        /// 创建Platform单例，用于更新单例引用
        /// </summary>
        /// <param name="platform"></param>
        public static void SetPlatformInstance(FormPlatform platform)
        {
            _instance = platform;
            
            //MessageBox.Show(platform.IsHidden.ToString());
        }

        private  List<PictureBox> pictureBoxes = new List<PictureBox>();

        private Dictionary<PictureBox, Size> pictureBoxSizes = new Dictionary<PictureBox, Size>(); //添加了一个字典，用于存储PictureBox的大小
        private Dictionary<PictureBox, Point> pictureBoxLocations = new Dictionary<PictureBox, Point>(); //添加了一个字典，用于存储PictureBox的位置
        private Dictionary<Object, bool> pictureBoxTag = new Dictionary<Object, bool>(); //添加一个字典，用于存储Picturebox的Tag是否被点击
        private static Dictionary<String, PictureBox> pictureBoxName = new Dictionary<String, PictureBox>(); //添加一个字典，通过Name查找PictureBox对象

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
            for (int i = 0; i < CameraCount; i++)
            {
                PictureBox pictureBox = new PictureBox
                {
                    Name = "图像" + (i + 1),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Black,
                    Margin = new Padding(1),
                    Dock = DockStyle.Fill,
                    Anchor = AnchorStyles.None,

                    Tag = i + 1,
                    
                    //可以设置其他属性
                };
                pictureBoxName[pictureBox.Name] = pictureBox;
                pictureBox.Click += PictureBox_Click;
                PictureBoxes.Add(pictureBox);
                string imgePath = "e:/1.jpg";
                Bitmap bitmap = new Bitmap(imgePath);
                PictureBoxes[0].Image = bitmap;
                flowLayoutPanel1.Controls.Add(pictureBox);
            }
        }


        private void AdjustImageArea()
        {
            int rows = (int)Math.Ceiling(Math.Sqrt(CameraCount));
            int cols = (int)Math.Ceiling((double)CameraCount / rows);
            

            foreach (PictureBox pictureBox in PictureBoxes)
            {
                int tag;
                if (int.TryParse(pictureBox.Tag.ToString(), out tag))
                {
                    pictureBox.Size = CalculatePictureBoxSize(rows, cols);
                    //pictureBox.Location = CalculatePictureBoxLocation(tag, rows, cols);
                }
            }
        }
        private Object tagToPictureBox;
        private PictureBox lastClickedPictureBox; // 添加一个字段存储上一次点击的 PictureBox
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;

            if (PictureBoxes.Count > 0)
            {

                // 确保字典中包含当前 PictureBox 的键
                if (!pictureBoxTag.ContainsKey(PictureBoxes[0].Tag))
                {
                    // 如果没有，可以将其添加到字典中,记录第一个窗口是否被点击
                    pictureBoxTag[PictureBoxes[0].Tag] = false;
                }


                //根据pictureBox状态来还原位置,如果已经被点击过
                if (pictureBoxTag[PictureBoxes[0].Tag])
                {

                    // 是放大状态，再次点击还原
                    //
                    PictureBoxes[0].Size = pictureBoxSizes[PictureBoxes[0]];
                    PictureBoxes[0].Location = pictureBoxLocations[PictureBoxes[0]];
                    PictureBoxes[0].Margin = new Padding(1);
                    pictureBoxTag[PictureBoxes[0].Tag] = false;

                    exChangePictureBox(clickedPictureBox, PictureBoxes[(int)tagToPictureBox - 1]);
                }
                else
                {
                    // 如果是还原状态，点击放大
                    //先存第一个pic的大小和位置

                    pictureBoxSizes[PictureBoxes[0]] = PictureBoxes[0].Size;
                    pictureBoxLocations[PictureBoxes[0]] = PictureBoxes[0].Location;
                    //exChangePictureBox(clickedPictureBox, pictureBoxes[0]);

                    //0为主显示框,点击后，直接把第一个放大，并且交换显示的图片
                    PictureBoxes[0].Size = new Size(flowLayoutPanel1.Width, flowLayoutPanel1.Height);

                    //clickedPictureBox.Location = new Point(0, 0);
                    //放到最满 然后取消边距
                    PictureBoxes[0].Margin = new Padding(0);
                    pictureBoxTag[PictureBoxes[0].Tag] = true;
                    tagToPictureBox = clickedPictureBox.Tag;//1
                    exChangePictureBox(clickedPictureBox, PictureBoxes[0]);



                }
                //存储点击的图片，为了防止调整窗体bug出现
                lastClickedPictureBox = clickedPictureBox;//0


            }
            else
            {
                MessageBox.Show("无图像显示框");
            }
               




        }

        
        /// <summary>
        /// 图像显示区域大小 2参
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private Size CalculatePictureBoxSize(int rows, int cols)
        {
            int width = (int)((flowLayoutPanel1.Width - cols * 2) / cols);
            int height = (int)((flowLayoutPanel1.Height - rows * 2) / rows);

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

            //// 计算每个 PictureBox 在其行内的居中偏移量
            // int centeringOffsetX = (flowLayoutPanel1.Width / cols - CalculatePictureBoxSize(rows, cols).Width) / 2;
            //+ centeringOffsetX
            return new Point(col * (flowLayoutPanel1.Width / cols) + xOffset , row * (flowLayoutPanel1.Height / rows) + yOffset);
        }

        /// <summary>
        /// 交换算法
        /// </summary>
        /// <param name="pictureBox_Sourse">交换源</param>
        /// <param name="pictureBox_Mudi">目的地</param>
        private void exChangePictureBox(PictureBox pictureBox_Sourse, PictureBox pictureBox_Mudi)
        {
            if (pictureBox_Sourse == pictureBox_Mudi) return;


            //不能直接换pictureBox
            // 保存源控件的属性
            Image imageTemp = pictureBox_Sourse.Image;
            // 将目标控件的属性赋给源控件
            pictureBox_Sourse.Image = pictureBox_Mudi.Image;
            // 将源控件的属性赋给目标控件
            pictureBox_Mudi.Image = imageTemp;




            //更新字典
            PictureBox pictureBox = new PictureBox();
            pictureBox = FormPlatform.PictureBoxName[pictureBox_Sourse.Name];
            FormPlatform.PictureBoxName[pictureBox_Sourse.Name] = FormPlatform.PictureBoxName[pictureBox_Mudi.Name];
            FormPlatform.PictureBoxName[pictureBox_Mudi.Name] =pictureBox ;

            // pictureBox_Sourse.Tag = pictureBox_Mudi.Tag; 
            
            
            // pictureBox_Mudi.Tag = TagTemp;
        }

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            // 当窗体大小发生变化时更新 PictureBox 的大小和位置
            AdjustImageArea();

            // 将上一次点击的 PictureBox 的 Tag 状态设置为 false
            if (lastClickedPictureBox != null)
            {
                exChangePictureBox(lastClickedPictureBox, pictureBoxes[0]);
                pictureBoxes[0].Margin = new Padding(1);
                pictureBoxTag.Clear();
                lastClickedPictureBox = null;
            }
        }
    }
}
