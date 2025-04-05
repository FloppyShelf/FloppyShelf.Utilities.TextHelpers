using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FloppyShelf.Utilities.TextHelpers
{
    /// <summary>
    /// Provides string extension methods for sanitizing and processing text,
    /// including removal of invalid characters, string replacements, and pattern matching.
    /// </summary>
    public static class StringSanitizer
    {
        /// <summary>
        /// Removes all characters from the input string that are invalid in both file paths and file names.
        /// </summary>
        /// <param name="value">The input string to sanitize.</param>
        /// <returns>A new string with invalid path and file name characters removed.</returns>
        public static string RemoveInvalidCharacters(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            // Remove path-specific invalid characters first
            value = RemoveInvalidPathCharacters(value);

            // Then remove file name-specific invalid characters
            value = RemoveInvalidFileNameCharacters(value);

            return value;
        }

        /// <summary>
        /// Removes characters from the input string that are invalid in file paths.
        /// </summary>
        /// <param name="value">The string to sanitize.</param>
        /// <returns>A new string with invalid path characters removed.</returns>
        public static string RemoveInvalidPathCharacters(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            // Get array of characters that are invalid in file paths
            char[] invalidChars = Path.GetInvalidPathChars();

            // Filter out invalid characters from the input
            return new string(value.Where(c => !invalidChars.Contains(c)).ToArray());
        }

        /// <summary>
        /// Removes characters from the input string that are invalid in file names.
        /// </summary>
        /// <param name="value">The string to sanitize.</param>
        /// <returns>A new string with invalid file name characters removed.</returns>
        public static string RemoveInvalidFileNameCharacters(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            // Get array of characters that are invalid in file names
            char[] invalidChars = Path.GetInvalidFileNameChars();

            // Filter out invalid characters from the input
            return new string(value.Where(c => !invalidChars.Contains(c)).ToArray());
        }

        /// <summary>
        /// Replaces the first occurrence of a specified substring within the original string.
        /// </summary>
        /// <param name="originalText">The full original string.</param>
        /// <param name="searchText">The substring to find and replace.</param>
        /// <param name="replaceText">The replacement string.</param>
        /// <returns>The modified string with the first occurrence replaced, if found.</returns>
        public static string ReplaceFirstOccurrenceOfStringInText(this string originalText, string searchText, string replaceText)
        {
            return ReplaceOccurrence(originalText, searchText, replaceText, true);
        }

        /// <summary>
        /// Replaces the last occurrence of a specified substring within the original string.
        /// </summary>
        /// <param name="originalText">The full original string.</param>
        /// <param name="searchText">The substring to find and replace.</param>
        /// <param name="replaceText">The replacement string.</param>
        /// <returns>The modified string with the last occurrence replaced, if found.</returns>
        public static string ReplaceLastOccurrenceOfStringInText(this string originalText, string searchText, string replaceText)
        {
            return ReplaceOccurrence(originalText, searchText, replaceText, false);
        }

        /// <summary>
        /// Private shared logic to replace either the first or last occurrence of a substring.
        /// </summary>
        /// <param name="originalText">The original text.</param>
        /// <param name="searchText">The text to search for.</param>
        /// <param name="replaceText">The text to replace with.</param>
        /// <param name="firstOccurrence">If true, replace the first occurrence; otherwise, the last.</param>
        /// <returns>Modified string with replacement made.</returns>
        private static string ReplaceOccurrence(string originalText, string searchText, string replaceText, bool firstOccurrence)
        {
            // Ensure valid input
            if (string.IsNullOrEmpty(originalText) || string.IsNullOrEmpty(searchText)) return originalText;

            // Locate index of the first or last match
            int pos = firstOccurrence 
                ? originalText.IndexOf(searchText, StringComparison.Ordinal) 
                : originalText.LastIndexOf(searchText, StringComparison.Ordinal);

            // If not found, return the original text
            if (pos < 0) return originalText;

            // Construct the new string by combining prefix, replacement, and suffix
            return originalText.Substring(0, pos) + replaceText + originalText.Substring(pos + searchText.Length);
        }

        /// <summary>
        /// Finds all characters in the input string that do not match the provided regular expression pattern.
        /// </summary>
        /// <param name="input">The input string to analyze.</param>
        /// <param name="pattern">The regex pattern to match against.</param>
        /// <returns>An array of distinct characters that do not match the pattern.</returns>
        public static char[] FindNonMatchingCharacters(this string input, string pattern)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern)) return new char[0];

            // Create a regex pattern
            Regex regex = new Regex(pattern);

            // Filter and return only distinct characters that don't match the pattern
            return input.Where(c => !regex.IsMatch(c.ToString())).Distinct().ToArray();
        }

        /// <summary>
        /// Finds and formats all non-matching characters in the input string based on the provided pattern.
        /// Special characters like space, newline, and tab are described in readable words.
        /// </summary>
        /// <param name="input">The input string to analyze.</param>
        /// <param name="pattern">The regex pattern to match against.</param>
        /// <returns>A comma-separated string of non-matching character descriptions.</returns>
        public static string FindNonMatchingCharactersFormatted(this string input, string pattern)
        {
            char[] nonMatchingChars = FindNonMatchingCharacters(input, pattern);

            // Format each special character to a readable label (e.g. [Space], [Tab])
            return string.Join(", ", nonMatchingChars.Select(c => FormatSpecialCharacter(c)));
        }

        /// <summary>
        /// Converts special control characters into a readable label for output.
        /// </summary>
        /// <param name="c">The character to format.</param>
        /// <returns>A descriptive string representing the character.</returns>
        private static string FormatSpecialCharacter(char c)
        {
            switch (c)
            {
                case ' ': return "[Space]";
                case '\n': return "[Newline]";
                case '\t': return "[Tab]";
                default: return c.ToString();
            }
        }
    }
}
