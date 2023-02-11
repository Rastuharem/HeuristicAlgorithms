using System;
using System.Collections.Generic;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class EnumMethod : AMethod
    {
        IPrinter printer;

        public EnumMethod(List<Task> tasks, IPrinter printer = null) : base(tasks)
        {
            if (printer == null)
            {
                this.printer = new PrintableByNothing();
            }
            else
            {
                this.printer = printer;
            }
        }

        public override List<Task> GetSolution()
        {
            printer.Print("Создаем все кодировки...");
            printer.Print("");

            this.StartTimeCount();

            var AllCodes = ShowAllCombinations<int>(new Codestring(Sample, Sample).codestring);
            var Floor = new List<Codestring>();
            for (int i = 0; i < AllCodes.Count; i++)
                Floor.Add(new Codestring(TranslateToListIntFromString(AllCodes[i]), Sample));
            var answ = Floor[0];
            for (int i = 1; i < Floor.Count; i++)
                if (Floor[i].Criterium < answ.Criterium)
                    answ = Floor[i];

            this.StopTimeCount();

            return answ.CurTasks;
        }

        private List<string> ShowAllCombinations<T>(IList<T> arr, List<string> list = null, string current = "")
        {
            if (list == null) list = new List<string>();
            if (arr.Count == 0)
            {
                list.Add(current);
                return list;
            }
            for (int i = 0; i < arr.Count; i++)
            {
                List<T> lst = new List<T>(arr);
                lst.RemoveAt(i);
                ShowAllCombinations(lst, list, current + arr[i].ToString() + '.');
            }
            return list;
        }
        private List<int> TranslateToListIntFromString(string str)
        {
            List<int> CodeStr = new List<int>();
            string buf = "";
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j] != '.')
                    buf += str[j];
                else
                {
                    CodeStr.Add(Convert.ToInt32(buf));
                    buf = "";
                }
            }
            return CodeStr;
        }
    }
}
