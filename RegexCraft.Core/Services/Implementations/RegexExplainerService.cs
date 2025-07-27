using RegexCraft.Core.Models;
using RegexCraft.Core.Services.Contracts;
using System.Text;

namespace RegexCraft.Core.Services.Implementations;

/// <summary>
/// Provides functionality to explain and summarize regular expression patterns
/// by breaking them down into their components and generating human-readable descriptions.
/// </summary>
public class RegexExplainerService: IRegexExplainerService
{
    /// <summary>
    /// A dictionary of common regex patterns and their explanations.
    /// </summary>
    private readonly Dictionary<string, string> _commonPatterns = new()
        {
            { "^", "Start of string anchor" },
            { "$", "End of string anchor" },
            { "\\b", "Word boundary" },
            { "\\B", "Non-word boundary" },
            { ".", "Any character (except newline)" },
            { "\\d", "Any digit (0-9)" },
            { "\\D", "Any non-digit" },
            { "\\w", "Any word character (letters, digits, underscore)" },
            { "\\W", "Any non-word character" },
            { "\\s", "Any whitespace character" },
            { "\\S", "Any non-whitespace character" },
            { "*", "Zero or more of the preceding element" },
            { "+", "One or more of the preceding element" },
            { "?", "Zero or one of the preceding element" },
            { "\\", "Escape character" },
            { "|", "Alternation (OR)" },
            { "(", "Start of capturing group" },
            { ")", "End of capturing group" },
            { "[", "Start of character class" },
            { "]", "End of character class" },
            { "{", "Start of quantifier" },
            { "}", "End of quantifier" }
        };

    /// <summary>
    /// Explains a regular expression pattern by breaking it down into its components
    /// and providing detailed explanations for each part, as well as a summary.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to explain.</param>
    /// <returns>
    /// A <see cref="RegexExplanation"/> object containing the pattern, its parts, and a summary.
    /// </returns>
    public RegexExplanation ExplainPattern(string pattern)
    {
        if (pattern == null)
        {
            return new RegexExplanation
            {
                Pattern = null,
                Parts = new List<ExplanationPart>(),
                Summary = string.Empty
            };
        }

        var explanation = new RegexExplanation
        {
            Pattern = pattern,
            Parts = new List<ExplanationPart>()
        };

        var parts = AnalyzePattern(pattern);
        explanation.Parts.AddRange(parts);
        explanation.Summary = GenerateSummary(parts);

        return explanation;
    }

    /// <summary>
    /// Generates a quick, human-readable summary of a regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to summarize.</param>
    /// <returns>
    /// A string containing a summary description of the pattern.
    /// </returns>
    public string GetQuickSummary(string pattern)
    {
        if (pattern == null)
            return string.Empty;

        var parts = AnalyzePattern(pattern);
        return GenerateSummary(parts);
    }

    /// <summary>
    /// Analyzes a regular expression pattern and breaks it down into its component parts,
    /// providing explanations for each part.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to analyze.</param>
    /// <returns>
    /// A list of <see cref="ExplanationPart"/> objects representing each component of the pattern.
    /// </returns>
    private List<ExplanationPart> AnalyzePattern(string pattern)
    {
        var parts = new List<ExplanationPart>();
        var i = 0;

        while (i < pattern.Length)
        {
            var part = new ExplanationPart();
            var currentChar = pattern[i];

            if (currentChar == '\\' && i + 1 < pattern.Length)
            {
                // Escape sequences
                var escaped = pattern.Substring(i, 2);
                part.Component = escaped;
                part.Explanation = _commonPatterns.ContainsKey(escaped)
                    ? _commonPatterns[escaped]
                    : $"Escaped character: {escaped[1]}";
                i += 2;
            }
            else if (currentChar == '[')
            {
                // Character class
                var endIndex = pattern.IndexOf(']', i + 1);
                if (endIndex != -1)
                {
                    part.Component = pattern.Substring(i, endIndex - i + 1);
                    part.Explanation = ExplainCharacterClass(part.Component);
                    i = endIndex + 1;
                }
                else
                {
                    part.Component = currentChar.ToString();
                    part.Explanation = "Literal '[' character";
                    i++;
                }
            }
            else if (currentChar == '(')
            {
                // Group
                var groupEnd = FindMatchingParen(pattern, i);
                if (groupEnd != -1)
                {
                    part.Component = pattern.Substring(i, groupEnd - i + 1);
                    part.Explanation = "Capturing group";
                    i = groupEnd + 1;
                }
                else
                {
                    part.Component = currentChar.ToString();
                    part.Explanation = "Literal '(' character";
                    i++;
                }
            }
            else if (currentChar == '{')
            {
                // Quantifier
                var endIndex = pattern.IndexOf('}', i + 1);
                if (endIndex != -1)
                {
                    part.Component = pattern.Substring(i, endIndex - i + 1);
                    part.Explanation = ExplainQuantifier(part.Component);
                    i = endIndex + 1;
                }
                else
                {
                    part.Component = currentChar.ToString();
                    part.Explanation = "Literal '{' character";
                    i++;
                }
            }
            else if (_commonPatterns.ContainsKey(currentChar.ToString()))
            {
                part.Component = currentChar.ToString();
                part.Explanation = _commonPatterns[currentChar.ToString()];
                i++;
            }
            else
            {
                // Literal character
                part.Component = currentChar.ToString();
                part.Explanation = $"Literal character: '{currentChar}'";
                i++;
            }

            if (!string.IsNullOrEmpty(part.Component))
            {
                parts.Add(part);
            }
        }

        return parts;
    }

    /// <summary>
    /// Provides an explanation for a character class component in a regex pattern.
    /// </summary>
    /// <param name="charClass">The character class string (e.g., "[a-z]").</param>
    /// <returns>
    /// A string explaining what the character class matches.
    /// </returns>
    private string ExplainCharacterClass(string charClass)
    {
        if (charClass.StartsWith("[^"))
            return $"Negated character class - matches any character NOT in: {charClass.Substring(2, charClass.Length - 3)}";

        var inner = charClass.Substring(1, charClass.Length - 2);

        if (inner.Contains("a-z"))
            return "Character class - matches lowercase letters" + (inner.Length > 3 ? " and other specified characters" : "");
        if (inner.Contains("A-Z"))
            return "Character class - matches uppercase letters" + (inner.Length > 3 ? " and other specified characters" : "");
        if (inner.Contains("0-9"))
            return "Character class - matches digits" + (inner.Length > 3 ? " and other specified characters" : "");

        return $"Character class - matches any of: {inner}";
    }

    /// <summary>
    /// Provides an explanation for a quantifier component in a regex pattern.
    /// </summary>
    /// <param name="quantifier">The quantifier string (e.g., "{2,5}").</param>
    /// <returns>
    /// A string explaining what the quantifier means.
    /// </returns>
    private string ExplainQuantifier(string quantifier)
    {
        var inner = quantifier.Substring(1, quantifier.Length - 2);

        if (inner.Contains(','))
        {
            var parts = inner.Split(',');
            if (parts.Length == 2)
            {
                if (string.IsNullOrEmpty(parts[1]))
                    return $"Quantifier - {parts[0]} or more times";
                else
                    return $"Quantifier - between {parts[0]} and {parts[1]} times";
            }
        }
        else
        {
            return $"Quantifier - exactly {inner} times";
        }

        return $"Quantifier: {quantifier}";
    }

    /// <summary>
    /// Finds the index of the matching closing parenthesis for a group in a regex pattern.
    /// </summary>
    /// <param name="pattern">The regex pattern.</param>
    /// <param name="start">The index of the opening parenthesis.</param>
    /// <returns>
    /// The index of the matching closing parenthesis, or -1 if not found.
    /// </returns>
    private int FindMatchingParen(string pattern, int start)
    {
        var count = 1;
        for (int i = start + 1; i < pattern.Length; i++)
        {
            if (pattern[i] == '(') count++;
            else if (pattern[i] == ')') count--;

            if (count == 0) return i;
        }
        return -1;
    }

    /// <summary>
    /// Generates a summary description for a list of regex explanation parts.
    /// </summary>
    /// <param name="parts">The list of <see cref="ExplanationPart"/> objects.</param>
    /// <returns>
    /// A string summarizing the overall pattern.
    /// </returns>
    private string GenerateSummary(List<ExplanationPart> parts)
    {
        var summary = new StringBuilder();

        var hasStartAnchor = parts.Any(p => p.Component == "^");
        var hasEndAnchor = parts.Any(p => p.Component == "$");

        if (hasStartAnchor && hasEndAnchor)
            summary.Append("Matches the entire string that ");
        else if (hasStartAnchor)
            summary.Append("Matches strings that start with ");
        else if (hasEndAnchor)
            summary.Append("Matches strings that end with ");
        else
            summary.Append("Matches strings containing ");

        var nonAnchorParts = parts.Where(p => p.Component != "^" && p.Component != "$").ToList();

        if (nonAnchorParts.Count == 1)
        {
            summary.Append(GetSimpleDescription(nonAnchorParts[0]));
        }
        else if (nonAnchorParts.Count > 1)
        {
            summary.Append("a pattern consisting of multiple components");
        }

        return summary.ToString();
    }

    /// <summary>
    /// Provides a simple, human-readable description for a single regex explanation part.
    /// </summary>
    /// <param name="part">The <see cref="ExplanationPart"/> to describe.</param>
    /// <returns>
    /// A string describing the part in simple terms.
    /// </returns>
    private string GetSimpleDescription(ExplanationPart part)
    {
        if (part.Component.StartsWith("[") && part.Component.EndsWith("]"))
            return "characters from a specific set";
        if (part.Component.Contains("+"))
            return "one or more characters";
        if (part.Component.Contains("*"))
            return "zero or more characters";
        if (part.Component.Contains("?"))
            return "optional characters";

        return "specific characters or patterns";
    }
}
