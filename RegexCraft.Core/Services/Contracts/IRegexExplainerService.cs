using RegexCraft.Core.Models;

namespace RegexCraft.Core.Services.Contracts;

/// <summary>
/// Provides methods for explaining and summarizing regular expression patterns.
/// </summary>
public interface IRegexExplainerService
{
    /// <summary>
    /// Generates a detailed explanation of the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to explain.</param>
    /// <returns>
    /// A <see cref="RegexExplanation"/> object containing a breakdown and summary of the pattern.
    /// </returns>
    RegexExplanation ExplainPattern(string pattern);

    /// <summary>
    /// Returns a concise summary describing the purpose of the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to summarize.</param>
    /// <returns>
    /// A short summary string describing the pattern.
    /// </returns>
    string GetQuickSummary(string pattern);
}
