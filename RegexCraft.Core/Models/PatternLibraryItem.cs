namespace RegexCraft.Core.Models;

/// <summary>
/// Represents a reusable regular expression pattern with metadata for categorization and documentation.
/// </summary>
public class PatternLibraryItem
{
    /// <summary>
    /// Gets or sets the display name of the pattern.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the regular expression pattern.
    /// </summary>
    public string Pattern { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a description explaining the purpose or usage of the pattern.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets example strings that demonstrate the pattern's usage.
    /// </summary>
    public string[] Examples { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets the category to which the pattern belongs.
    /// </summary>
    public string Category { get; set; } = "General";
}
