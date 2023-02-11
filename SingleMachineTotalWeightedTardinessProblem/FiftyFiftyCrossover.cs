using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class FiftyFiftyCrossover : ICrossover
    {
        public void MakeCrossover(Codestring firstParent, Codestring secondParent, out Codestring child)
        {
            List<int> childCode = new List<int>(firstParent.codestring);
            int size = childCode.Count;
            int divider = 0;
            if (size % 2 == 1)
            {
                divider = (size + 1) / 2;
            }
            else
            {
                divider = size / 2;
            }
            for (int i = divider; i < childCode.Count; i++)
            {
                childCode[i] = secondParent.codestring[i];
            }
            child = new Codestring(childCode, firstParent.Sample);
        }

        public override string ToString()
        {
            return "Crossover which takes half of genotype from one parent, half from another";
        }
    }
}
