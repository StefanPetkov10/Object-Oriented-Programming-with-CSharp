using BankLoan.IO.Contracts;
using System.IO;

namespace BankLoan.IO
{
    public class TextWriter : IWriter
    {
        private string path = "../../../Output.txt";

        public void Write(string text)
        {
            //System.IO.File.AppendAllText(path, text);

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(text);
            }
        }

        public void WriteLine(string text)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(text);
            }
        }
    }
}
