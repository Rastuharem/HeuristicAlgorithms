using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SingleMachineTotalWeightedTardinessProblem
{
    public partial class Form1 : Form
    {
        private static List<Task> Sample = new List<Task>();
        private static IPrinter printer;

        public Form1() {
            InitializeComponent();
            //listBox1.Visible = false;
            textBox1.Text = "Откройте файл!!!!";
            listBox1.Items.Add("------------------ КОНСОЛЬ РАЗРАБОТЧИКА -------------------");
            listBox1.Items.Add("");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            printer = new PrintableByListBox(listBox1);
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Sample.Clear();
                    dataGridView1.Rows.Clear();
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        int TaskNum = Convert.ToInt32(sr.ReadLine());
                        for (int i = 0; i < TaskNum; i++)
                        {
                            string line = sr.ReadLine();
                            string[] Data = line.Split(' ');
                            Sample.Add(new Task(Data[0], Convert.ToInt32(Data[1]), Convert.ToInt32(Data[3]), Convert.ToInt32(Data[2])));
                        }
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("Не удалось прочитать файл: " + ex.Message);
                    printer.Print("Неудачная попытка открыть файл...");
                    printer.Print("");
                    return;
                }
                dataGridView1.Rows.Add(Sample.Count);
                for (int i = 0; i < Sample.Count; i++) {
                    dataGridView1.Rows[i].Cells[0].Value = Sample[i].name;
                    dataGridView1.Rows[i].Cells[1].Value = Sample[i].t;
                    dataGridView1.Rows[i].Cells[2].Value = Sample[i].d;
                    dataGridView1.Rows[i].Cells[3].Value = Sample[i].w;
                    dataGridView1.Rows[i].Cells[4].Value = '-';
                    dataGridView1.Rows[i].Cells[5].Value = '-';
                }
                dataGridView1.CurrentCell.Selected = false;
                SwitchButtons(true);

                printer.Clear();
                printer.Print("Файл успешно открыт...");
                printer.Print("");
                textBox1.Text = "";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            файлToolStripMenuItem_Click(файлToolStripMenuItem, new EventArgs());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printer.Clear();
            var TaskSolution = new Machine(Sample, new HillClimbingMethod(Sample, new PrintableByNothing()));
            SolutionOutput(TaskSolution, dataGridView1, textBox1, listBox1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            printer.Clear();
            if (Sample.Count <= 10)
            {
                var TaskSolution = new Machine(Sample, new EnumMethod(Sample, new PrintableByNothing()));
                SolutionOutput(TaskSolution, dataGridView1, textBox1, listBox1);
            }
            else
                MessageBox.Show("Кол-во задач больше 10!");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            printer.Clear();
            var TaskSolution = new Machine(Sample, new AnnealingSimulatonMethod(Sample, new PrintableByNothing()));
            SolutionOutput(TaskSolution, dataGridView1, textBox1, listBox1);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            printer.Clear();

            EvolutionGeneticAlgorithm.PopulationCount = 300;
            EvolutionGeneticAlgorithm.NumberOfRepeats = 10;
            EvolutionGeneticAlgorithm.NumberOfChildren = EvolutionGeneticAlgorithm.PopulationCount;
            EvolutionGeneticAlgorithm.MutationProbability = 0.3;

            var TaskSolution = new Machine(Sample, new EvolutionGeneticAlgorithm(Sample));
            SolutionOutput(TaskSolution, dataGridView1, textBox1, listBox1);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            printer.Clear();

            EvolutionGeneticAlgorithm.PopulationCount = 100;
            EvolutionGeneticAlgorithm.NumberOfRepeats = 10;
            EvolutionGeneticAlgorithm.NumberOfChildren = 300;
            EvolutionGeneticAlgorithm.MutationProbability = 0.3;

            IPopulationGenerator generator = new PopulationByMethod(new HillClimbingMethod(Sample));
            var TaskSolution = new Machine(Sample, new EvolutionGeneticAlgorithm(Sample, generator));
            SolutionOutput(TaskSolution, dataGridView1, textBox1, listBox1);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            printer.Clear();

            EvolutionGeneticAlgorithm.PopulationCount = 200;
            EvolutionGeneticAlgorithm.NumberOfRepeats = 10;
            EvolutionGeneticAlgorithm.NumberOfChildren = EvolutionGeneticAlgorithm.PopulationCount;
            EvolutionGeneticAlgorithm.MutationProbability = 0.3;

            IPopulationGenerator generator = new PopulationByMethod(new AnnealingSimulatonMethod(Sample));
            var TaskSolution = new Machine(Sample, new EvolutionGeneticAlgorithm(Sample, generator));
            SolutionOutput(TaskSolution, dataGridView1, textBox1, listBox1);
        }

        private void SwitchButtons(bool swithcer)
        {
            button1.Enabled = swithcer;
            button2.Enabled = swithcer;
            button3.Enabled = swithcer;
            button4.Enabled = swithcer;
            button5.Enabled = swithcer;
            button6.Enabled = swithcer;
        }

        private static void SolutionOutput(Machine TaskSolution, DataGridView dataGridView1, TextBox textBox1, ListBox listBox1)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(TaskSolution.Count);
            int curt = 0;

            for (int i = 0; i < TaskSolution.Count; i++)
            {
                int curw = 0;
                curt += TaskSolution.Solution[i].t;
                if (TaskSolution.Solution[i].d < curt)
                    curw = (curt - TaskSolution.Solution[i].d) * TaskSolution.Solution[i].w;
                dataGridView1.Rows[i].Cells[0].Value = TaskSolution.Solution[i].name;
                dataGridView1.Rows[i].Cells[1].Value = TaskSolution.Solution[i].t;
                dataGridView1.Rows[i].Cells[2].Value = TaskSolution.Solution[i].d;
                dataGridView1.Rows[i].Cells[3].Value = TaskSolution.Solution[i].w;
                dataGridView1.Rows[i].Cells[4].Value = i + 1;
                dataGridView1.Rows[i].Cells[5].Value = curw;
            }
            dataGridView1.CurrentCell.Selected = false;
            textBox1.Text = "Задача решена: наименьшее найденное суммарное взвешивание: " + (new Codestring(TaskSolution.Solution, Sample).Criterium);
            textBox1.Text += " Время выполнения алгоритма: " + TaskSolution.Worktime;
            listBox1.Items.Add("Время выполнения алгоритма: " + TaskSolution.Worktime);

            string AverageDispersion = "";

            if (TaskSolution.Count <= 10)
            {
                var Machine = new Machine(TaskSolution.Sample, new EnumMethod(TaskSolution.Sample, new PrintableByNothing()));
                double accurateSol = new Codestring(Machine.Solution, Sample).Criterium;
                double approximateSol = new Codestring(TaskSolution.Solution, Sample).Criterium;
                AverageDispersion = CountAverageDispersion(accurateSol, approximateSol).ToString();
                listBox1.Items.Add("Абсолютное отклонение: " + AverageDispersion + "%.");
                listBox1.Items.Add("Точное решение: " + accurateSol);
            }
            else
            {
                AverageDispersion = "Невозможно считать относительное отклонение, т.к. кол-во задач больше 10";
            }
            listBox1.Items.Add("Ответы выведены пользователю...");
            listBox1.Items.Add("");
        }

        private static double CountAverageDispersion(double accurateSol, double approximateSol)
        {
            return (approximateSol - accurateSol) / Sample.Count;
        }
    }
}
