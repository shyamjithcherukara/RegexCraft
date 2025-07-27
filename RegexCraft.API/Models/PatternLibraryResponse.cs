namespace RegexCraft.API.Models;

/// <summary>
/// Represents a response containing a collection of pattern library entries and their count.
/// </summary>
public record PatternLibraryResponse
{
    /// <summary>
    /// Gets the array of pattern library entries.
    /// </summary>
    public PatternLibraryDto[] Patterns { get; init; } = Array.Empty<PatternLibraryDto>();

    /// <summary>
    /// Gets the total number of pattern library entries.
    /// </summary>
    public int Count { get; init; }
}