using _03.TelephonyWithAllFolders.Core.Interfaces;
using _03.TelephonyWithAllFolders.Models.Interfaces;
using _03.TelephonyWithAllFolders.Models;
using _03.TelephonyWithAllFolders.IO.Interfaces;

namespace _03.TelephonyWithAllFolders.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string[] phoneNumbers = reader.ReadLine().Split();
            string[] urls = reader.ReadLine().Split();

            ICallable phone;

            foreach (var phoneNumber in phoneNumbers)
            {
                if (phoneNumber.Length == 10)
                {
                    phone = new Smartphone();
                }
                else
                {
                    phone = new StationaryPhone();
                }

                try
                {
                    writer.WriteLine(phone.Call(phoneNumber));
                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            IBrowsable browser = new Smartphone();

            foreach (var url in urls)
            {
                try
                {
                    writer.WriteLine(browser.Browse(url));
                }
                catch (ArgumentException ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}

