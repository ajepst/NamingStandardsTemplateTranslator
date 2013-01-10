﻿using System.Collections.Generic;

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
            for (var currentLetterIndex = 1; currentLetterIndex < source.Length; currentLetterIndex++)
            {
                if (!char.IsUpper(source, currentLetterIndex))
                {
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