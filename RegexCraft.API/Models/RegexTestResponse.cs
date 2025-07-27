namespace RegexCraft.API.Models;

/// <summary>
/// Represents the result of testing a regular expression against a test string.
/// </summary>
public record RegexTestResponse
{
    /// <summary>
    /// Gets the regular expression pattern used for testing.
    /// </summary>
    public string Pattern { get; init; } = string.Empty;

    /// <summary>
    /// Gets the input string that was tested against the pattern.
    /// </summary>
    public string TestString { get; init; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether the pattern matched the test string.
    /// </summary>
    public bool IsMatch { get; init; }

    /// <summary>
    /// Gets the number of matches found in the test string.
    /// </summary>
    public int MatchCount { get; init; }

    /// <summary>
    /// Gets the execution time of the regex test in milliseconds.
    /// </summary>
    public double ExecutionTimeMs { get; init; }

    /// <summary>
    /// Gets the error message if an error occurred during regex evaluation; otherwise, null.
    /// </summary>
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// Gets a value indicating whether the pattern is a valid regular expression.
    /// </summary>
    public bool IsValid { get; init; }

    /// <summary>
    /// Gets the collection of match results found in the test string.
    /// </summary>
    public MatchResult[] Matches { get; init; } = Array.Empty<MatchResult>();
}