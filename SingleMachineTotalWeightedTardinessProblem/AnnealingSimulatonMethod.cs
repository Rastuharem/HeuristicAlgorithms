using System;
using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class AnnealingSimulatonMethod : AMethod
    {
        private const double InitialTemperature = 10000;
        private const double MinimalTemperature = 0.15;

        private IPrinter printer;

        public AnnealingSimulatonMethod(List<Task> tasks, IPrinter printer) : base(tasks)
        {
            this.printer = printer;
        }

        public override List<Task> GetSolution()
        {
            this.StartTimeCount();

            double T = InitialTemperature;
            List<Codestring> buf = new List<Codestring>();
            Codestring CurMin = CreateRandString(Sample);

            printer.Print("Начальная температура:" + T);
            printer.Print("Берем начальную кодировку: ");
            buf.Add(CurMin);
            CodestringsOutput(buf);
            buf.Clear();
            int index = 0;
            while (T > MinimalTemperature)
            {
                index++;
                Codestring PotentialMin = GenerateNewCondition(CurMin);

                printer.Print("Переходим в новое состояние по функции перехода: ");
                buf.Add(PotentialMin);
                CodestringsOutput(buf);
                buf.Clear();
                printer.Print(CurMin.Criterium + " > " + PotentialMin.Criterium + "?");

                if (PotentialMin.Criterium < CurMin.Criterium)
                {
                    CurMin = PotentialMin;

                    printer.Print("Да! Новая лучшая кодировка: ");
                    buf.Add(CurMin);
                    CodestringsOutput(buf);
                    buf.Clear();
                }
                else
                {
                    double P = GetTransitionProbability(PotentialMin.Criterium - CurMin.Criterium, T);
                    printer.Print("Нет. Считаем вероятность перехода - она равна = " + P);
                    if (MakeTransition(P))
                    {
                        CurMin = PotentialMin;

                        printer.Print("Повезло - переходим в новое состояние: ");
                        buf.Add(CurMin);
                        CodestringsOutput(buf);
                        buf.Clear();
                    }
                    else
                    {
                        printer.Print("Не повезло - состояние остается прежним :(");
                    }
                }
                T = DecreaseTemperature(T, index);
                printer.Print("Температура уменьшилась - новая температура: " + T);
            }

            this.StopTimeCount();

            printer.Print("Температура достигла минимума!");
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
        private Codestring ReplaceTasks(Codestring cdstr, int index1, int index2, List<Task> _sample)
        {
            List<int> newTasks = new List<int>(cdstr.codestring);
            int buf = newTasks[index1];
            newTasks[index1] = newTasks[index2];
            newTasks[index2] = buf;
            return new Codestring(newTasks, _sample);
        }

        private double DecreaseTemperature(double T, int index)
        {
            return InitialTemperature * 0.1 / (double)index;
        }
        private Codestring GenerateNewCondition(Codestring codestring)
        {
            Random rnd = new Random();
            int index1 = rnd.Next(0, SampleCount);
            int index2 = rnd.Next(0, SampleCount);
            return ReplaceTasks(codestring, index1, index2, Sample);
        }
        private double GetTransitionProbability(double deltaE, double T)
        {
            return Math.Exp(-deltaE / T);
        }
        private bool MakeTransition(double P)
        {
            Random rnd = new Random();
            double value = rnd.NextDouble();
            if (P < value)
                return false;
            else
                return true;
        }
    }
}
