
namespace PersonsInfo
{
    public class Person
    {

        private string firstName;
        private string lastName;
        private int age;
        private decimal salary;
        public Person(string firstName, string lastName, int age, decimal salary)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Salary = salary;
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            private set
            {
                if (value != null && value.Length < 3)
                {
                    throw new System.ArgumentException("First name cannot contain fewer than 3 symbols!");
                }
                this.firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            private set
            {
                if (value != null && value.Length < 3)
                {
                    throw new System.ArgumentException("Last name cannot contain fewer than 3 symbols!");
                }
                this.lastName = value;
            }
        }
        public int Age
        {
            get
            {
                return this.age;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Age cannot be zero or a negative integer!");
                }

                this.age = value;
            }
        }

        public decimal Salary
        {
            get
            {
                return this.salary;
            }
            private set
            {
                if (value < 650)
                {
                    throw new ArgumentException("Salary cannot be less than 650 leva!");
                }

                this.salary = value;
            }
        }
       

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} receives {Salary:F2} leva.";
        }
    }
}
