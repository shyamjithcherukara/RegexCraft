namespace RegexCraft.API.Models;

/// <summary>
/// Represents the response containing generated code snippets.
/// </summary>
public record CodeGenerationResponse
{
    /// <summary>
    /// Gets the collection of generated code snippets.
    /// </summary>
    public CodeSnippetDto[] Snippets { get; init; } = Array.Empty<CodeSnippetDto>();
}