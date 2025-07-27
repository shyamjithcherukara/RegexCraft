using RegexCraft.API.Models;
using RegexCraft.Core.Models;
using System.Text.RegularExpressions;

namespace RegexCraft.API.Extensions;

/// <summary>
/// Provides extension methods for mapping between core models and API DTOs/results.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Maps a <see cref="RegexTestResult"/> to a <see cref="RegexTestResponse"/>.
    /// </summary>
    /// <param name="result">The regex test result to map.</param>
    /// <returns>A <see cref="RegexTestResponse"/> representing the result.</returns>
    public static RegexTestResponse ToResponse(this RegexTestResult result)
    {
        return new RegexTestResponse
        {
            Pattern = result.Pattern,
            TestString = result.TestString,
            IsMatch = result.IsMatch,
            MatchCount = result.MatchCount,
            ExecutionTimeMs = result.ExecutionTime.TotalMilliseconds,
            ErrorMessage = result.ErrorMessage,
            IsValid = result.IsValid,
            Matches = result.Matches.Select(m => m.ToMatchResult()).ToArray()
        };
    }

    /// <summary>
    /// Maps a <see cref="Match"/> to a <see cref="MatchResult"/>.
    /// </summary>
    /// <param name="match">The regex match to map.</param>
    /// <returns>A <see cref="MatchResult"/> representing the match.</returns>
    public static MatchResult ToMatchResult(this Match match)
    {
        return new MatchResult
        {
            Value = match.Value,
            Index = match.Index,
            Length = match.Length,
            Groups = match.Groups.Cast<Group>().Skip(1).Select(g => g.ToGroupResult()).ToArray()
        };
    }

    /// <summary>
    /// Maps a <see cref="Group"/> to a <see cref="GroupResult"/>.
    /// </summary>
    /// <param name="group">The regex group to map.</param>
    /// <returns>A <see cref="GroupResult"/> representing the group.</returns>
    public static GroupResult ToGroupResult(this Group group)
    {
        return new GroupResult
        {
            Value = group.Value,
            Index = group.Index,
            Length = group.Length,
            Success = group.Success
        };
    }

    /// <summary>
    /// Maps a <see cref="PatternLibraryItem"/> to a <see cref="PatternLibraryDto"/>.
    /// </summary>
    /// <param name="item">The pattern library item to map.</param>
    /// <returns>A <see cref="PatternLibraryDto"/> representing the item.</returns>
    public static PatternLibraryDto ToDto(this PatternLibraryItem item)
    {
        return new PatternLibraryDto
        {
            Name = item.Name,
            Pattern = item.Pattern,
            Description = item.Description,
            Category = item.Category,
            Examples = item.Examples
        };
    }

    /// <summary>
    /// Maps a <see cref="CodeSnippet"/> to a <see cref="CodeSnippetDto"/>.
    /// </summary>
    /// <param name="snippet">The code snippet to map.</param>
    /// <returns>A <see cref="CodeSnippetDto"/> representing the snippet.</returns>
    public static CodeSnippetDto ToDto(this CodeSnippet snippet)
    {
        return new CodeSnippetDto
        {
            Language = snippet.Language,
            Code = snippet.Code,
            Description = snippet.Description
        };
    }

    /// <summary>
    /// Maps a <see cref="RegexExplanation"/> to a <see cref="PatternExplanationResponse"/>.
    /// </summary>
    /// <param name="explanation">The regex explanation to map.</param>
    /// <returns>A <see cref="PatternExplanationResponse"/> representing the explanation.</returns>
    public static PatternExplanationResponse ToResponse(this RegexExplanation explanation)
    {
        return new PatternExplanationResponse
        {
            Pattern = explanation.Pattern,
            Summary = explanation.Summary,
            Parts = explanation.Parts.Select(p => new ExplanationPartDto
            {
                Component = p.Component,
                Explanation = p.Explanation,
                Example = p.Example
            }).ToArray()
        };
    }
}