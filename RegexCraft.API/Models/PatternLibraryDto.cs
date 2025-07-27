namespace RegexCraft.API.Models;

/// <summary>
/// Represents a data transfer object for a regex pattern library entry.
/// </summary>
public record PatternLibraryDto
{
    /// <summary>
    /// Gets the name of the pattern.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the regular expression pattern.
    /// </summary>
    public string Pattern { get; init; } = string.Empty;

    /// <summary>
    /// Gets the description of the pattern.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets the category of the pattern.
    /// </summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>
    /// Gets example usages of the pattern.
    /// </summary>
    public string[] Examples { get; init; } = Array.Empty<string>();
}