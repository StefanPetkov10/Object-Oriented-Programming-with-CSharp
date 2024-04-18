using VehiclesExtension.IO.Interfaces;

namespace VehiclesExtension.IO;

public class FileWriter : IWriter
{
    public void WriteLine(string str)
    {
        using StreamWriter writer = new("D:\\test.txt", true);

        writer.WriteLine(str);
    }
}