using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class Codestring 
    {
        public List<Task> CurTasks { get; }
        public List<Task> Sample { get; }
        public int Criterium { get; }
        public List<int> codestring { get; }

        public Codestring(List<int> CodeStr, List<Task> sample) {
            Sample = sample;
            codestring = CodeStr;
            CurTasks = Uncode(CodeStr, sample);
            Criterium = CriteriumCount(Uncode(CodeStr, sample));
        }
        public Codestring(List<Task> tasks, List<Task> sample) {
            Sample = sample;
            CurTasks = tasks;
            codestring = CreateCdstr(tasks, sample);
            Criterium = CriteriumCount(tasks);
        }

        private List<int> CreateCdstr(List<Task> tasks, List<Task> sample) {
            List<int> codestring = new List<int>();
            for (int i = 0; i < tasks.Count; i++)
                for (int j = 0; j < tasks.Count; j++)
                    if (tasks[j] == sample[i])
                        codestring.Add(j);
            return codestring;
        }
        private int CriteriumCount(List<Task> tasks) {
            int curTime = 0;
            int Criterium = 0;
            for (int i = 0; i < tasks.Count; i++) {
                curTime += tasks[i].t;
                if (curTime > tasks[i].d)
                    Criterium += tasks[i].w * (curTime - tasks[i].d);
            }
            return Criterium;
        }
        private List<Task> Uncode(List<int> codestring, List<Task> sample) {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < codestring.Count; i++)
                tasks.Add(sample[codestring[i]]);
            return tasks;
        }
    }
}