using _305Vision.DAL;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.BLL
{
    public class STNodeOptionBLL
    {

        // 封装获取连接的所有输出节点并重新连接的函数
        //作者提供的GetConnection()方法有问题
        /// <summary>
        /// 重新绘制连线，目的为了连接的上个节点重新传输数据
        /// </summary>
        /// <param name="in_option">输入节点</param>
        public static bool ReconnectOutputNodes(STNodeOption in_option)
        {
            try
            {
                STNodeOptionDAL.ReconnectOutputNodes(in_option);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }


        // 封装获取连接的所有输出节点并重新传输数据的函数
        /// <summary>
        /// 获取连接的所有输出节点并重新传输数据的函数
        /// </summary>
        /// <param name="in_option">输入节点</param>
        public static bool TransferDataFromConnectedOutputNodes(STNodeOption in_option)
        {

            try
            {
                STNodeOptionDAL.TransferDataFromConnectedOutputNodes(in_option);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
