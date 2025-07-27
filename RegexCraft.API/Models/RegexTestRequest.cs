using System.ComponentModel.DataAnnotations;

namespace RegexCraft.API.Models;

/// <summary>
/// Represents a request to test a regular expression pattern against a test string.
/// </summary>
public record RegexTestRequest
{
    /// <summary>
    /// Gets the regular expression pattern to be tested.
    /// </summary>
    /// <remarks>
    /// This field is required and cannot exceed 1000 characters.
    /// </remarks>
    [Required(ErrorMessage = "Pattern is required")]
    [StringLength(1000, ErrorMessage = "Pattern cannot exceed 1000 characters")]
    public string Pattern { get; init; } = string.Empty;

    /// <summary>
    /// Gets the string to test the regular expression pattern against.
    /// </summary>
    /// <remarks>
    /// This field is required and cannot exceed 10000 characters.
    /// </remarks>
    [Required(ErrorMessage = "Test string is required")]
    [StringLength(10000, ErrorMessage = "Test string cannot exceed 10000 characters")]
    public string TestString { get; init; } = string.Empty;
}