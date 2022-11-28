using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class Machine 
    {
        public List<Task> Sample { get; }
        public string Worktime { get; } 
        private AMethod Method;
        public List<Task> Solution { get; }

        public int Count { get { return Sample.Count; } }

        public Machine(List<Task> Tasks, AMethod Method) {
            Sample = Tasks;
            this.Method = Method;
            Solution = this.GetSolution();
            Worktime = Method.myStopWatch.Elapsed.ToString();
        }

        private List<Task> GetSolution()
        {
            return Method.GetSolution();
        }
    }
}