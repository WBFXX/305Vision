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

namespace _305Vision.MySTNode.功能节点
{

    
    /// <summary>
    /// 灰度处理
    /// </summary>
    [STNode("/ROI功能节点", "在图像上画点")]
    public class DotROI : ImageBaseNode
    {
        private STNodeOption in_option;
        private OWindows.DotROIForm RetangelROI = new OWindows.DotROIForm();

        //private STNodeOption out_option;

        protected override void OnCreate()
        {
            base.OnCreate();
           

            this.Title = "画点";
            in_option = this.InputOptions.Add("输入图像", typeof(Image), true);
            
            this.AutoSize = false;
            this.Height += 30;
            var ctrl = new STNodeButton();
            ctrl.Text = "绘制";
            ctrl.Location = new Point(42, 110);
            this.Controls.Add(ctrl);
            

            //当输入节点有数据输入时候
            in_option.DataTransfer += new STNodeOptionEventHandler(Op_img_in_DataTransfer);
            
            ctrl.MouseClick += Owner_Click;
        }


        
        private void Owner_Click(object sender, EventArgs e)
        {
            //创建RetangelROI类的实例，并传递RetangelROIInfo类的实例
            //实例化图像处理窗口

            RetangelROI.ShowDialog();
            m_img_draw = RetangelROI.OverImage;
            m_op_img_out.TransferData(RetangelROI.OverImage);//out选项 输出

        }
        
        void Op_img_in_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            //如果当前不是连接状态 或者 接受到的数据为空
            if (e.Status != ConnectionStatus.Connected || e.TargetOption.Data == null)
            {
                m_op_img_out.TransferData(null);    //向所有输出节点输出空数据
                m_img_draw = null;                  //需要绘制显示的图片置为空
            }
            else
            {
                    Bitmap img = (Bitmap)e.TargetOption.Data;
                    //把图像传给操作表格
                    RetangelROI.ResouseImage = (Image)img;
                    m_op_img_out.TransferData((Image)img);//out选项 输出
                    m_img_draw = (Image)img;
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
