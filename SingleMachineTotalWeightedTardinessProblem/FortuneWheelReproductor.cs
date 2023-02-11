using System;
using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class FortuneWheelReproductor : IReproductor
    {
        public void MakeReproduction(List<Codestring> population, out Codestring firstParent, out Codestring secondParent)
        {
            List<Codestring> sortedPopulation = new List<Codestring>(population);
            sortedPopulation.Sort(new CodestringComparer());
            sortedPopulation.Reverse();

            firstParent = GetRandomIndividual(sortedPopulation);
            secondParent = GetRandomIndividual(sortedPopulation);

            return;
        }

        private Codestring GetRandomIndividual(List<Codestring> sortedPopulation)
        {
            Codestring res = sortedPopulation[0];
            List<double> NormilizedVec = new List<double>();
            Random rnd = new Random();
            long sumCriterium = 0;
            for (int i = 0; i < sortedPopulation.Count; i++)
            {
                sumCriterium += sortedPopulation[i].Criterium;
            }
            NormilizedVec.Add((double)sortedPopulation[0].Criterium / (double)sumCriterium);
            for (int i = 1; i < sortedPopulation.Count; i++)
            {
                NormilizedVec.Add(NormilizedVec[i - 1] + (double)sortedPopulation[i].Criterium / (double)sumCriterium);
            }
            NormilizedVec[NormilizedVec.Count - 1] = 1.0;

            double rndProb = rnd.NextDouble();
            for (int i = 0; i < sortedPopulation.Count; i++)
            {
                if (NormilizedVec[i] > rndProb)
                {
                    res = sortedPopulation[i];
                    return res;
                }
            }
            return res;
        }

        public override string ToString()
        {
            return "Reproduction which counts probabilities for each codestring and choose parents using them";
        }
    }
}
