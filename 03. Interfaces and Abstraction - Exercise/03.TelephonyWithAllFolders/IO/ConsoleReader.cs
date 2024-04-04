using _03.TelephonyWithAllFolders.IO.Interfaces;


namespace _03.TelephonyWithAllFolders.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
