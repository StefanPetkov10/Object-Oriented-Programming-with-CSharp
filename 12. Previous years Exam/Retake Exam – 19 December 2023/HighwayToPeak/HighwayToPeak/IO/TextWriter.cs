using HighwayToPeak.IO.Contracts;

namespace HighwayToPeak.IO
{
    public class TextWriter : IWriter
    {
        private string path = "../../../output.txt";
        public void Write(string text)
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.Write(text);
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

