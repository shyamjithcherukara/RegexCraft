using RegexCraft.Core.Models;

namespace RegexCraft.Core.Services.Contracts;

/// <summary>
/// Provides methods for managing and retrieving reusable regular expression patterns from a pattern library.
/// </summary>
public interface IPatternLibraryService
{
    /// <summary>
    /// Retrieves all patterns available in the pattern library.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an array of <see cref="PatternLibraryItem"/> objects.
    /// </returns>
    Task<PatternLibraryItem[]> GetAllPatternsAsync();

    /// <summary>
    /// Retrieves all patterns that belong to the specified category.
    /// </summary>
    /// <param name="category">The category to filter patterns by.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an array of <see cref="PatternLibraryItem"/> objects in the specified category.
    /// </returns>
    Task<PatternLibraryItem[]> GetPatternsByCategoryAsync(string category);

    /// <summary>
    /// Retrieves all unique categories available in the pattern library.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an array of category names.
    /// </returns>
    Task<string[]> GetCategoriesAsync();

    /// <summary>
    /// Retrieves a pattern by its unique name.
    /// </summary>
    /// <param name="name">The name of the pattern to retrieve.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="PatternLibraryItem"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<PatternLibraryItem?> GetPatternByNameAsync(string name);

    /// <summary>
    /// Searches for patterns that match the specified search term in their name, description, or category.
    /// </summary>
    /// <param name="searchTerm">The term to search for.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains an array of matching <see cref="PatternLibraryItem"/> objects.
    /// </returns>
    Task<PatternLibraryItem[]> SearchPatternsAsync(string searchTerm);
}
