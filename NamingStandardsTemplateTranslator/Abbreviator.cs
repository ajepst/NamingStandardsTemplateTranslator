﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NamingStandardsTemplateTranslator.Models;
using NamingStandardsTemplateTranslator.Extensions;

namespace NamingStandardsTemplateTranslator
{
    public class Abbreviator
    {
        private readonly string _originalSource;
        private string _source;
        private readonly NamingStandardsTemplate _namingStandardsTemplate;
        private bool _reduced;
        private int _currentMatchCount = 0;
        private readonly List<object> _matchList = new List<object>();
        private static readonly Regex Pattern = new Regex(@"([a-zA-Z0-9]+)(?!\})");

        public Abbreviator(string source, NamingStandardsTemplate namingStandardsTemplate)
        {
            _source = source;
            _originalSource = source;
            _namingStandardsTemplate = namingStandardsTemplate;
        }

        public string Abbreviate()
        {
            if (!_reduced)
                lock (_namingStandardsTemplate)
                    if (!_reduced)
                    {
                        Reduce();
                        _reduced = true;
                    }
            return _source;
        }

        private void Reduce()
        {
            try
            {
                ApplyAbbreviationsInNamingStandardTemplate();
                SeparateUnmatchedWords();
                FormatMatchesAndStripExtraUnderscores();
                RemoveLeadingAndTrailingUnderscores();
            }
            catch (Exception exception)
            {
                throw new AbbreviationException(string.Format("Failed to reduce {0}. Current reduced state: {1}", _originalSource, _source));
            }
        }

        private void SeparateUnmatchedWords()
        {
            var currentIndex = 0;
            var stringBuilder = new StringBuilder();
            foreach (Match match in Pattern.Matches(_source))
            {
                stringBuilder.Append(_source.Substring(currentIndex, match.Index - currentIndex));
                stringBuilder.Append(SeperatedUnmatchedPart(match.Value));
                currentIndex = match.Index + match.Length;
            }
            if (currentIndex < _source.Length)
            {
                stringBuilder.Append(_source.Substring(currentIndex));
            }
            _source = stringBuilder.ToString();
        }

        private static string SeperatedUnmatchedPart(string value)
        {
            var result = string.Empty;
            foreach (var word in value.SplitOnUpperCase())
                if (word.Length > 1)
                    result += "_" + word;
                else
                    result += word;
            return result;
        }

        private void RemoveLeadingAndTrailingUnderscores()
        {
            if (_source.StartsWith("_"))
                _source = _source.Substring(1);
            if (_source.EndsWith("_"))
                _source = _source.Substring(0, _source.Length - 1);
        }

        private void FormatMatchesAndStripExtraUnderscores()
        {
            _source = string.Format(_source, _matchList.ToArray()).Replace("__", "_");
        }

        private void ApplyAbbreviationsInNamingStandardTemplate()
        {
            foreach (
                var abbreviation in
                    _namingStandardsTemplate.Abbreviations.OrderBy(abbreviation => abbreviation.Priority)
                                            .ThenByDescending(abbreviation => abbreviation.LogicalText.Length)
                                            .ThenBy(abbreviation => abbreviation.LogicalText))
            {
                if (! Regex.IsMatch(_source, ".*"+abbreviation.LogicalText+"([^a-z].*)?$"))
                    continue;
                _matchList.Add(abbreviation.PhysicalText);
                _source = _source.Replace(abbreviation.LogicalText, "_{" + _currentMatchCount + "}_");
                _currentMatchCount += 1;
            }
        }
    }
}