using System;
using System.Drawing;
using System.Windows.Forms;

namespace _305Vision.Utils
{
    public static class InputBox
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

        public static string Show(string prompt, string title = "Input", string defaultValue = "")
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

            Label textLabel = new Label { Left = LabelLeft, Top = LabelTop, Text = prompt,Width = TextBoxWidth };
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
