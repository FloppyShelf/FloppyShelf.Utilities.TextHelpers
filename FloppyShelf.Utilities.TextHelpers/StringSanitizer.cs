using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FloppyShelf.Utilities.TextHelpers
{
    /// <summary>
    /// Provides utility methods for sanitizing strings, replacing substrings, and identifying invalid characters.
    /// </summary>
    public static class StringSanitizer
    {
        /// <summary>
        /// Removes all invalid characters from a string that are not allowed in file paths or file names.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>A sanitized string without invalid file path or file name characters.</returns>
        public static string RemoveInvalidCharacters(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            // First, remove invalid path characters
            value = RemoveInvalidPathCharacters(value);

            // Then, remove invalid file name characters
            value = RemoveInvalidFileNameCharacters(value);

            return value;
        }

        /// <summary>
        /// Removes characters from a string that are invalid in file paths.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>A string without invalid file path characters.</returns>
        public static string RemoveInvalidPathCharacters(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            // Get the list of invalid path characters
            char[] invalidChars = Path.GetInvalidPathChars();

            // Filter out invalid characters
            return new string(value.Where(c => !invalidChars.Contains(c)).ToArray());
        }

        /// <summary>
        /// Removes characters from a string that are invalid in file names.
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>A string without invalid file name characters.</returns>
        public static string RemoveInvalidFileNameCharacters(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;

            // Get the list of invalid file name characters
            char[] invalidChars = Path.GetInvalidFileNameChars();

            // Filter out invalid characters
            return new string(value.Where(c => !invalidChars.Contains(c)).ToArray());
        }

        /// <summary>
        /// Replaces the first occurrence of a specified substring in a given text.
        /// </summary>
        public static string ReplaceFirstOccurrenceOfStringInText(string originalText, string searchText, string replaceText)
        {
            return ReplaceOccurrence(originalText, searchText, replaceText, true);
        }

        /// <summary>
        /// Replaces the last occurrence of a specified substring in a given text.
        /// </summary>
        public static string ReplaceLastOccurrenceOfStringInText(string originalText, string searchText, string replaceText)
        {
            return ReplaceOccurrence(originalText, searchText, replaceText, false);
        }

        /// <summary>
        /// Replaces either the first or last occurrence of a specified substring in a given text.
        /// </summary>
        private static string ReplaceOccurrence(string originalText, string searchText, string replaceText, bool firstOccurrence)
        {
            if (string.IsNullOrEmpty(originalText) || string.IsNullOrEmpty(searchText)) return originalText;

            // Find the position of the first or last occurrence
            int pos = firstOccurrence ? originalText.IndexOf(searchText, StringComparison.Ordinal) : originalText.LastIndexOf(searchText, StringComparison.Ordinal);

            // If the search text is not found, return the original string
            if (pos < 0) return originalText;

            // Replace the found occurrence
            return originalText.Substring(0, pos) + replaceText + originalText.Substring(pos + searchText.Length);
        }

        /// <summary>
        /// Finds all characters in a string that do not match the specified regular expression pattern.
        /// </summary>
        public static char[] FindNonMatchingCharacters(string input, string pattern)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern)) return new char[0];

            // Create a regex pattern
            Regex regex = new Regex(pattern);

            // Find characters that do not match the pattern
            return input.Where(c => !regex.IsMatch(c.ToString())).Distinct().ToArray();
        }

        /// <summary>
        /// Finds and formats all non-matching characters in a string based on a regex pattern.
        /// Special characters like space, newline, and tab are described in words.
        /// </summary>
        public static string FindNonMatchingCharactersFormatted(string input, string pattern)
        {
            char[] nonMatchingChars = FindNonMatchingCharacters(input, pattern);
            return string.Join(", ", nonMatchingChars.Select(c => FormatSpecialCharacter(c)));
        }

        /// <summary>
        /// Formats special characters into readable text representations.
        /// </summary>
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
