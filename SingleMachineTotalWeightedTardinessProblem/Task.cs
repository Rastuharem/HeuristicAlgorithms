namespace SingleMachineTotalWeightedTardinessProblem
{
    class Task 
    {
        public int t { get; } // Кол-во тактов времени требуемое для выполнения задачи
        public int d { get; } // Такт времени, к которому задача должна быть сделана (нет штрафов)
        public int w { get; } // Величина штрафа за единицу просроченного такта времени
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