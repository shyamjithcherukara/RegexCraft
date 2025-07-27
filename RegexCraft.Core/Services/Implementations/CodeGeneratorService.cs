using RegexCraft.Core.Models;
using RegexCraft.Core.Services.Contracts;
using System.Text;

namespace RegexCraft.Core.Services.Implementations;

/// <summary>
/// Provides code generation services for demonstrating regular expression usage in various programming languages.
/// </summary>
public class CodeGeneratorService : ICodeGeneratorService
{
    /// <summary>
    /// The list of supported programming languages for code generation.
    /// </summary>
    private readonly string[] _supportedLanguages = { "csharp", "javascript", "python", "java", "php" };

    /// <summary>
    /// Generates code snippets for all supported languages that demonstrate how to use the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to generate code for.</param>
    /// <param name="sampleText">Optional sample text to use in the generated code snippets.</param>
    /// <returns>
    /// An array of <see cref="CodeSnippet"/> objects, each containing code for a supported language.
    /// </returns>
    public CodeSnippet[] GenerateCode(string pattern, string sampleText = "")
    {
        return _supportedLanguages.Select(lang => GenerateCodeForLanguage(pattern, lang, sampleText)).ToArray();
    }

    /// <summary>
    /// Generates a code snippet for a specific programming language that demonstrates how to use the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to generate code for.</param>
    /// <param name="language">The programming language for which to generate the code snippet.</param>
    /// <param name="sampleText">Optional sample text to use in the generated code snippet.</param>
    /// <returns>
    /// A <see cref="CodeSnippet"/> containing the generated code for the specified language.
    /// </returns>
    public CodeSnippet GenerateCodeForLanguage(string pattern, string language, string sampleText = "")
    {
        var sample = string.IsNullOrEmpty(sampleText) ? "your_test_string" : $"\"{sampleText}\"";

        return language.ToLower() switch
        {
            "csharp" => GenerateCSharpCode(pattern, sample),
            "javascript" => GenerateJavaScriptCode(pattern, sample),
            "python" => GeneratePythonCode(pattern, sample),
            "java" => GenerateJavaCode(pattern, sample),
            "php" => GeneratePHPCode(pattern, sample),
            _ => new CodeSnippet { Language = language, Code = "Language not supported", Description = "" }
        };
    }

    /// <summary>
    /// Gets the list of programming languages supported for code generation.
    /// </summary>
    /// <returns>
    /// An array of strings representing the names of supported programming languages.
    /// </returns>
    public string[] GetSupportedLanguages()
    {
        return _supportedLanguages;
    }

    /// <summary>
    /// Generates a C# code snippet demonstrating usage of the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern.</param>
    /// <param name="sample">The sample text to use in the code snippet.</param>
    /// <returns>A <see cref="CodeSnippet"/> containing the C# code.</returns>
    private CodeSnippet GenerateCSharpCode(string pattern, string sample)
    {
        var code = new StringBuilder();
        code.AppendLine("using System.Text.RegularExpressions;");
        code.AppendLine();
        code.AppendLine($"var pattern = @\"{EscapePattern(pattern, "csharp")}\";");
        code.AppendLine("var regex = new Regex(pattern);");
        code.AppendLine($"var testString = {sample};");
        code.AppendLine();
        code.AppendLine("// Test if string matches");
        code.AppendLine("bool isMatch = regex.IsMatch(testString);");
        code.AppendLine();
        code.AppendLine("// Get all matches");
        code.AppendLine("MatchCollection matches = regex.Matches(testString);");
        code.AppendLine("foreach (Match match in matches)");
        code.AppendLine("{");
        code.AppendLine("    Console.WriteLine($\"Match: {match.Value} at position {match.Index}\");");
        code.AppendLine("}");

        return new CodeSnippet
        {
            Language = "C#",
            Code = code.ToString(),
            Description = "C# implementation using System.Text.RegularExpressions"
        };
    }

    /// <summary>
    /// Generates a JavaScript code snippet demonstrating usage of the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern.</param>
    /// <param name="sample">The sample text to use in the code snippet.</param>
    /// <returns>A <see cref="CodeSnippet"/> containing the JavaScript code.</returns>
    private CodeSnippet GenerateJavaScriptCode(string pattern, string sample)
    {
        var code = new StringBuilder();
        code.AppendLine($"const pattern = /{EscapePattern(pattern, "javascript")}/g;");
        code.AppendLine($"const testString = {sample};");
        code.AppendLine();
        code.AppendLine("// Test if string matches");
        code.AppendLine("const isMatch = pattern.test(testString);");
        code.AppendLine("console.log('Is match:', isMatch);");
        code.AppendLine();
        code.AppendLine("// Get all matches");
        code.AppendLine("const matches = testString.match(pattern);");
        code.AppendLine("if (matches) {");
        code.AppendLine("    matches.forEach((match, index) => {");
        code.AppendLine("        console.log(`Match ${index + 1}: ${match}`);");
        code.AppendLine("    });");
        code.AppendLine("} else {");
        code.AppendLine("    console.log('No matches found');");
        code.AppendLine("}");

        return new CodeSnippet
        {
            Language = "JavaScript",
            Code = code.ToString(),
            Description = "JavaScript implementation using built-in RegExp"
        };
    }

    /// <summary>
    /// Generates a Python code snippet demonstrating usage of the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern.</param>
    /// <param name="sample">The sample text to use in the code snippet.</param>
    /// <returns>A <see cref="CodeSnippet"/> containing the Python code.</returns>
    private CodeSnippet GeneratePythonCode(string pattern, string sample)
    {
        var code = new StringBuilder();
        code.AppendLine("import re");
        code.AppendLine();
        code.AppendLine($"pattern = r'{EscapePattern(pattern, "python")}'");
        code.AppendLine($"test_string = {sample}");
        code.AppendLine();
        code.AppendLine("# Test if string matches");
        code.AppendLine("is_match = re.search(pattern, test_string) is not None");
        code.AppendLine("print(f'Is match: {is_match}')");
        code.AppendLine();
        code.AppendLine("# Get all matches");
        code.AppendLine("matches = re.findall(pattern, test_string)");
        code.AppendLine("for i, match in enumerate(matches, 1):");
        code.AppendLine("    print(f'Match {i}: {match}')");
        code.AppendLine();
        code.AppendLine("# Get match objects with positions");
        code.AppendLine("for match in re.finditer(pattern, test_string):");
        code.AppendLine("    print(f'Match: {match.group()} at position {match.start()}-{match.end()}')");

        return new CodeSnippet
        {
            Language = "Python",
            Code = code.ToString(),
            Description = "Python implementation using the re module"
        };
    }

    /// <summary>
    /// Generates a Java code snippet demonstrating usage of the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern.</param>
    /// <param name="sample">The sample text to use in the code snippet.</param>
    /// <returns>A <see cref="CodeSnippet"/> containing the Java code.</returns>
    private CodeSnippet GenerateJavaCode(string pattern, string sample)
    {
        var code = new StringBuilder();
        code.AppendLine("import java.util.regex.Pattern;");
        code.AppendLine("import java.util.regex.Matcher;");
        code.AppendLine();
        code.AppendLine("public class RegexExample {");
        code.AppendLine("    public static void main(String[] args) {");
        code.AppendLine($"        String pattern = \"{EscapePattern(pattern, "java")}\";");
        code.AppendLine($"        String testString = {sample};");
        code.AppendLine("        ");
        code.AppendLine("        // Compile pattern");
        code.AppendLine("        Pattern compiledPattern = Pattern.compile(pattern);");
        code.AppendLine("        Matcher matcher = compiledPattern.matcher(testString);");
        code.AppendLine("        ");
        code.AppendLine("        // Test if string matches");
        code.AppendLine("        boolean isMatch = matcher.find();");
        code.AppendLine("        System.out.println(\"Is match: \" + isMatch);");
        code.AppendLine("        ");
        code.AppendLine("        // Reset matcher and find all matches");
        code.AppendLine("        matcher.reset();");
        code.AppendLine("        int matchCount = 1;");
        code.AppendLine("        while (matcher.find()) {");
        code.AppendLine("            System.out.println(\"Match \" + matchCount + \": \" + matcher.group() + \" at position \" + matcher.start());");
        code.AppendLine("            matchCount++;");
        code.AppendLine("        }");
        code.AppendLine("    }");
        code.AppendLine("}");

        return new CodeSnippet
        {
            Language = "Java",
            Code = code.ToString(),
            Description = "Java implementation using java.util.regex package"
        };
    }

    /// <summary>
    /// Generates a PHP code snippet demonstrating usage of the specified regular expression pattern.
    /// </summary>
    /// <param name="pattern">The regular expression pattern.</param>
    /// <param name="sample">The sample text to use in the code snippet.</param>
    /// <returns>A <see cref="CodeSnippet"/> containing the PHP code.</returns>
    private CodeSnippet GeneratePHPCode(string pattern, string sample)
    {
        var code = new StringBuilder();
        code.AppendLine("<?php");
        code.AppendLine($"$pattern = '/{EscapePattern(pattern, "php")}/';");
        code.AppendLine($"$testString = {sample};");
        code.AppendLine();
        code.AppendLine("// Test if string matches");
        code.AppendLine("$isMatch = preg_match($pattern, $testString);");
        code.AppendLine("echo \"Is match: \" . ($isMatch ? 'true' : 'false') . \"\\n\";");
        code.AppendLine();
        code.AppendLine("// Get all matches");
        code.AppendLine("$matches = [];");
        code.AppendLine("$matchCount = preg_match_all($pattern, $testString, $matches, PREG_OFFSET_CAPTURE);");
        code.AppendLine();
        code.AppendLine("if ($matchCount > 0) {");
        code.AppendLine("    foreach ($matches[0] as $index => $match) {");
        code.AppendLine("        echo \"Match \" . ($index + 1) . \": \" . $match[0] . \" at position \" . $match[1] . \"\\n\";");
        code.AppendLine("    }");
        code.AppendLine("} else {");
        code.AppendLine("    echo \"No matches found\\n\";");
        code.AppendLine("}");
        code.AppendLine("?>");

        return new CodeSnippet
        {
            Language = "PHP",
            Code = code.ToString(),
            Description = "PHP implementation using PCRE functions"
        };
    }

    /// <summary>
    /// Escapes the regular expression pattern for the specified programming language.
    /// </summary>
    /// <param name="pattern">The regular expression pattern to escape.</param>
    /// <param name="language">The target programming language.</param>
    /// <returns>The escaped pattern string.</returns>
    private string EscapePattern(string pattern, string language)
    {
        return language.ToLower() switch
        {
            "csharp" => pattern.Replace("\\", "\\\\").Replace("\"", "\\\""),
            "javascript" => pattern.Replace("/", "\\/"),
            "python" => pattern,
            "java" => pattern.Replace("\\", "\\\\").Replace("\"", "\\\""),
            "php" => pattern.Replace("/", "\\/"),
            _ => pattern
        };
    }
}
