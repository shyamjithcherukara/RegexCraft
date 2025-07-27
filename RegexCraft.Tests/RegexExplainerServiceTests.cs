using RegexCraft.Core.Services.Contracts;
using RegexCraft.Core.Services.Implementations;
using Xunit;

namespace RegexCraft.Tests
{
    public class RegexExplainerServiceTests
    {
        private readonly IRegexExplainerService _service = new RegexExplainerService();

        [Fact]
        public void ExplainPattern_ReturnsExplanation()
        {
            var explanation = _service.ExplainPattern("abc");
            Assert.NotNull(explanation);
            Assert.NotNull(explanation.Parts);
        }

        [Fact]
        public void ExplainPattern_EmptyPattern_ReturnsExplanation()
        {
            var explanation = _service.ExplainPattern("");
            Assert.NotNull(explanation);
            Assert.NotNull(explanation.Parts);
        }

        [Fact]
        public void ExplainPattern_NullPattern_ReturnsExplanation()
        {
            var explanation = _service.ExplainPattern(null);
            Assert.NotNull(explanation);
            Assert.NotNull(explanation.Parts);
        }

        [Fact]
        public void GetQuickSummary_ReturnsSummary()
        {
            var summary = _service.GetQuickSummary("abc");
            Assert.False(string.IsNullOrWhiteSpace(summary));
        }

        [Fact]
        public void GetQuickSummary_EmptyPattern_ReturnsSummary()
        {
            var summary = _service.GetQuickSummary("");
            Assert.NotNull(summary);
        }

        [Fact]
        public void GetQuickSummary_NullPattern_ReturnsSummary()
        {
            var summary = _service.GetQuickSummary(null);
            Assert.NotNull(summary);
        }
    }
}
