using System.ComponentModel.DataAnnotations;

namespace RegexCraft.API.Models;

/// <summary>
/// Represents a request to explain a regular expression pattern.
/// </summary>
public record PatternExplanationRequest
{
    /// <summary>
    /// Gets the regular expression pattern to be explained.
    /// </summary>
    [Required(ErrorMessage = "Pattern is required")]
    [StringLength(1000, ErrorMessage = "Pattern cannot exceed 1000 characters")]
    public string Pattern { get; init; } = string.Empty;
}