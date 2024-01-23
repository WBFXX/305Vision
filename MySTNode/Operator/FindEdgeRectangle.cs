using _305Vision.SDK;
using _305Vision.图片操作测试;
using NLog;
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
using _305Vision.BLL;
using _305Vision.OWindows;
using _305Vision.MySTNode.控件库;
using _305Vision.Model;

namespace _305Vision.MySTNode.Operator
{


    /// <summary>
    /// 找边算法
    /// </summary>
    [STNode("/算子", "在图像上画点")]
    public class FindEdgeRectangle : ImageBaseNode
    {
        #region 找边参数
        private STNodeOption in_option;
        private OWindows.FindEdgeRectangleForm findEdgeRectangleForm = new OWindows.FindEdgeRectangleForm();

        [STNodeProperty("开始坐标", "开始坐标")]
        public Point Start { set; get; }
        [STNodeProperty("终点坐标", "终点坐标")]
        public Point End{ set; get; }
        [STNodeProperty("旋转角度", "旋转角度")]
        public double Angle { set; get; }
        [STNodeProperty("找边线数量", "找边线数量")]
        public int EdgeNum { set; get; }
        
        #endregion

        //private STNodeOption out_option;

        protected override void OnCreate()
        {
            base.OnCreate();
           

            this.Title = "找边算法";
            in_option = this.InputOptions.Add("输入图像", typeof(Image), true);
            
            this.AutoSize = false;
            this.Height += 30;

            var ctrl = new STNodeButton();
            ctrl.Text = "选取";
            ctrl.Location = new Point(42, 110);
            this.Controls.Add(ctrl);
            //当输入节点有数据输入时候
            in_option.DataTransfer += new STNodeOptionEventHandler(Op_img_in_DataTransfer);
            ctrl.MouseClick += Owner_Click;
        }


        
        private void Owner_Click(object sender, EventArgs e)
        {
            //点击按钮的时候把参数传过去而不是OnCreate时候
            findEdgeRectangleForm.EdgeNum = EdgeNum;
            findEdgeRectangleForm.Start = Start;
            findEdgeRectangleForm.End = End;

            //创建RetangelROI类的实例，并传递RetangelROIInfo类的实例
            //实例化图像处理窗口
            DialogResult result = findEdgeRectangleForm.ShowDialog();
            // 检查用户的操作
            if (result == DialogResult.Cancel)
            {
                return; // 用户选择了关闭，提前退出方法
            }

            //选取完框框，展示图像
            Start = findEdgeRectangleForm.Start; 
            End = findEdgeRectangleForm.End;
            Angle = findEdgeRectangleForm.Angle;

            m_img_draw = findEdgeRectangleForm.OverImage;
            m_op_img_out.TransferData(findEdgeRectangleForm.OverImage);//out选项 输出
            this.Invalidate();

        }
        
        void Op_img_in_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            //如果当前不是连接状态 或者 接受到的数据为空
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {

                m_op_img_out.TransferData(null);    //向所有输出节点输出空数据
                m_img_draw = null;                  //需要绘制显示的图片置为空
                
            }
            else //有图像的时候，我们店家运行按钮的时候直接运行找边算法
            {
                Bitmap img = (Bitmap)e.TargetOption.Data;
                Bitmap processdImage = ProcessImageBLL.ProcessImage(img, imageData =>
                {
                    unsafe
                    {
                        //保存提取数据结构
                        BasicImageInfo info = BasicImageInfo.NewMethod(imageData);
                        byte* imageDataPtr = OpenCVSDK.findEdgeRectangle(info.ImagePtr, (int)info.Width, (int)info.Height, (int)info.Stride, Start.X, Start.Y, End.X, End.Y, Angle, EdgeNum);
                        int size = imageData.Width * imageData.Height * 3;
                        byte[] imageByte = new byte[size];
                        Marshal.Copy((IntPtr)imageDataPtr, imageByte, 0, size);
                        OpenCVSDK.releaseBuffer((IntPtr)imageDataPtr);
                        return imageByte;
                    }

                });
                    //把图像传给操作表格
                    findEdgeRectangleForm.ResouseImage = (Image)processdImage;
                    m_op_img_out.TransferData((Image)processdImage);//out选项 输出
                    m_img_draw = (Image)processdImage;
                this.Invalidate();
                    
            }
        }

        

        protected override void OnDrawBody(DrawingTools dt)
        {
            base.OnDrawBody(dt);
            //非static 需要先实例化
            STNodeBLL.DrawBody(dt, m_img_draw,this.Left,this.Top);
        }

        

    }
}
