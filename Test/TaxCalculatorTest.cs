using ConsoleApp.Services;

namespace Test
{
    public class TaxCalculatorTest
    {
        private readonly TaxCalculator _taxCalculator;

        public TaxCalculatorTest()
        {
            _taxCalculator = new TaxCalculator();
        }

        [Fact]
        public void Test_If_AnnualSalary_Is_LessThan_Zero()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                var annualTax = _taxCalculator.CalculateTax(-1);
            });

            Assert.Equal("Annual salary should be greater than or equal to 0", exception.Message);
        }
    }
}