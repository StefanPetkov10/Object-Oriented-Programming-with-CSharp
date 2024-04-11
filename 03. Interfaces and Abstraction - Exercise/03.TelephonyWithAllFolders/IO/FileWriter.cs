using _03.TelephonyWithAllFolders.IO.Interfaces;


namespace _03.TelephonyWithAllFolders.IO
{
    public class FileWriter : IWriter
    {
        public void WriteLine(string line)
        {
            string filePath = "../../../text.text";

            using StreamWriter sw = new(filePath, true);

            sw.WriteLine(line);
        }
    }
}
