using System.Windows.Forms;

namespace SingleMachineTotalWeightedTardinessProblem
{
    class PrintableByListBox : IPrinter
    {
        ListBox box;

        public PrintableByListBox(ListBox box)
        {
            this.box = box;
        }

        public void Print(string text)
        {
            box.Items.Add(text);
        }
    }
}
