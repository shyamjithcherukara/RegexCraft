using RegexCraft.Core.Models;

namespace RegexCraft.Core.Services.Contracts
{
    /// <summary>
    /// Provides methods for testing, validating, and measuring the performance of regular expressions.
    /// </summary>
    public interface IRegexTestingService
    {
        /// <summary>
        /// Tests a regular expression pattern against a single test string.
        /// </summary>
        /// <param name="pattern">The regular expression pattern to test.</param>
        /// <param name="testString">The input string to test against the pattern.</param>
        /// <returns>
        /// A <see cref="RegexTestResult"/> containing the outcome of the test, including match results and any errors.
        /// </returns>
        RegexTestResult TestPattern(string pattern, string testString);

        /// <summary>
        /// Tests a regular expression pattern against multiple test strings in a batch operation.
        /// </summary>
        /// <param name="pattern">The regular expression pattern to test.</param>
        /// <param name="testStrings">An array of input strings to test against the pattern.</param>
        /// <returns>
        /// An array of <see cref="RegexTestResult"/> objects, each representing the result for a corresponding test string.
        /// </returns>
        RegexTestResult[] TestPatternBatch(string pattern, string[] testStrings);

        /// <summary>
        /// Determines whether the specified regular expression pattern is valid.
        /// </summary>
        /// <param name="pattern">The regular expression pattern to validate.</param>
        /// <returns>
        /// <c>true</c> if the pattern is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidPattern(string pattern);

        /// <summary>
        /// Measures the performance of a regular expression pattern against a test string over a specified number of iterations.
        /// </summary>
        /// <param name="pattern">The regular expression pattern to test.</param>
        /// <param name="testString">The input string to test against the pattern.</param>
        /// <param name="iterations">The number of times to execute the match for performance measurement. Default is 1000.</param>
        /// <returns>
        /// A <see cref="TimeSpan"/> representing the total time taken to execute the match for the specified number of iterations.
        /// </returns>
        TimeSpan MeasurePerformance(string pattern, string testString, int iterations = 1000);
    }
}
