using _305Vision.BLL;
using NLog;
using PictureWindowControl;
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

        private List<PictureWindow> pictureWindows = new List<PictureWindow>();
        private static Dictionary<String, PictureWindow> pictureWindowName = new Dictionary<String, PictureWindow>(); //添加一个字典，通过Name查找PictureBox对象
        private Dictionary<Object, bool> pictureWindowTag = new Dictionary<Object, bool>(); //添加一个字典，用于存储Picturebox的Tag是否被点击
        private Dictionary<PictureWindow, Size> pictureWindowSizes = new Dictionary<PictureWindow, Size>(); //添加了一个字典，用于存储PictureBox的大小
        private Dictionary<PictureWindow, Point> pictureWindowLocations = new Dictionary<PictureWindow, Point>(); //添加了一个字典，用于存储PictureBox的位置
        private Dictionary<Object, PictureWindow> tagToPictureWindow = new Dictionary<Object, PictureWindow>(); //添加了一个字典，用于存储Tag对应的PictureWindow

        public static Dictionary<string, PictureWindow> PictureWindowName { get => pictureWindowName; set => pictureWindowName = value; }

        private int cameraCount;
        public List<PictureWindow> PictureWindows
        {
            get
            {
                //当窗口被隐藏时（也就是假关闭后，应该获取不到PictureBoxes的）
                return pictureWindows;
            }
            set
            {
                pictureWindows = value;
            }
        }


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

        public int CameraCount { get => cameraCount; set => cameraCount = value; }


        public FormPlatform(int cameraCount)
        {
            //PictureBoxes = new List<PictureBox>();
            PictureWindows = new List<PictureWindow>();
            this.cameraCount = cameraCount;
            InitializeComponent();
            InitializeCamera();
            AdjustImageArea();
            this.Text = "主窗口";
        }

        public FormPlatform()
        {
            if (CameraCount == 0)
                 {
                      cameraCount = 4;//为了测试效果，将相机数量设置为4
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
        }

        

        private void InitializeCamera()
        {
            
            
            //根据相机数量生成PictureBox
            for (int i = 0; i < CameraCount; i++)
            {
                PictureWindow pictureWindow = new PictureWindow
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
                pictureWindow.PictureBox.Tag = pictureWindow.Tag;//把标志赋给PictureBox
                tagToPictureWindow[pictureWindow.Tag] = pictureWindow;
                pictureWindowName[pictureWindow.Name] = pictureWindow;
                //pictureBoxName[pictureBox.Name] = pictureBox;

                // 添加事件订阅
                pictureWindow.PictureBox.Click += PictureBox_Click;

                PictureWindows.Add(pictureWindow);
                
                //string imgePath = "e:/1.jpg";
                //Bitmap bitmap = new Bitmap(imgePath);
                //PictureBoxes[0].Image = bitmap;
                flowLayoutPanel1.Controls.Add(pictureWindow);
            }
        }

        private void AdjustImageArea()
        {
            int rows = (int)Math.Ceiling(Math.Sqrt(CameraCount));
            int cols = (int)Math.Ceiling((double)CameraCount / rows);

            foreach (PictureWindow pictureWindow in PictureWindows)
            {
                int tag;
                if (int.TryParse(pictureWindow.Tag.ToString(), out tag))
                {
                    pictureWindow.Size = CalculatePictureBoxSize(rows, cols);
                }
            }

        }


        private Object tagOfPictureWindow;
        private PictureWindow lastClickedPictureBox; // 添加一个字段存储上一次点击的 PictureBox
        private void PictureBox_Click(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
            {
                PictureBox clickedPictureBox = (PictureBox)sender;

                PictureWindow clickedPictureWindow = tagToPictureWindow[clickedPictureBox.Tag];////加一下通过TAG找pictureWindow然后继续

                if (PictureWindows.Count > 0)
                {

                    // 确保字典中包含当前 PictureBox 的键
                    if (!pictureWindowTag.ContainsKey(PictureWindows[0].Tag))
                    {
                        // 如果没有，可以将其添加到字典中,记录第一个窗口是否被点击
                        pictureWindowTag[PictureWindows[0].Tag] = false;
                    }


                    //根据pictureBox状态来还原位置,如果已经被点击过
                    if (pictureWindowTag[PictureWindows[0].Tag])
                    {

                        // 是放大状态，再次点击还原
                        //
                        PictureWindows[0].Size = pictureWindowSizes[PictureWindows[0]];
                        PictureWindows[0].Location = pictureWindowLocations[PictureWindows[0]];
                        PictureWindows[0].Margin = new Padding(1);
                        pictureWindowTag[PictureWindows[0].Tag] = false;

                        //exChangePictureBox(clickedPictureBox, PictureBoxes[(int)tagOfPictureWindow - 1]);
                        exChangePictureWindowBox(clickedPictureWindow, PictureWindows[(int)tagOfPictureWindow - 1]);
                    }
                    else
                    {
                        // 如果是还原状态，点击放大
                        //先存第一个pic的大小和位置

                        pictureWindowSizes[PictureWindows[0]] = PictureWindows[0].Size;
                        pictureWindowLocations[PictureWindows[0]] = PictureWindows[0].Location;
                        //exChangePictureBox(clickedPictureBox, pictureBoxes[0]);

                        //0为主显示框,点击后，直接把第一个放大，并且交换显示的图片
                        PictureWindows[0].Size = new Size(flowLayoutPanel1.Width, flowLayoutPanel1.Height);

                        //clickedPictureBox.Location = new Point(0, 0);
                        //放到最满 然后取消边距
                        PictureWindows[0].Margin = new Padding(0);
                        pictureWindowTag[PictureWindows[0].Tag] = true;
                        tagOfPictureWindow = clickedPictureWindow.Tag;//1
                        //exChangePictureBox(clickedPictureBox, PictureBoxes[0]);
                        exChangePictureWindowBox(clickedPictureWindow, PictureWindows[0]);
                    }
                    //存储点击的图片，为了防止调整窗体bug出现
                    lastClickedPictureBox = clickedPictureWindow;//
                }
                else
                {
                    MessageBox.Show("无图像显示框");
                }
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
        /// PW交换算法
        /// </summary>
        /// <param name="pictureBox_Sourse"></param>
        /// <param name="pictureBox_Mudi"></param>
        private void exChangePictureWindowBox(PictureWindow pictureWindow_Sourse, PictureWindow pictureWindow_Mudi)
        {
            if (pictureWindow_Sourse == pictureWindow_Mudi) return;


            //不能直接换pictureBox
            // 保存源控件的属性
            Image imageTemp = pictureWindow_Sourse.Image;
            // 将目标控件的属性赋给源控件
            pictureWindow_Sourse.Image = pictureWindow_Mudi.Image;
            // 将源控件的属性赋给目标控件
            pictureWindow_Mudi.Image = (Bitmap)imageTemp;

            //更新字典
            PictureWindow pictureExWindow = FormPlatform.PictureWindowName[pictureWindow_Sourse.Name];
            FormPlatform.PictureWindowName[pictureWindow_Sourse.Name] = FormPlatform.PictureWindowName[pictureWindow_Mudi.Name];
            FormPlatform.PictureWindowName[pictureWindow_Mudi.Name] = pictureExWindow;

        }


        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            // 当窗体大小发生变化时更新 PictureBox 的大小和位置
            AdjustImageArea();

            // 将上一次点击的 PictureBox 的 Tag 状态设置为 false
            if (lastClickedPictureBox != null)
            {
                //exChangePictureBox(lastClickedPictureBox, pictureBoxes[0]);
                exChangePictureWindowBox(lastClickedPictureBox, pictureWindows[0]);
                pictureWindows[0].Margin = new Padding(1);
                pictureWindowTag.Clear();
                lastClickedPictureBox = null;
            }
        }
    }
}
