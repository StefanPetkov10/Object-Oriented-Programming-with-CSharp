namespace ShoppingSpree.Models
{
    public class ExceptionMessages
    {
        public static void NameEmpty()
        {
            throw new ArgumentException("Name cannot be empty");
        }

        public static void MoneyNegative()
        {
            throw new ArgumentException("Money cannot be negative");
        }
    }
}