using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    interface ISelector
    {
        void MakeSelection(List<Codestring> curPopulation, out List<Codestring> nextPopulation);
    }
}
