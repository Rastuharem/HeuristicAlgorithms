namespace SingleMachineTotalWeightedTardinessProblem
{
    interface IMutator
    {
        void MakeMutation(Codestring child, out Codestring mutatedChild);
    }
}
