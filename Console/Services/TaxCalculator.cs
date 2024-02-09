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
                new(250_000.01m, 400_000, 0, 15),
                new(400_000.01m, 800_000, 22_500, 20),
                new(800_000.01m, 2_000_000, 102_500, 25),
                new(2_000_000.01m, 8_000_000m, 402_500, 30),
                new(8_000_000.01m, decimal.MaxValue, 2_202_500, 35)
            }.AsEnumerable();
        }

        public decimal CalculateTax(decimal annualSalary)
        {
            if (annualSalary < 0)
            {
                throw new InvalidOperationException("Annual salary should be greater than or equal to 0");
            }

            var taxBracket = _taxBrackets
                .Where(bracket => annualSalary >= bracket.Min && annualSalary <= bracket.Max)
                .SingleOrDefault();

            if (taxBracket is null)
            {
                throw new InvalidOperationException($"The annual salary {annualSalary} does not belong to any tax bracket.");
            }

            return taxBracket.FixTax + (annualSalary - taxBracket.Min) * taxBracket.ExcessRate / 100;
        }
    }

    internal class TaxBracket
    {
        public decimal Min { get; init; }
        public decimal Max { get; init; }
        public decimal FixTax { get; init; }
        public decimal ExcessRate { get; init; }

        public TaxBracket(decimal min, decimal max, decimal fixTax, decimal excessRate)
        {
            Min = min;
            Max = max;
            FixTax = fixTax;
            ExcessRate = excessRate;
        }
    }
}
