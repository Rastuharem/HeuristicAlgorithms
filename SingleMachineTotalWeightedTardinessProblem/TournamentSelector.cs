using System;
using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class TournamentSelector : ISelector
    {
        private static int NumberOfTournamentFighters = 2; // Number of participants in fight club :)

        public void MakeSelection(List<Codestring> curPopulation, out List<Codestring> nextPopulation)
        {
            Random rnd = new Random();
            nextPopulation = new List<Codestring>();

            while (nextPopulation.Count < EvolutionGeneticAlgorithm.GetPopulationCount())
            {
                List<Codestring> Fighters = new List<Codestring>();
                for (int i = 0; i < NumberOfTournamentFighters; i++)
                {
                    int index = rnd.Next(0, curPopulation.Count);
                    Fighters.Add(curPopulation[i]);
                }
                Fighters.Sort(new CodestringComparer());
                nextPopulation.Add(Fighters[0]);
            }
        }

        public override string ToString()
        {
            return "Tournament selection with groups volume " + NumberOfTournamentFighters;
        }
    }
}
