using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.Utils
{
    public class MyOption
    {
        // 封装获取连接的所有输出节点并重新连接的函数
        //作者提供的GetConnection()方法有问题
        /// <summary>
        /// 重新绘制连线，目的为了连接的上个节点重新传输数据
        /// </summary>
        /// <param name="in_option">输入节点</param>
        public void ReconnectOutputNodes(STNodeOption in_option)
        {
            var connectedNodes = in_option.Owner.Owner.GetConnectionInfo();
            foreach (var outputNode in connectedNodes)
            {
                if(outputNode.Input == in_option)
                {
                // 断开连接
                in_option.DisConnectOption(outputNode.Output);

                // 重新连接
                in_option.ConnectOption(outputNode.Output);
            }
        }
        }

        // 封装获取连接的所有输出节点并重新传输数据的函数
        /// <summary>
        /// 获取连接的所有输出节点并重新传输数据的函数
        /// </summary>
        /// <param name="in_option">输入节点</param>
        public void TransferDataFromConnectedOutputNodes(STNodeOption in_option)
        {
            var connectedNodes = in_option.Owner.Owner.GetConnectionInfo();
            foreach (var outputNode in connectedNodes)
            {
                // 如果当前连接的前一个节点是in_option，重新传输数据
                if (outputNode.Input == in_option)
                {
                    outputNode.Output.TransferData();
                }
            }
        }
    }
}
