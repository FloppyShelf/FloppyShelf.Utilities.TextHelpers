using FloppyShelf.Utilities.TextHelpers;

namespace FloppyShelf.Utilities.TextHelpers.Tests
{
    public class StringSanitizerTests
    {
        [Fact]
        public void RemoveInvalidCharacters_RemovesInvalidPathAndFileNameChars()
        {
            string input = "test<>:|?*string";
            string result = StringSanitizer.RemoveInvalidCharacters(input);
            Assert.DoesNotContain("<>:|?*", result);
        }

        [Fact]
        public void RemoveInvalidPathCharacters_RemovesOnlyPathInvalidChars()
        {
            string input = "test<>:|?*string";
            string result = StringSanitizer.RemoveInvalidPathCharacters(input);
            Assert.All(System.IO.Path.GetInvalidPathChars(), invalidChar => Assert.False(result.Contains(invalidChar)));
        }

        [Fact]
        public void RemoveInvalidFileNameCharacters_RemovesOnlyFileNameInvalidChars()
        {
            string input = "test<>:|?*string";
            string result = StringSanitizer.RemoveInvalidFileNameCharacters(input);
            Assert.All(System.IO.Path.GetInvalidFileNameChars(), invalidChar => Assert.False(result.Contains(invalidChar)));
        }

        [Fact]
        public void ReplaceFirstOccurrenceOfStringInText_ReplacesCorrectly()
        {
            string input = "hello world, hello!";
            string result = StringSanitizer.ReplaceFirstOccurrenceOfStringInText(input, "hello", "hi");
            Assert.Equal("hi world, hello!", result);
        }

        [Fact]
        public void ReplaceLastOccurrenceOfStringInText_ReplacesCorrectly()
        {
            string input = "hello world, hello!";
            string result = StringSanitizer.ReplaceLastOccurrenceOfStringInText(input, "hello", "hi");
            Assert.Equal("hello world, hi!", result);
        }

        [Fact]
        public void FindNonMatchingCharacters_ReturnsCorrectCharacters()
        {
            string input = "abc123";
            string pattern = "[a-z]";
            char[] result = StringSanitizer.FindNonMatchingCharacters(input, pattern);
            Assert.Equal(new char[] { '1', '2', '3' }, result);
        }

        [Fact]
        public void FindNonMatchingCharactersFormatted_FormatsSpecialCharacters()
        {
            string input = "a b\nc\t";
            string pattern = "[a-z]";
            string result = StringSanitizer.FindNonMatchingCharactersFormatted(input, pattern);
            Assert.Equal("[Space], [Newline], [Tab]", result);
        }
    }
}