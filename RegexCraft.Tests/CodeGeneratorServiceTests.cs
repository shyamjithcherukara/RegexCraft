using RegexCraft.Core.Services.Contracts;
using RegexCraft.Core.Services.Implementations;
using Xunit;

namespace RegexCraft.Tests
{
    public class CodeGeneratorServiceTests
    {
        private readonly ICodeGeneratorService _service = new CodeGeneratorService();

        [Fact]
        public void GenerateCode_ReturnsSnippets()
        {
            var snippets = _service.GenerateCode("abc");
            Assert.NotNull(snippets);
            Assert.NotEmpty(snippets);
        }

        [Fact]
        public void GenerateCode_EmptyPattern_ReturnsSnippets()
        {
            var snippets = _service.GenerateCode("");
            Assert.NotNull(snippets);
        }

        [Fact]
        public void GenerateCodeForLanguage_ReturnsSnippet()
        {
            var snippet = _service.GenerateCodeForLanguage("abc", "C#");
            Assert.NotNull(snippet);
            Assert.Equal("C#", snippet.Language);
        }

        [Fact]
        public void GenerateCodeForLanguage_UnknownLanguage_ThrowsOrReturnsNull()
        {
            var ex = Record.Exception(() => _service.GenerateCodeForLanguage("abc", "NotALang"));
            Assert.True(ex == null || ex is NotSupportedException);
        }

        [Fact]
        public void GetSupportedLanguages_ReturnsLanguages()
        {
            var langs = _service.GetSupportedLanguages();
            Assert.NotNull(langs);
            Assert.NotEmpty(langs);
        }
    }
}
