using System.ComponentModel.DataAnnotations;

namespace RegexCraft.API.Models;

/// <summary>
/// Represents a request for code generation based on a regular expression pattern.
/// </summary>
public record CodeGenerationRequest
{
    /// <summary>
    /// Gets the regular expression pattern to use for code generation.
    /// </summary>
    /// <remarks>
    /// This property is required and cannot exceed 1000 characters.
    /// </remarks>
    [Required(ErrorMessage = "Pattern is required")]
    [StringLength(1000, ErrorMessage = "Pattern cannot exceed 1000 characters")]
    public string Pattern { get; init; } = string.Empty;

    /// <summary>
    /// Gets the sample text to be used with the regular expression.
    /// </summary>
    /// <remarks>
    /// This property is optional and cannot exceed 1000 characters.
    /// </remarks>
    [StringLength(1000, ErrorMessage = "Sample text cannot exceed 1000 characters")]
    public string SampleText { get; init; } = string.Empty;

    /// <summary>
    /// Gets the programming language for which code should be generated.
    /// </summary>
    public string? Language { get; init; }
}