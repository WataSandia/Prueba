using SolutionTwo;

namespace SolutionTwoUT
{
    public class Numbers_CalculateShould
    {
        [Fact]
        public void Calculate_ExtractMethodWasNotCalledFirst_ThrowsInvalidOperationException()
        {
            var numbers = new Numbers();

            Func<object> actual = () => numbers.Calculate();

            Assert.Throws<InvalidOperationException>(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(25)]
        [InlineData(73)]
        [InlineData(100)]
        public void Calculate_CalculteMissingValue_ReturnsMissingNumber(int number)
        {
            var numbers = new Numbers();

            numbers.Extract(number);
            var actual = numbers.Calculate();

            Assert.Equal(number, actual);
        }
    }
}
