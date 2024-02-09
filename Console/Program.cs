using ConsoleApp.Services;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter annual salary: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal annualSalary))
            {
                Console.WriteLine("Annual salary must be a number");
                return;
            }

            var taxCalculator = new TaxCalculator();
            Console.WriteLine($"Tax is {taxCalculator.CalculateTax(annualSalary)}");
        }
    }
}