namespace RegexCraft.API.Models;

/// <summary>
/// Represents the result of a single regex match.
/// </summary>
public record MatchResult
{
    /// <summary>
    /// Gets the value matched by the regular expression.
    /// </summary>
    public string Value { get; init; } = string.Empty;

    /// <summary>
    /// Gets the zero-based starting position of the match in the input string.
    /// </summary>
    public int Index { get; init; }

    /// <summary>
    /// Gets the length of the matched value.
    /// </summary>
    public int Length { get; init; }

    /// <summary>
    /// Gets the collection of group results for this match.
    /// </summary>
    public GroupResult[] Groups { get; init; } = Array.Empty<GroupResult>();
}