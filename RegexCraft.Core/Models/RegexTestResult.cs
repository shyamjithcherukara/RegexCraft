using System.Text.RegularExpressions;

namespace RegexCraft.Core.Models;

/// <summary>
/// Represents the result of testing a regular expression against a test string.
/// </summary>
public class RegexTestResult
{
    /// <summary>
    /// Gets or sets the regular expression pattern used for testing.
    /// </summary>
    public string Pattern { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the input string that was tested against the pattern.
    /// </summary>
    public string TestString { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the pattern matched the test string.
    /// </summary>
    public bool IsMatch { get; set; }

    /// <summary>
    /// Gets or sets the array of matches found in the test string.
    /// </summary>
    public Match[] Matches { get; set; } = Array.Empty<Match>();

    /// <summary>
    /// Gets or sets the time taken to execute the regular expression match.
    /// </summary>
    public TimeSpan ExecutionTime { get; set; }

    /// <summary>
    /// Gets or sets the error message if an error occurred during matching; otherwise, <c>null</c>.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the regular expression is valid.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets the number of matches found in the test string.
    /// </summary>
    public int MatchCount => Matches?.Length ?? 0;

    /// <summary>
    /// Gets a value indicating whether an error occurred during matching.
    /// </summary>
    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
}
