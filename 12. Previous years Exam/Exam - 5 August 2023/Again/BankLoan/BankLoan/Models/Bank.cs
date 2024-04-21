using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity;

        private List<IClient> clients;
        private List<ILoan> loans;

        public Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            clients = new List<IClient>();
            loans = new List<ILoan>();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BankNameNullOrWhiteSpace));
                }
                name = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set => capacity = value;
        }

        public IReadOnlyCollection<ILoan> Loans => loans;


        public IReadOnlyCollection<IClient> Clients => clients;

        public void AddClient(IClient Client)
        {
            if (clients.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }
            clients.Add(Client);
        }

        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {this.Name}, Type: {GetType().Name}");

            string printClients = clients.Any()
                ? string.Join(", ", clients.Select(c => c.Name))
                : "none";

            sb.AppendLine($"Clients: {printClients}");

            sb.AppendLine($"Loans: {this.Loans.Count}, Sum of Rates: {this.SumRates()}");

            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient Client)
        {
            clients.Remove(Client);
        }

        public double SumRates()
        {
            if (this.Loans.Count == 0)
            {
                return 0;
            }
            return double.Parse(this.Loans.Select(l => l.InterestRate).Sum().ToString());
        }
    }
}
