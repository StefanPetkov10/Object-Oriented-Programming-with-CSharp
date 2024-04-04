using _03.TelephonyWithAllFolders.Core;
using _03.TelephonyWithAllFolders.Core.Interfaces;
using _03.TelephonyWithAllFolders.IO;

ConsoleReader reader = new();
//ConsoleWriter consoleWriter = new();
FileWriter writer = new();

IEngine engine = new Engine(reader, writer);

engine.Run();

