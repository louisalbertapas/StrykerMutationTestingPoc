namespace ConsoleApp.Services
{
    public class TaxCalculator
    {
        private readonly IEnumerable<TaxBracket> _taxBrackets;

        public TaxCalculator()
        {
            _taxBrackets = new List<TaxBracket>
            {
                new(0, 250_000m, 0, 0),
                new(250_000m, 400_000m, 0, 15m),
                new(400_000m, 800_000m, 22_500m, 20m),
                new(800_000m, 2_000_000m, 102_500m, 25m),
                new(2_000_000m, 8_000_000m, 402_500m, 30m),
                new(8_000_000m, decimal.MaxValue, 2_202_500m, 35m)
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
                .Single();

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
