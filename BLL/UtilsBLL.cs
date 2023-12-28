using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _305Vision.DAL;

namespace _305Vision.BLL
{
    /// <summary>
    /// 公共工具类
    /// </summary>
    public class UtilsBLL
    {

        /// <summary>
        /// 创建输入框并判断 输入提示信息，窗口标题，输入栏提示词
        /// </summary>
        /// <param name="prompt">输入提示信息</param>
        /// <param name="title">窗口标题</param>
        /// <param name="defaultValue">输入栏提示词</param>
        /// <returns>用户输入的数,返回0为输入无效</returns>
        public static int InputBox(string prompt, string title = "Input", string defaultValue = "")
        {
            //把用户输入的值赋值给inputValue
            int.TryParse(UtilsDAL.InputBox(prompt, title, defaultValue), out int inputValue);
                try
            {
                // 检查规则
                if (inputValue > 9  || inputValue <= 0)
                {
                    MessageBox.Show("输入无效，请输入一个介于1和9之间的数字。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return 0; // 输入无效，关闭窗口
                }

                return inputValue;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message,"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return 0;
            }

        }

    }
}
