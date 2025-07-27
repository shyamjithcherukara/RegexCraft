namespace RegexCraft.API.Models;

/// <summary>
/// Represents the explanation of a regular expression pattern, including the pattern itself,
/// a summary description, and detailed explanation parts.
/// </summary>
public record PatternExplanationResponse
{
    /// <summary>
    /// Gets the regular expression pattern being explained.
    /// </summary>
    public string Pattern { get; init; } = string.Empty;

    /// <summary>
    /// Gets a summary description of the pattern's purpose or behavior.
    /// </summary>
    public string Summary { get; init; } = string.Empty;

    /// <summary>
    /// Gets the detailed explanation parts that break down the pattern.
    /// </summary>
    public ExplanationPartDto[] Parts { get; init; } = Array.Empty<ExplanationPartDto>();
}