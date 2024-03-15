using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.Common
{
    /// <summary>
    /// 枚举
    /// </summary>
    public static class _305Enum
    {
        public enum BlackGround
        {
            黑色,
            白色
        }
        public enum EdgeDetectionType
        {
            白到黑 = 0,
            黑到白 = 1
        }
    }
}
