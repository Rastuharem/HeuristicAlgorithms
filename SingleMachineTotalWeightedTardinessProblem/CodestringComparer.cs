using System.Collections.Generic;

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
