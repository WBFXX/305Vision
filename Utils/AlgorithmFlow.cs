using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _305Vision.Utils
{
    public class AlgorithmFlow
    {
        public List<OperatorCallInfo> OperatorCalls { get; set; }

        public AlgorithmFlow()
        {
            OperatorCalls = new List<OperatorCallInfo>();
        }
    }
}
