using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class CodestringComparer : IComparer<Codestring>
    {
        public int Compare(Codestring x, Codestring y)
        {
            return x.Criterium - y.Criterium;
        }
    }
}
