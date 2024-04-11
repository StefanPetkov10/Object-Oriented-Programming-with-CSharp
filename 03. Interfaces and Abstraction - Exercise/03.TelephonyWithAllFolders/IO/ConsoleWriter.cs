

using _03.TelephonyWithAllFolders.IO.Interfaces;

namespace _03.TelephonyWithAllFolders.IO
{
    internal class ConsoleWriter : IWriter
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
