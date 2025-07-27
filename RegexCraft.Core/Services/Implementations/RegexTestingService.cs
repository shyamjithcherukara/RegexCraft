using RegexCraft.Core.Models;
using RegexCraft.Core.Services.Contracts;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RegexCraft.Core.Services.Implementations;

/// <summary>
/// Provides services for testing and validating regular expressions against input strings,
/// including batch testing, pattern validation, and performance measurement.
/// </summary>
public class RegexTestingService : IRegexTestingService
{
    /// <summary>
    /// Tests a regular expression pattern against a specified test string and returns the result.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to test.</param>
    /// <param name="testString">The input string to test against the pattern.</param>
    /// <returns>
    /// A <see cref="RegexTestResult"/> containing the outcome of the test, including match information,
    /// execution time, and any error messages.
    /// </returns>
    public RegexTestResult TestPattern(string pattern, string testString)
    {
        if (string.IsNullOrEmpty(pattern))
        {
            return new RegexTestResult
            {
                Pattern = pattern,
                TestString = testString,
                IsValid = false,
                ErrorMessage = "Pattern cannot be null or empty."
            };
        }

        var result = new RegexTestResult
        {
            Pattern = pattern,
            TestString = testString
        };

        var stopwatch = Stopwatch.StartNew();

        try
        {
            var regex = new Regex(pattern, RegexOptions.Compiled, TimeSpan.FromSeconds(5));
            var matches = regex.Matches(testString);

            stopwatch.Stop();

            result.IsMatch = matches.Count > 0;
            result.Matches = matches.Cast<Match>().ToArray();
            result.ExecutionTime = stopwatch.Elapsed;
            result.IsValid = true;
        }
        catch (RegexMatchTimeoutException)
        {
            stopwatch.Stop();
            result.ErrorMessage = "Regex execution timeout (5 seconds exceeded)";
            result.IsValid = false;
            result.ExecutionTime = stopwatch.Elapsed;
        }
        catch (ArgumentException ex)
        {
            stopwatch.Stop();
            result.ErrorMessage = $"Invalid regex pattern: {ex.Message}";
            result.IsValid = false;
            result.ExecutionTime = stopwatch.Elapsed;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            result.ErrorMessage = $"Unexpected error: {ex.Message}";
            result.IsValid = false;
            result.ExecutionTime = stopwatch.Elapsed;
        }

        return result;
    }

    /// <summary>
    /// Tests a regular expression pattern against a batch of test strings and returns the results.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to test.</param>
    /// <param name="testStrings">An array of input strings to test against the pattern.</param>
    /// <returns>
    /// An array of <see cref="RegexTestResult"/> objects, each containing the outcome of the test for a corresponding input string.
    /// </returns>
    public RegexTestResult[] TestPatternBatch(string pattern, string[] testStrings)
    {
        if (testStrings == null)
            return null;

        return testStrings.Select(testString => TestPattern(pattern, testString)).ToArray();
    }

    /// <summary>
    /// Determines whether the specified regular expression pattern is valid.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to validate.</param>
    /// <returns>
    /// <c>true</c> if the pattern is valid; otherwise, <c>false</c>.
    /// </returns>
    public bool IsValidPattern(string pattern)
    {
        if (string.IsNullOrEmpty(pattern))
            return false;

        try
        {
            _ = new Regex(pattern);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Measures the average execution time of matching a regular expression pattern against a test string over a specified number of iterations.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to test.</param>
    /// <param name="testString">The input string to test against the pattern.</param>
    /// <param name="iterations">The number of times to repeat the match operation for averaging. Default is 1000.</param>
    /// <returns>
    /// The average <see cref="TimeSpan"/> taken per match operation, or <see cref="TimeSpan.Zero"/> if the pattern is invalid.
    /// </returns>
    public TimeSpan MeasurePerformance(string pattern, string testString, int iterations = 1000)
    {
        if (!IsValidPattern(pattern))
            throw new ArgumentException("Invalid regex pattern.", nameof(pattern));
        if (iterations <= 0)
            return TimeSpan.Zero;

        var regex = new Regex(pattern, RegexOptions.Compiled);
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < iterations; i++)
        {
            regex.IsMatch(testString);
        }

        stopwatch.Stop();
        return TimeSpan.FromTicks(stopwatch.Elapsed.Ticks / iterations);
    }
}
