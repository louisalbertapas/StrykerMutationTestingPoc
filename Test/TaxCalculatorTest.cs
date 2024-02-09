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

        [Theory]
        [InlineData(0, 0)]
        [InlineData(250000, 0)]
        [InlineData(350000, 15000)]
        [InlineData(400000, 22500)]
        [InlineData(450000, 32500)]
        [InlineData(800000, 102500)]
        [InlineData(1000000, 152500)]
        public void Test_Should_Return_Correct_Tax_Amount(decimal annualSalary, decimal expectedTax)
        {
            var annualTax = _taxCalculator.CalculateTax(annualSalary);

            Assert.Equal(expectedTax, annualTax);
        }
    }
}