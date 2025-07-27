namespace RegexCraft.API.Models;

/// <summary>
/// Represents a part of a regex explanation, including the regex component, its explanation, and an example.
/// </summary>
public record ExplanationPartDto
{
    /// <summary>
    /// Gets the regex component being explained.
    /// </summary>
    public string Component { get; init; } = string.Empty;

    /// <summary>
    /// Gets the explanation for the regex component.
    /// </summary>
    public string Explanation { get; init; } = string.Empty;

    /// <summary>
    /// Gets an example demonstrating the regex component.
    /// </summary>
    public string Example { get; init; } = string.Empty;
}