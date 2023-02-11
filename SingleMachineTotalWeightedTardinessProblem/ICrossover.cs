namespace SingleMachineTotalWeightedTardinessProblem
{
    interface ICrossover
    {
        void MakeCrossover(Codestring firstParent, Codestring secondParent, out Codestring child);
    }
}
