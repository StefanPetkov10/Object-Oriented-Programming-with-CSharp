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

        protected Bank(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.clients = new List<IClient>();
            this.loans = new List<ILoan>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
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

        public double SumRates()
        {
            if (this.Loans.Count == 0)
            {
                return 0;
            }
            return double.Parse(this.Loans.Select(l => l.InterestRate).Sum().ToString());
        }

        //public double SumRates()
        //{
        //    if (this.Loans.Count == 0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        double sum = 0;
        //        foreach (var loan in this.Loans)
        //        {
        //            sum += loan.InterestRate;
        //        }
        //        return sum;
        //    }
        //}
        public void AddClient(IClient client)
        {
            if (this.Clients.Count < this.Capacity)
            {
                this.clients.Add(client);
            }
        }
        //public void AddClient(IClient Client)
        //{
        //    if (Clients.Count == Capacity)
        //    {
        //        throw new InvalidOperationException("Not enough capacity for this client.");
        //    }

        //    clients.Add(Client);
        //}
        public void RemoveClient(IClient Client)
        {
            clients.Remove(Client);
        }

        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }
        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {this.Name}, Type: {this.GetType().Name}");
            sb.Append($"Clients: ");

            if (this.clients.Count == 0)
            {
                sb.AppendLine("none");
            }
            else
            {
                var names = clients.Select(c => c.Name).ToArray();

                sb.AppendLine(string.Join(", ", names));
            }

            sb.AppendLine($"Loans: {this.loans.Count}, Sum of Rates: {this.SumRates()}");

            return sb.ToString().TrimEnd();
        }
        //public string GetStatistics()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine($"Name: {this.Name}, Type: {GetType().Name})");

        //    string printClients = clients.Any()
        //        ? string.Join(", ", clients.Select(c => c.Name))
        //        : "None";

        //    sb.AppendLine($"Clients: {printClients}");

        //    sb.AppendLine($"Loans: {this.Loans.Count}, Sum of Rates: {this.SumRates()}");

        //    return sb.ToString().TrimEnd();
        //}
    }
}
