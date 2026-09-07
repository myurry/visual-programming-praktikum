using PizzaSplit.Core;


namespace PizzaSplit.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
			try
			{
                Console.WriteLine("Welcome to Pizza Split!");
                Console.WriteLine("Enter cost per person (example: 15,50): ");
                float costPerPerson = float.Parse(Console.ReadLine());
                Console.WriteLine("Enter number of people: ");
                int n = int.Parse(Console.ReadLine());
                Console.WriteLine("Add 10% tip? (yes/no): ");
                bool tip = string.Equals(Console.ReadLine(), "yes");

                float result = BillCalculator.EvaluateBill(costPerPerson, n, tip);
                Console.WriteLine($"Evaluated Bill: {result}");
            }
			catch (Exception)
			{
                Console.WriteLine("An error occurred while evaluating the bill. Please check your input and try again.");
                throw;
			}
        }
    }
}
