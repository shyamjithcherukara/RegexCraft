using RegexCraft.Core.Models;

namespace RegexCraft.Core.Services.Contracts;

/// <summary>
/// Defines methods for generating code snippets that demonstrate regular expression usage in various programming languages.
/// </summary>
public interface ICodeGeneratorService
{
    /// <summary>
    /// Generates code snippets for all supported languages that demonstrate how to use the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to generate code for.</param>
    /// <param name="sampleText">Optional sample text to use in the generated code snippets.</param>
    /// <returns>
    /// An array of <see cref="CodeSnippet"/> objects, each containing code for a supported language.
    /// </returns>
    CodeSnippet[] GenerateCode(string pattern, string sampleText = "");

    /// <summary>
    /// Generates a code snippet for a specific programming language that demonstrates how to use the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to generate code for.</param>
    /// <param name="language">The programming language for which to generate the code snippet.</param>
    /// <param name="sampleText">Optional sample text to use in the generated code snippet.</param>
    /// <returns>
    /// A <see cref="CodeSnippet"/> containing the generated code for the specified language.
    /// </returns>
    CodeSnippet GenerateCodeForLanguage(string pattern, string language, string sampleText = "");

    /// <summary>
    /// Gets the list of programming languages supported for code generation.
    /// </summary>
    /// <returns>
    /// An array of strings representing the names of supported programming languages.
    /// </returns>
    string[] GetSupportedLanguages();
}
