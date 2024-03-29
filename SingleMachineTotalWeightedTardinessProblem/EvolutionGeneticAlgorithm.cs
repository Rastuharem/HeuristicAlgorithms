﻿using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class EvolutionGeneticAlgorithm : AMethod
    {
        public static int PopulationCount = 300; // Number of individuals in population
        public static int NumberOfRepeats = 10; // Number of best solution repeats: algorithm stops when counter reaches it
        public static int NumberOfChildren = PopulationCount; // Number of children generated by parents
        public static double MutationProbability = 0.3; // Probability of mutations in new child

        public IPopulationGenerator StartingPopulation { get; set; }
        public IReproductor Reproductor { get; set; }
        public ICrossover Crossover { get; set; }
        public IMutator Mutator { get; set; }
        public ISelector Selector { get; set; }

        private IPrinter Printer;

        public EvolutionGeneticAlgorithm(List<Task> sample, IPopulationGenerator firstPopulationGenerator = null, IPrinter printer = null) : base(sample)
        {
            if (firstPopulationGenerator == null)
            {
                StartingPopulation = new PopulationByRandom(sample);
            }
            else
            {
                StartingPopulation = firstPopulationGenerator;
            }
            
            Reproductor = new FortuneWheelReproductor();
            Crossover = new FiftyFiftyCrossover();
            Mutator = new SymmetryMutator(MutationProbability);
            Selector = new TournamentSelector();

            if (printer == null)
            {
                this.Printer = new PrintableByNothing();
            }
            else
            {
                this.Printer = printer;
            }
        }

        public override List<Task> GetSolution()
        {
            Printer.Clear();
            Printer.Print("Algorithm will use:");
            Printer.Print("");
            Printer.Print("Global Parameters:");
            Printer.Print("     Population size = " + PopulationCount);
            Printer.Print("     Number of children = " + NumberOfChildren);
            Printer.Print("     Number of repeats to stop: " + NumberOfRepeats);
            Printer.Print("     Probability of mutations: " + MutationProbability);
            Printer.Print("Local Parameters:");
            Printer.Print("Starting Population: " + StartingPopulation.ToString());
            Printer.Print("     Reproductor - " + Reproductor.ToString());
            Printer.Print("     Crossover - " + Crossover.ToString());
            Printer.Print("     Mutator - " + Mutator.ToString());
            Printer.Print("     Selector - " + Selector.ToString());
            Printer.Print("------------------------------------------------------------");
            Printer.Print("Evolution algorithm begins!");

            StartTimeCount();

            // Creating first population
            List<Codestring> CurrentPopulation = StartingPopulation.GetPopulation(PopulationCount);
            Codestring answ = FindBestIndividual(CurrentPopulation);

            int counterOfRepeats = 0;

            while (counterOfRepeats < NumberOfRepeats)
            {
                // Reproduction
                for (int i = 0; i < NumberOfChildren; i++)
                {
                    Codestring FirstParent, SecondParent;
                    Reproductor.MakeReproduction(CurrentPopulation, out FirstParent, out SecondParent);
                    // Crossover
                    Codestring Child;
                    Crossover.MakeCrossover(FirstParent, SecondParent, out Child);
                    // Mutation
                    Mutator.MakeMutation(Child, out Child);

                    CurrentPopulation.Add(Child);
                }

                // Selection
                Selector.MakeSelection(CurrentPopulation, out CurrentPopulation);

                Codestring BestIndividual = FindBestIndividual(CurrentPopulation);
                if (BestIndividual.Criterium > answ.Criterium)
                {
                    answ = BestIndividual;
                }
                else
                {
                    if (BestIndividual.Criterium == answ.Criterium)
                    {
                        counterOfRepeats++;
                    }
                }
            }

            StopTimeCount();

            Printer.Print("Algorithm done in " + myStopWatch.Elapsed.ToString());
            Printer.Print("Solution is: ");
            Printer.Print(answ.Criterium.ToString());

            return answ.CurTasks;
        }

        private Codestring FindBestIndividual(List<Codestring> population)
        {
            Codestring bestIndividual = population[0];

            for (int i = 0; i < population.Count; i++)
            {
                if (bestIndividual.Criterium > population[i].Criterium)
                {
                    bestIndividual = population[i];
                }
            }

            return bestIndividual;
        }
        
        public static int GetPopulationCount() { return PopulationCount; }
    }
}
