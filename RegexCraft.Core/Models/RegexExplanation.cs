namespace RegexCraft.Core.Models;

/// <summary>
/// Represents a detailed explanation of a regular expression pattern,
/// including its parts and a summary.
/// </summary>
public class RegexExplanation
{
    /// <summary>
    /// Gets or sets the regular expression pattern being explained.
    /// </summary>
    public string Pattern { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of explanation parts that break down the pattern.
    /// </summary>
    public List<ExplanationPart> Parts { get; set; } = new();

    /// <summary>
    /// Gets or sets a summary description of the regular expression pattern.
    /// </summary>
    public string Summary { get; set; } = string.Empty;
}

/// <summary>
/// Represents a single part of a regular expression explanation,
/// including the component, its explanation, and an example.
/// </summary>
public class ExplanationPart
{
    /// <summary>
    /// Gets or sets the regex component (e.g., a character class or quantifier).
    /// </summary>
    public string Component { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the explanation of the regex component.
    /// </summary>
    public string Explanation { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an example demonstrating the regex component.
    /// </summary>
    public string Example { get; set; } = string.Empty;
}