using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    abstract class AMethod
    {
        public List<Task> Sample { get; }
        public System.Diagnostics.Stopwatch myStopWatch { get; }
        public int SampleCount { get { return Sample.Count; } }

        public AMethod(List<Task> tasks)
        {
            this.Sample = tasks;
            myStopWatch = new System.Diagnostics.Stopwatch();
        }

        public abstract List<Task> GetSolution();

        protected void StartTimeCount()
        {
            myStopWatch.Start();
        }
        protected void StopTimeCount()
        {
            myStopWatch.Stop();
        }
    }
}
