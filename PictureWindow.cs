using System;
using System.Drawing;
using System.Windows.Forms;

public class PictureWindow : PictureBox
{
    private Point mouseDownPoint;
    private Point currentImageLocation;
    private bool isDragging;

    public PictureWindow()
    {
        InitializePictureBox();
    }

    private void InitializePictureBox()
    {
        this.SizeMode = PictureBoxSizeMode.Zoom;

        // 添加鼠标按下和释放事件
        this.MouseDown += ZoomPictureBox_MouseDown;
        this.MouseUp += ZoomPictureBox_MouseUp;

        // 添加鼠标移动事件
        this.MouseMove += ZoomPictureBox_MouseMove;

        // 添加滚轮事件
        this.MouseWheel += ZoomPictureBox_MouseWheel;
    }

    private void ZoomPictureBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDragging = true;
            mouseDownPoint = e.Location;
        }
    }

    private void ZoomPictureBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDragging = false;
        }
    }

    private void ZoomPictureBox_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging)
        {
            int deltaX = e.X - mouseDownPoint.X;
            int deltaY = e.Y - mouseDownPoint.Y;

            // 更新当前图像的位置
            currentImageLocation.X += deltaX;
            currentImageLocation.Y += deltaY;

            // 设置图像新的位置
            this.Location = currentImageLocation;

            mouseDownPoint = e.Location;
        }
    }

    private void ZoomPictureBox_MouseWheel(object sender, MouseEventArgs e)
    {
        if (ModifierKeys == Keys.Control) // 按住Ctrl键时才进行缩放
        {
            int change = e.Delta / 120; // 计算滚轮滚动的值
            ZoomImage(change > 0); // 根据滚动方向进行缩放
        }
    }

    private void ZoomImage(bool zoomIn)
    {
        int factor = zoomIn ? 2 : 1 / 2;

        int newWidth = (int)(this.Width * factor);
        int newHeight = (int)(this.Height * factor);

        // 设置缩放后的图像大小
        this.Size = new Size(newWidth, newHeight);
    }
}
