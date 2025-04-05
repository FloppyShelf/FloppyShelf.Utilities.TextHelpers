# FloppyShelf.Utilities.TextHelpers

A C# utility library providing extension methods for string sanitization and manipulation. Especially useful for scenarios requiring safe file names or paths.

## Features

- **RemoveInvalidCharacters(this string value)**: Removes characters invalid in both paths and filenames.
- **RemoveInvalidPathCharacters(this string value)**: Strips only path-invalid characters.
- **RemoveInvalidFileNameCharacters(this string value)**: Strips only filename-invalid characters.
- **ReplaceFirstOccurrenceOfStringInText(this string originalText, string searchText, string replaceText)**: Replaces the first occurrence of `searchText` with `replaceText`.
- **ReplaceLastOccurrenceOfStringInText(this string originalText, string searchText, string replaceText)**: Replaces the last occurrence of `searchText` with `replaceText`.
- **FindNonMatchingCharacters(this string input, string pattern)**: Returns all unique characters in `input` that don't match the regex `pattern`.
- **FindNonMatchingCharactersFormatted(this string input, string pattern)**: Returns non-matching characters formatted as `[Space]`, `[Newline]`, etc.

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

```csharp
using FloppyShelf.Utilities.TextHelpers;

string input = "test<>:|?*string";
string sanitized = input.RemoveInvalidCharacters();
// Result: "teststring"

string result = "hello world, hello!".ReplaceFirstOccurrenceOfStringInText("hello", "hi");
// Result: "hi world, hello!"

string pattern = "[a-z]";
char[] nonMatching = "abc123".FindNonMatchingCharacters(pattern);
// Result: { '1', '2', '3' }
```