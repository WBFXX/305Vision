using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _305Vision.DAL
{
    public class UtilsDAL
    {
        private const int FormWidth = 500;
        private const int FormHeight = 170;
        private const int LabelLeft = 50;
        private const int LabelTop = 20;
        private const int TextBoxLeft = 50;
        private const int TextBoxTop = 50;
        private const int TextBoxWidth = 400;
        private const int ButtonLeft = 350;
        private const int ButtonTop = 90;
        private const int ButtonWidth = 100;
        /// <summary>
        /// 输入框，返回用户输入的内容
        /// </summary>
        /// <param name="prompt">提示内容</param>
        /// <param name="title">窗口标题</param>
        /// <param name="defaultValue">默认内容</param>
        /// <returns>返回string类型内容</returns>
        public static string InputBox(string prompt, string title = "Input", string defaultValue = "")
        {
            Form promptForm = new Form
            {
                Width = FormWidth,
                Height = FormHeight,
                //BackColor = Color.AliceBlue,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false,
                Opacity = 0.95,
                ShowIcon = false,
                ShowInTaskbar = false,
            };

            Label textLabel = new Label { Left = LabelLeft, Top = LabelTop, Text = prompt, Width = TextBoxWidth };
            TextBox textBox = new TextBox { Left = TextBoxLeft, Top = TextBoxTop, Width = TextBoxWidth, Text = defaultValue };

            Button confirmation = new Button { Text = "确定", Left = ButtonLeft, Width = ButtonWidth, Top = ButtonTop, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => promptForm.Close();

            promptForm.Controls.Add(textBox);
            promptForm.Controls.Add(confirmation);
            promptForm.Controls.Add(textLabel);
            promptForm.AcceptButton = confirmation;

            return promptForm.ShowDialog() == DialogResult.OK ? textBox.Text : defaultValue;
        }
        
    }
}
