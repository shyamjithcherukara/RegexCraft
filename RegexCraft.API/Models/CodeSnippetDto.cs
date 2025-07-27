namespace RegexCraft.API.Models;

/// <summary>
/// Represents a code snippet with its language, code content, and description.
/// </summary>
public record CodeSnippetDto
{
    /// <summary>
    /// Gets the programming language of the code snippet.
    /// </summary>
    public string Language { get; init; } = string.Empty;

    /// <summary>
    /// Gets the code content of the snippet.
    /// </summary>
    public string Code { get; init; } = string.Empty;

    /// <summary>
    /// Gets the description of the code snippet.
    /// </summary>
    public string Description { get; init; } = string.Empty;
}