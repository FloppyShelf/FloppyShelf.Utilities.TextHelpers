using FloppyShelf.Utilities.TextHelpers;

namespace FloppyShelf.Utilities.TextHelpers.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="StringSanitizer"/> string extension methods.
    /// These tests ensure functionality and cross-platform consistency on both Windows and Linux.
    /// </summary>
    public class StringSanitizerTests
    {
        // <summary>
        /// Tests that all invalid path and file name characters are removed.
        /// Filters out non-printable control characters for cross-platform compatibility.
        /// </summary>
        [Fact]
        public void RemoveInvalidCharacters_RemovesInvalidPathAndFileNameChars()
        {
            string input = "test<>:|?*string";
            string result = input.RemoveInvalidCharacters();

            // Combine and filter invalid characters (printable only)
            var invalidChars = Path.GetInvalidPathChars()
                .Concat(Path.GetInvalidFileNameChars())
                .Where(c => !char.IsControl(c))
                .Distinct();

            foreach (char invalidChar in invalidChars)
            {
                Assert.DoesNotContain(invalidChar.ToString(), result);
            }
        }

        /// <summary>
        /// Ensures only invalid path characters are removed from the string.
        /// Excludes control characters to avoid false positives on Linux.
        /// </summary>
        [Fact]
        public void RemoveInvalidPathCharacters_RemovesOnlyPathInvalidChars()
        {
            string input = "test<>:|?*string";
            string result = input.RemoveInvalidPathCharacters();

            var invalidChars = Path.GetInvalidPathChars()
                .Where(c => !char.IsControl(c))
                .Distinct();

            foreach (char invalidChar in invalidChars)
            {
                Assert.DoesNotContain(invalidChar.ToString(), result);
            }
        }

        /// <summary>
        /// Ensures that only file name-invalid characters are removed.
        /// Filters out control characters for platform consistency.
        /// </summary>
        [Fact]
        public void RemoveInvalidFileNameCharacters_RemovesOnlyFileNameInvalidChars()
        {
            string input = "test<>:|?*string";
            string result = input.RemoveInvalidFileNameCharacters();

            var invalidChars = Path.GetInvalidFileNameChars()
                .Where(c => !char.IsControl(c))
                .Distinct();

            foreach (char invalidChar in invalidChars)
            {
                Assert.DoesNotContain(invalidChar.ToString(), result);
            }
        }

        /// <summary>
        /// Ensures the first matching occurrence of a substring is replaced correctly.
        /// </summary>
        [Fact]
        public void ReplaceFirstOccurrenceOfStringInText_ReplacesCorrectly()
        {
            string input = "hello world, hello!";
            string result = input.ReplaceFirstOccurrenceOfStringInText("hello", "hi");

            // Only the first 'hello' should be replaced
            Assert.Equal("hi world, hello!", result);
        }

        /// <summary>
        /// Ensures the last matching occurrence of a substring is replaced correctly.
        /// </summary>
        [Fact]
        public void ReplaceLastOccurrenceOfStringInText_ReplacesCorrectly()
        {
            string input = "hello world, hello!";
            string result = input.ReplaceLastOccurrenceOfStringInText("hello", "hi");

            // Only the last 'hello' should be replaced
            Assert.Equal("hello world, hi!", result);
        }

        /// <summary>
        /// Verifies that only characters not matching the provided pattern are returned.
        /// </summary>
        [Fact]
        public void FindNonMatchingCharacters_ReturnsCorrectCharacters()
        {
            string input = "abc123";
            string pattern = "[a-z]";
            char[] result = input.FindNonMatchingCharacters(pattern);

            // Expect only digits to be non-matching
            Assert.Equal(new char[] { '1', '2', '3' }, result);
        }

        /// <summary>
        /// Verifies formatting of special characters like space, newline, and tab.
        /// </summary>
        [Fact]
        public void FindNonMatchingCharactersFormatted_FormatsSpecialCharacters()
        {
            string input = "a b\nc\t";
            string pattern = "[a-z]";
            string result = input.FindNonMatchingCharactersFormatted(pattern);

            // Space, newline, tab should be detected and formatted
            Assert.Equal("[Space], [Newline], [Tab]", result);
        }

        /// <summary>
        /// Ensures null and empty strings return valid (non-crashing) results for RemoveInvalidCharacters.
        /// </summary>
        [Fact]
        public void RemoveInvalidCharacters_DoesNotThrowOnEmptyOrNull()
        {
            string nullString = null;
            string emptyString = string.Empty;

            // Null remains null, empty remains empty
            Assert.Null(nullString.RemoveInvalidCharacters());
            Assert.Equal(string.Empty, emptyString.RemoveInvalidCharacters());
        }

        /// <summary>
        /// Ensures that if the search text is not found, the original string is returned unchanged.
        /// </summary>
        [Fact]
        public void ReplaceOccurrence_ReturnsOriginalIfNotFound()
        {
            string input = "nothing to replace here";
            string result = input.ReplaceFirstOccurrenceOfStringInText("missing", "found");

            Assert.Equal(input, result);
        }

        /// <summary>
        /// Ensures FindNonMatchingCharacters returns an empty array when all characters match the pattern.
        /// </summary>
        [Fact]
        public void FindNonMatchingCharacters_ReturnsEmptyOnFullMatch()
        {
            string input = "abc";
            string pattern = "[a-c]";
            char[] result = input.FindNonMatchingCharacters(pattern);

            Assert.Empty(result);
        }
    }
}