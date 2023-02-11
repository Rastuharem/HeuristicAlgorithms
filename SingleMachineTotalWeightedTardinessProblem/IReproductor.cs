using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    interface IReproductor
    { 
        void MakeReproduction(List<Codestring> population, out Codestring firstParent, out Codestring secondParent);
    }
}
