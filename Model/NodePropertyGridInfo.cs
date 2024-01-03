// 在 _305Vision.Service 命名空间下
using ST.Library.UI.NodeEditor;
using System;

namespace _305Vision.Model

{
    public class NodePropertyGridInfo
    {
        /// <summary>
        /// 节点属性信息
        /// </summary>
        private static STNodePropertyGrid sTNodePropertyGrid;

        public static STNodePropertyGrid STNodePropertyGrid { get => sTNodePropertyGrid; set => sTNodePropertyGrid = value; }

        

    }
}
