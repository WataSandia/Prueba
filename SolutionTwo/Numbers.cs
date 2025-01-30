namespace SolutionTwo
{
    /// <summary>
    /// API Class to calculate a missing number in a collection.
    /// </summary>
    public class Numbers
    {
        /// <summary>
        /// A collection for saving the first 100 natural numbers.
        /// </summary>
        public readonly List<int> numbers;

        /// <summary>
        /// Initialize the list and populates it with the first 100 natural numbers.
        /// </summary>
        public Numbers()
        {
            numbers = Enumerable.Range(1, 100)
                .ToList();
        }

        /// <summary>
        /// Extracts a single number from a collection.
        /// The method can't be called twice.
        /// </summary>
        /// <param name="number">The number to extract from the collection.</param>
        /// <returns>True if the number was succefully removed</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool Extract(int number)
        {
            if (numbers.Count < 100)
                throw new InvalidOperationException("This method was called already.");

            if (number < 1)
                throw new ArgumentOutOfRangeException(nameof(number), number, "Input can't be less than 1.");         

            else if (number > 100)
                throw new ArgumentOutOfRangeException(nameof(number), number, "Input can't be greater than 100.");

            var isRemoved = numbers.Remove(number);
            return isRemoved;
        }

        /// <summary>
        /// Calculates the missing number from the collection.
        /// <see cref="Extract(int)"/> needs to be called first.
        /// </summary>
        /// <returns>The missing number.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public int Calculate()
        {
            if (numbers.Count is 100)
                throw new InvalidOperationException($"{Extract} method must be called first.");

            const int EXPECTED_SUM = 5050;
            int realSum = numbers.Sum();

            int missingNumber = EXPECTED_SUM - realSum;
            return missingNumber;
        }
    }
}
