namespace RegexCraft.API.Models;

/// <summary>
/// Represents the result of a single regex group match.
/// </summary>
public record GroupResult
{
    /// <summary>
    /// Gets the value captured by the group.
    /// </summary>
    public string Value { get; init; } = string.Empty;

    /// <summary>
    /// Gets the zero-based starting position of the group in the input string.
    /// </summary>
    public int Index { get; init; }

    /// <summary>
    /// Gets the length of the captured value.
    /// </summary>
    public int Length { get; init; }

    /// <summary>
    /// Gets a value indicating whether the group successfully captured a value.
    /// </summary>
    public bool Success { get; init; }
}