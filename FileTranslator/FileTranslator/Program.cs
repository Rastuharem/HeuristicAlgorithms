using System;
using System.Collections.Generic;
using System.IO;

namespace FileTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] buf_t = { }, buf_d = { }, buf_w = { };
            string pathRead = "C:\\Users\\Admin\\source\\repos\\SingleMachineTotalWeightedTardinessProblem\\FileTranslator\\FileTranslator\\RawTasks.txt";
            string pathWrite = "C:\\Users\\Admin\\source\\repos\\SingleMachineTotalWeightedTardinessProblem\\SingleMachineTotalWeightedTardinessProblem\\MachineData\\MachineData40.txt";
            try
            {
                using (StreamReader sr = new StreamReader(pathRead))
                {
                    buf_t = (sr.ReadLine()).Split(' ');
                    buf_d = (sr.ReadLine()).Split(' ');
                    buf_w = (sr.ReadLine()).Split(' ');
                }
                Console.WriteLine("Прочитано успешно!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка прочтения файла: " + e.Message);
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(pathWrite))
                {
                    sw.Write(buf_w.Length);
                    for (int i = 0; i < buf_t.Length; i++)
                    {
                        sw.WriteLine();
                        sw.Write(i + 1);
                        sw.Write(' ');
                        sw.Write(buf_t[i]);
                        sw.Write(' ');
                        sw.Write(buf_d[i]);
                        sw.Write(' ');
                        sw.Write(buf_w[i]);
                    }
                }
                Console.WriteLine("Записано успешно!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка записи в файл: " + e.Message);
            }
        }
    }
}
