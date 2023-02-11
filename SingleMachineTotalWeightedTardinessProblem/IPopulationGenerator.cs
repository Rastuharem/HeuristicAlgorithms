using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    interface IPopulationGenerator
    {
        List<Codestring> GetPopulation(int sizeOfPopulation);
    }
}
