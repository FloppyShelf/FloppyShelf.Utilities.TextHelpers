# FloppyShelf.Utilities.TextHelpers

C# utility library that provides various text manipulation functions. It includes methods to sanitize strings by removing invalid characters for file paths and file names, replace specific occurrences of substrings, and identify characters that don't match a given regular expression pattern. This library is especially useful for handling user inputs and ensuring clean and valid file paths and names in file-related operations.

## Features

- **RemoveInvalidCharacters(string value)**: Removes all characters from the input string that are invalid in either file paths or file names.
- **RemoveInvalidPathCharacters(string value)**: Removes all characters from the input string that are invalid in file paths.
- **RemoveInvalidFileNameCharacters(string value)**: Removes all characters from the input string that are invalid in file names.
- **ReplaceFirstOccurrenceOfStringInText(string originalText, string searchText, string replaceText)**: Replaces the first occurrence of a specified substring within a given text.
- **ReplaceLastOccurrenceOfStringInText(string originalText, string searchText, string replaceText)**: Replaces the last occurrence of a specified substring within a given text.
- **FindNonMatchingCharacters(string input, string pattern)**: Finds all characters in the input string that do not match the provided regular expression pattern.
- **FindNonMatchingCharactersFormatted(string input, string pattern)**: Finds and formats all non-matching characters in the input string based on the provided pattern. Special characters like space, newline, and tab are described in words.

## Installation

You can install the **FloppyShelf.Utilities.TextHelpers** library via [NuGet](https://www.nuget.org/).

To install the library, run the following command in the Package Manager Console:

```
Install-Package FloppyShelf.Utilities.TextHelpers
```

Alternatively, you can add it via .NET CLI:

```
dotnet add package FloppyShelf.Utilities.TextHelpers
```

## Usage

### Example 1: Removing Invalid Characters

```csharp
using FloppyShelf.Utilities.TextHelpers;

string input = "test<>:|?*string";
string sanitized = StringSanitizer.RemoveInvalidCharacters(input);
Console.WriteLine(sanitized);  // Output: teststring
```

### Example 2: Replace First Occurrence of a String

```csharp
using FloppyShelf.Utilities.TextHelpers;

string input = "hello world, hello!";
string result = StringSanitizer.ReplaceFirstOccurrenceOfStringInText(input, "hello", "hi");
Console.WriteLine(result);  // Output: hi world, hello!
```

### Example 3: Find Non-Matching Characters

```csharp
using FloppyShelf.Utilities.TextHelpers;

string input = "abc123";
string pattern = "[a-z]";
char[] nonMatching = StringSanitizer.FindNonMatchingCharacters(input, pattern);
Console.WriteLine(string.Join(", ", nonMatching));  // Output: 1, 2, 3
```