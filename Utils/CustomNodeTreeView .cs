using NLog;
using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.Utils
{
    public class CustomNodeTreeView : STNodeTreeView
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        protected override int OnStartDrawItem(DrawingTools dt, STNodeTreeCollection Items, int nCounter, int nLevel)
        {
            Items.Parent.IsOpen = true;
            logger.Info(Items);
            return base.OnStartDrawItem(dt, Items, nCounter, nLevel);
            
        }
    }
}
