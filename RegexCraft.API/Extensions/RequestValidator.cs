using FluentValidation;
using RegexCraft.API.Models;

namespace RegexCraft.API.Extensions;

/// <summary>
/// Validator for <see cref="RegexTestRequest"/>. Ensures the pattern and test string are present and within allowed length.
/// </summary>
public class RegexTestRequestValidator : AbstractValidator<RegexTestRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegexTestRequestValidator"/> class.
    /// </summary>
    public RegexTestRequestValidator()
    {
        RuleFor(x => x.Pattern)
            .NotEmpty().WithMessage("Pattern is required")
            .MaximumLength(1000).WithMessage("Pattern cannot exceed 1000 characters");

        RuleFor(x => x.TestString)
            .NotEmpty().WithMessage("Test string is required")
            .MaximumLength(10000).WithMessage("Test string cannot exceed 10000 characters");
    }
}

/// <summary>
/// Validator for <see cref="CodeGenerationRequest"/>. Validates pattern, sample text, and language.
/// </summary>
public class CodeGenerationRequestValidator : AbstractValidator<CodeGenerationRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CodeGenerationRequestValidator"/> class.
    /// </summary>
    public CodeGenerationRequestValidator()
    {
        RuleFor(x => x.Pattern)
            .NotEmpty().WithMessage("Pattern is required")
            .MaximumLength(1000).WithMessage("Pattern cannot exceed 1000 characters");

        RuleFor(x => x.SampleText)
            .MaximumLength(1000).WithMessage("Sample text cannot exceed 1000 characters");

        RuleFor(x => x.Language)
            .Must(BeValidLanguage).WithMessage("Invalid language specified")
            .When(x => !string.IsNullOrEmpty(x.Language));
    }

    /// <summary>
    /// Checks if the provided language is valid for code generation.
    /// </summary>
    /// <param name="language">The language to validate.</param>
    /// <returns><c>true</c> if the language is valid or null/empty; otherwise, <c>false</c>.</returns>
    private bool BeValidLanguage(string? language)
    {
        if (string.IsNullOrEmpty(language)) return true;
        var validLanguages = new[] { "csharp", "javascript", "python", "java", "php" };
        return validLanguages.Contains(language.ToLower());
    }
}

/// <summary>
/// Validator for <see cref="PerformanceTestRequest"/>. Validates pattern, test string, and iteration count.
/// </summary>
public class PerformanceTestRequestValidator : AbstractValidator<PerformanceTestRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PerformanceTestRequestValidator"/> class.
    /// </summary>
    public PerformanceTestRequestValidator()
    {
        RuleFor(x => x.Pattern)
            .NotEmpty().WithMessage("Pattern is required")
            .MaximumLength(1000).WithMessage("Pattern cannot exceed 1000 characters");

        RuleFor(x => x.TestString)
            .NotEmpty().WithMessage("Test string is required")
            .MaximumLength(10000).WithMessage("Test string cannot exceed 10000 characters");

        RuleFor(x => x.Iterations)
            .InclusiveBetween(1, 10000).WithMessage("Iterations must be between 1 and 10000");
    }
}

/// <summary>
/// Validator for <see cref="PatternExplanationRequest"/>. Ensures the pattern is present and within allowed length.
/// </summary>
public class PatternExplanationRequestValidator : AbstractValidator<PatternExplanationRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PatternExplanationRequestValidator"/> class.
    /// </summary>
    public PatternExplanationRequestValidator()
    {
        RuleFor(x => x.Pattern)
            .NotEmpty().WithMessage("Pattern is required")
            .MaximumLength(1000).WithMessage("Pattern cannot exceed 1000 characters");
    }
}