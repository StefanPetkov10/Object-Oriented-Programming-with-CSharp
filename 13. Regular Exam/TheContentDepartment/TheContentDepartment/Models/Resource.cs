using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models
{
    public abstract class Resource : IResource
    {
        private string name;
        private string creator;
        private int priority;

        private bool isTested = false;
        private bool isApproved = false;

        public Resource(string name, string creator, int priority)
        {
            this.Name = name;
            this.Creator = creator;
            this.Priority = priority;
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(string.Format(ExceptionMessages.NameNullOrWhiteSpace));
                }
                this.name = value;
            }
        }

        public string Creator
        {
            get => this.creator;
            private set => this.creator = value;
        }

        public int Priority
        {
            get => this.priority;
            private set => this.priority = value;
        }

        public bool IsTested
        {
            get { return isTested; }
            private set { isTested = value; }
        }

        public bool IsApproved
        {
            get { return isApproved; }
            private set { isApproved = value; }
        }

        public void Approve()
        {
            IsApproved = true;
        }

        public void Test()
        {
            IsTested = !IsTested;
        }

        public override string ToString()
        {
            return $"{Name} ({GetType().Name}), Created By: {Creator}";
        }
    }
}
