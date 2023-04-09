namespace SingleMachineTotalWeightedTardinessProblem
{
    class Task 
    {
        public int t { get; } // Number of time segments needed to complete task
        public int d { get; } // Такт времени, к которому задача должна быть сделана (нет штрафов) Time segment, which after task should be done (no penalty)
        public int w { get; } // Penalty amount for one overdue time segment
        public string name { get; }

        public Task(string _name, int _t, int _d, int _w) {
            t = _t;
            d = _d;
            w = _w;
            name = "Task: " + _name;
        }
        public Task(int index, int _t, int _d, int _w) {
            t = _t;
            d = _d;
            w = _w;
            name = "Task: " + index.ToString();
        }
    }
}