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
        public void Test_Should_Throw_If_Annual_Salary_Is_Less_Than_Zero()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                var annualTax = _taxCalculator.CalculateTax(-1);
            });

            Assert.Equal("Annual salary should be greater than or equal to 0", exception.Message);
        }

        [Fact]
        public void Test_Should_Throw_If_Tax_Bracket_Not_Found()
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                var annualTax = _taxCalculator.CalculateTax(1_100_000_000);
            });

            Assert.Equal("No tax bracket found for the specified annual amount", exception.Message);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(250_000, 0)]
        [InlineData(350_000, 15_000)]
        [InlineData(400_000, 22_500)]
        [InlineData(450_000, 32_500)]
        [InlineData(800_000, 102_500)]
        [InlineData(1_000_000, 152_500)]
        public void Test_Should_Return_Correct_Tax_Amount(decimal annualSalary, decimal expectedTax)
        {
            var annualTax = _taxCalculator.CalculateTax(annualSalary);

            Assert.Equal(expectedTax, annualTax);
        }
    }
}