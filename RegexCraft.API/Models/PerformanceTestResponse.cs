namespace RegexCraft.API.Models;

/// <summary>
/// Represents the result of a performance test for a regular expression pattern.
/// </summary>
public record PerformanceTestResponse
{
    /// <summary>
    /// Gets the regular expression pattern that was tested.
    /// </summary>
    public string Pattern { get; init; } = string.Empty;

    /// <summary>
    /// Gets the input string used for testing the pattern.
    /// </summary>
    public string TestString { get; init; } = string.Empty;

    /// <summary>
    /// Gets the number of iterations the test was executed.
    /// </summary>
    public int Iterations { get; init; }

    /// <summary>
    /// Gets the execution time in milliseconds for a single test run.
    /// </summary>
    public double SingleExecutionTimeMs { get; init; }

    /// <summary>
    /// Gets the average execution time in milliseconds across all iterations.
    /// </summary>
    public double AverageExecutionTimeMs { get; init; }

    /// <summary>
    /// Gets the number of operations performed per second.
    /// </summary>
    public double OperationsPerSecond { get; init; }

    /// <summary>
    /// Gets a value indicating whether the pattern matched the test string.
    /// </summary>
    public bool IsMatch { get; init; }

    /// <summary>
    /// Gets the performance rating for the test (e.g., "Fast", "Moderate", "Slow").
    /// </summary>
    public string PerformanceRating { get; init; } = string.Empty;
}