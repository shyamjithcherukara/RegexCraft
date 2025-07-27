using System.ComponentModel.DataAnnotations;

namespace RegexCraft.API.Models;

/// <summary>
/// Represents a request to perform a performance test on a regular expression pattern.
/// </summary>
public record PerformanceTestRequest
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
    /// Gets the test string that the pattern will be evaluated against.
    /// </summary>
    /// <remarks>
    /// This field is required and cannot exceed 10000 characters.
    /// </remarks>
    [Required(ErrorMessage = "Test string is required")]
    [StringLength(10000, ErrorMessage = "Test string cannot exceed 10000 characters")]
    public string TestString { get; init; } = string.Empty;

    /// <summary>
    /// Gets the number of iterations to run the performance test.
    /// </summary>
    /// <remarks>
    /// Must be between 1 and 10000. Default is 1000.
    /// </remarks>
    [Range(1, 10000, ErrorMessage = "Iterations must be between 1 and 10000")]
    public int Iterations { get; init; } = 1000;
}