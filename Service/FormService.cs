using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.Service
{
    public class FormService
    {
        private static FormPlatform _platform = new FormPlatform();
        private static FormProcess _process;
        private static FormOutput _output;
        private static ToolsBox _toolsBox;

        public static FormPlatform Platform { get => _platform; set => _platform = value; }
        public static FormProcess Process { get => _process; set => _process = value; }
        public static FormOutput Output { get => _output; set => _output = value; }
        public static ToolsBox ToolsBox { get => _toolsBox; set => _toolsBox = value; }
    }
}
