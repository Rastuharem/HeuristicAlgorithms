using System;
using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class HillClimbingMethod : AMethod
    {
        private IPrinter printer;

        public HillClimbingMethod(List<Task> tasks, IPrinter printer) : base(tasks)
        {
            this.printer = printer;
        }

        public override List<Task> GetSolution()
        {
            this.StartTimeCount();

            List<Codestring> buf = new List<Codestring>();
            Codestring CurMin = CreateRandString(Sample);
            printer.Print("Берем начальную кодировку: ");
            buf.Add(CurMin);
            CodestringsOutput(buf);
            buf.Clear();
            List<Codestring> Sigma = GetSigma(CurMin, Sample);
            //printer.Print("Ее окрестность:");
            //CodestringsOutput(Sigma);
            while (Sigma.Count != 0)
            {
                Codestring MinSigma = SearchMinIn(Sigma);
                printer.Print("Берем кодировку с минимальной приспособленностью: ");
                buf.Add(MinSigma);
                CodestringsOutput(buf);
                buf.Clear();
                printer.Print(CurMin.Criterium + " > " + MinSigma.Criterium + "?");
                if (CurMin.Criterium > MinSigma.Criterium)
                {
                    CurMin = MinSigma;
                    Sigma = GetSigma(CurMin, Sample);
                    printer.Print("Да! Новая лучшая кодировка: ");
                    buf.Add(CurMin);
                    CodestringsOutput(buf);
                    buf.Clear();
                    //printer.Print("Новая окрестность:");
                    //CodestringsOutput(Sigma);
                }
                else
                {
                    for (int i = 0; i < Sigma.Count; i++)
                        if (MinSigma.Equals(Sigma[i]))
                            Sigma.RemoveAt(i);
                    printer.Print("Нет! Удаляем эту кодировку из окрестности...");
                    //printer.Print("Новая окрестность:");
                    //CodestringsOutput(Sigma);
                }
            }

            this.StopTimeCount();

            printer.Print("Кодировки кончились!");
            printer.Print("");
            printer.Print("Задача решена: Лучшая кодировка | Ее приспособленность:");
            buf.Add(CurMin);
            CodestringsOutput(buf);
            buf.Clear();
            printer.Print("");
            return CurMin.CurTasks;
        }

        private Codestring CreateRandString(List<Task> _sample)
        {
            printer.Print("Создаем случайную кодировку...");
            printer.Print("");
            Random rnd = new Random();
            int[] taskList = new int[_sample.Count];
            for (int i = 0; i < _sample.Count; i++)
                taskList[i] = i;
            for (int i = 0; i < rnd.Next(5, 100); i++)
            {
                int index1 = rnd.Next(0, _sample.Count);
                int index2 = rnd.Next(0, _sample.Count);
                int buf = taskList[index1];
                taskList[index1] = taskList[index2];
                taskList[index2] = buf;
            }
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < _sample.Count; i++)
                tasks.Add(_sample[taskList[i]]);
            return new Codestring(tasks, _sample);
        }
        private void CodestringsOutput(List<Codestring> searchfield)
        {
            foreach (Codestring cdstr in searchfield)
            {
                string buf = "";
                foreach (int value in cdstr.codestring)
                    buf += (value + 1).ToString() + ".";
                buf += "   |   " + cdstr.Criterium;
                printer.Print(buf);
            }
        }
        private List<Codestring> GetSigma(Codestring curmin, List<Task> _sample)
        {
            List<Codestring> Sigma = new List<Codestring>();
            for (int i = 0; i < curmin.CurTasks.Count; i++)
                for (int j = i + 1; j < curmin.CurTasks.Count; j++)
                    Sigma.Add(ReplaceTasks(curmin, i, j, _sample));
            return Sigma;
        }
        private Codestring ReplaceTasks(Codestring cdstr, int index1, int index2, List<Task> _sample)
        {
            List<int> newTasks = new List<int>(cdstr.codestring);
            int buf = newTasks[index1];
            newTasks[index1] = newTasks[index2];
            newTasks[index2] = buf;
            return new Codestring(newTasks, _sample);
        }
        private Codestring SearchMinIn(List<Codestring> sigma)
        {
            Random rnd = new Random();
            Codestring Max = sigma[rnd.Next(0, sigma.Count)];
            foreach (Codestring cdstr in sigma)
                if (cdstr.Criterium < Max.Criterium)
                    Max = cdstr;
            return Max;
        }
    }
}
