using RegexCraft.Core.Services.Contracts;
using RegexCraft.Core.Services.Implementations;
using System;
using Xunit;

namespace RegexCraft.Tests
{
    public class RegexTestingServiceTests
    {
        private readonly IRegexTestingService _service = new RegexTestingService();

        [Fact]
        public void TestPattern_ValidMatch_ReturnsMatch()
        {
            var result = _service.TestPattern("abc", "abcdef");
            Assert.True(result.IsMatch);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void TestPattern_NoMatch_ReturnsNoMatch()
        {
            var result = _service.TestPattern("xyz", "abcdef");
            Assert.False(result.IsMatch);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void TestPattern_EmptyPattern_ReturnsError()
        {
            var result = _service.TestPattern("", "abcdef");
            Assert.False(result.IsValid);
            Assert.NotNull(result.ErrorMessage);
        }

        [Fact]
        public void TestPattern_EmptyTestString_ReturnsNoMatch()
        {
            var result = _service.TestPattern("abc", "");
            Assert.False(result.IsMatch);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void TestPattern_InvalidPattern_ReturnsError()
        {
            var result = _service.TestPattern("[", "test");
            Assert.False(result.IsValid);
            Assert.NotNull(result.ErrorMessage);
        }

        [Fact]
        public void TestPatternBatch_MultipleInputs_ReturnsResults()
        {
            var results = _service.TestPatternBatch("abc", new[] { "abc", "def" });
            Assert.Equal(2, results.Length);
        }

        [Fact]
        public void TestPatternBatch_EmptyArray_ReturnsEmpty()
        {
            var results = _service.TestPatternBatch("abc", Array.Empty<string>());
            Assert.Empty(results);
        }

        [Fact]
        public void IsValidPattern_ValidAndInvalidPatterns()
        {
            Assert.True(_service.IsValidPattern("abc"));
            Assert.False(_service.IsValidPattern("["));
        }

        [Fact]
        public void MeasurePerformance_ReturnsTimeSpan()
        {
            var time = _service.MeasurePerformance("abc", "abcdef", 10);
            Assert.True(time > TimeSpan.Zero);
        }

        [Fact]
        public void MeasurePerformance_ZeroIterations_ReturnsZero()
        {
            var time = _service.MeasurePerformance("abc", "abcdef", 0);
            Assert.Equal(TimeSpan.Zero, time);
        }

        // Additional test cases
        [Fact]
        public void TestPattern_SpecialCharacters_ReturnsMatch()
        {
            var result = _service.TestPattern("a.c", "abcdef");
            Assert.True(result.IsMatch);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void TestPatternBatch_NullInputs_ReturnsError()
        {
            var results = _service.TestPatternBatch("abc", null);
            Assert.Null(results);
        }

        [Fact]
        public void MeasurePerformance_InvalidPattern_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _service.MeasurePerformance("[", "abcdef", 10));
        }
    }
}
