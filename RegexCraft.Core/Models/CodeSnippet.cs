namespace RegexCraft.Core.Models;

/// <summary>
/// Represents a code snippet with its language, code content, and description.
/// </summary>
public class CodeSnippet
{
    /// <summary>
    /// Gets or sets the programming language of the code snippet.
    /// </summary>
    public string Language { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the code content of the snippet.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the code snippet.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}
