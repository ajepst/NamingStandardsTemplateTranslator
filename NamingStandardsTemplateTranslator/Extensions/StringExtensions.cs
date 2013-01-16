using System.Collections.Generic;

namespace NamingStandardsTemplateTranslator.Extensions
{
    public static class StringExtensions
    {
        public static string[] SplitOnUpperCase(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return new string[] { };
            var words = new List<string>();
            var wordStartIndex = 0;
            var maybeAbbreviation = false;
            for (var currentLetterIndex = 0; currentLetterIndex < source.Length; currentLetterIndex++)
            {
                if (!char.IsUpper(source, currentLetterIndex))
                {
                    if (maybeAbbreviation && (currentLetterIndex - wordStartIndex) > 1)
                    {
                        // we found an unmatched word following a caps abbreviation. back out this word and separate
                        words.Add(source.Substring(wordStartIndex, currentLetterIndex - wordStartIndex-1));
                        wordStartIndex = currentLetterIndex - 1;
                    }
                    maybeAbbreviation = false;
                    continue;
                }
                if (maybeAbbreviation && char.IsUpper(source, currentLetterIndex))
                    continue;
                words.Add(source.Substring(wordStartIndex, currentLetterIndex - wordStartIndex));
                wordStartIndex = currentLetterIndex;
                maybeAbbreviation = true;
            }
            words.Add(source.Substring(wordStartIndex));
            return words.ToArray();
        }
    }
}