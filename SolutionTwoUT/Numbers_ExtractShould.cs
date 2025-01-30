using SolutionTwo;

namespace SolutionTwoUT
{
    public class Numbers_ExtractShould
    {
        [Fact]
        public void Extract_InputIsZero_ThrowsArgumentOutOfRangeException()
        {
            var numbers = new Numbers();

            const int ZERO_VALUE = 0;
            Func<object> actual = () => numbers.Extract(ZERO_VALUE);

            Assert.Throws<ArgumentOutOfRangeException>(actual);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Extract_InputIsNegative_ThrowsArgumentOutOfRangeException(int number)
        {
            var numbers = new Numbers();
            
            Func<object> actual = () => numbers.Extract(number);

            Assert.Throws<ArgumentOutOfRangeException>(actual);
        }

        [Theory]
        [InlineData(101)]
        [InlineData(int.MaxValue)]
        public void Extract_InputIsGreaterThan100_ThrowsArgumentOutOfRangeException(int number)
        {
            var numbers = new Numbers();

            Func<object> actual = () => numbers.Extract(number);

            Assert.Throws<ArgumentOutOfRangeException>(actual);
        }

        [Fact]
        public void Extract_MethodWasCalledAlready_ThrowsInvalidOperationException()
        {
            var numbers = new Numbers();

            const int NUMBER = 1;
            numbers.Extract(NUMBER);
         
            Func<object> actual = () => numbers.Extract(NUMBER);

            Assert.Throws<InvalidOperationException>(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(50)]
        [InlineData(100)]
        public void Extract_InputIsValid_ReturnsTrue(int number)
        {
            var numbers = new Numbers();

            var actual = numbers.Extract(number);

            Assert.True(actual);
        }
    }
}