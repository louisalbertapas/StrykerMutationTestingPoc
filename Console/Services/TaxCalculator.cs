namespace ConsoleApp.Services
{
    public class TaxCalculator
    {
        private readonly IEnumerable<TaxBracket> _taxBrackets;

        public TaxCalculator()
        {
            _taxBrackets = new List<TaxBracket>
            {
                new(0, 250_000, 0, 0),
                new(250_000, 400_000, 0, 15),
                new(400_000, 800_000, 22_500, 20),
                new(800_000, 2_000_000, 102_500, 25),
                new(2_000_000, 8_000_000, 402_500, 30),
                new(8_000_000, 1_000_000_000, 2_202_500, 35)
            }.AsEnumerable();
        }

        public decimal CalculateTax(decimal annualSalary)
        {
            if (annualSalary < 0)
            {
                throw new InvalidOperationException("Annual salary should be greater than or equal to 0");
            }

            var taxBracket = _taxBrackets
                .Where(bracket => annualSalary >= bracket.MinSalary && annualSalary < bracket.MaxSalary)
                .SingleOrDefault();

            if (taxBracket is null)
            {
                throw new InvalidOperationException("No tax bracket found for the specified annual amount");
            }

            return taxBracket.FixTax + (annualSalary - taxBracket.MinSalary) * taxBracket.ExcessRate / 100;
        }
    }

    internal class TaxBracket
    {
        public decimal MinSalary { get; init; }
        public decimal MaxSalary { get; init; }
        public decimal FixTax { get; init; }
        public decimal ExcessRate { get; init; }

        public TaxBracket(decimal min, decimal max, decimal fixTax, decimal excessRate)
        {
            MinSalary = min;
            MaxSalary = max;
            FixTax = fixTax;
            ExcessRate = excessRate;
        }
    }
}
