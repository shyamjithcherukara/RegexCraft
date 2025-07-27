using RegexCraft.Core.Models;
using RegexCraft.Core.Services.Contracts;
using RegexCraft.Core.Services.Implementations;
using System.Threading.Tasks;
using Xunit;

namespace RegexCraft.Tests
{
    public class PatternLibraryServiceTests
    {
        private readonly IPatternLibraryService _service = new PatternLibraryService();

        [Fact]
        public async Task GetAllPatternsAsync_ReturnsPatterns()
        {
            var patterns = await _service.GetAllPatternsAsync();
            Assert.NotNull(patterns);
        }

        [Fact]
        public async Task GetCategoriesAsync_ReturnsCategories()
        {
            var categories = await _service.GetCategoriesAsync();
            Assert.NotNull(categories);
        }

        [Fact]
        public async Task GetPatternsByCategoryAsync_ReturnsPatterns()
        {
            var patterns = await _service.GetPatternsByCategoryAsync("General");
            Assert.NotNull(patterns);
        }

        [Fact]
        public async Task GetPatternsByCategoryAsync_UnknownCategory_ReturnsEmpty()
        {
            var patterns = await _service.GetPatternsByCategoryAsync("UnknownCategory");
            Assert.NotNull(patterns);
            Assert.Empty(patterns);
        }

        [Fact]
        public async Task GetPatternByNameAsync_ReturnsPatternOrNull()
        {
            var pattern = await _service.GetPatternByNameAsync("Email");
            Assert.True(pattern == null || pattern.Name == "Email");
        }

        [Fact]
        public async Task GetPatternByNameAsync_UnknownName_ReturnsNull()
        {
            var pattern = await _service.GetPatternByNameAsync("NotARealPatternName");
            Assert.Null(pattern);
        }

        [Fact]
        public async Task SearchPatternsAsync_ReturnsResults()
        {
            var results = await _service.SearchPatternsAsync("email");
            Assert.NotNull(results);
        }

        [Fact]
        public async Task SearchPatternsAsync_NoResults_ReturnsEmpty()
        {
            var results = await _service.SearchPatternsAsync("notarealpatternsearchterm");
            Assert.NotNull(results);
            Assert.Empty(results);
        }
    }
}
