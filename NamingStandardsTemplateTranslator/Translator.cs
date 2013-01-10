using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NamingStandardsTemplateTranslator.Extensions;
using NamingStandardsTemplateTranslator.Models;

namespace NamingStandardsTemplateTranslator
{
    public class Translator
    {
        private readonly NamingStandardsTemplate _namingStandardsTemplate;

        public Translator(NamingStandardsTemplate namingStandardsTemplate)
        {
            _namingStandardsTemplate = namingStandardsTemplate;
        }

        public Translator(string pathToNamingStandardsTemplateFile)
        {
            var serializer = new XmlSerializer(typeof(NamingStandardsTemplate));
            var reader = new StreamReader(pathToNamingStandardsTemplateFile);
            _namingStandardsTemplate = (NamingStandardsTemplate)serializer.Deserialize(reader);
        }

        public string Abbreviate(string source)
        {
            var words = source.SplitOnUpperCase();
            var abbreviatedWords = new List<string>();
            var currentWord = string.Empty;
            var isExtendedWord = false;

            foreach (var word in words)
            {
                var previousWordPart = currentWord;
                currentWord += word;
                var matchedAbbreviation =
                    _namingStandardsTemplate.Abbreviations.FirstOrDefault(
                        abbreviation => abbreviation.LogicalText == currentWord);
                if (matchedAbbreviation == null)
                {
                    if (isExtendedWord)
                    {
                        var subMatch = _namingStandardsTemplate.Abbreviations.FirstOrDefault(
                            abbreviation => abbreviation.LogicalText == word);
                        if (subMatch == null)
                        {
                            continue;
                        }
                        matchedAbbreviation = subMatch;
                        abbreviatedWords.Add(previousWordPart);
                        abbreviatedWords.Add(matchedAbbreviation.PhysicalText);
                        currentWord = string.Empty;
                        isExtendedWord = false;
                        continue;

                    }
                    isExtendedWord = true;
                    continue;
                }
                abbreviatedWords.Add(matchedAbbreviation.PhysicalText);
                currentWord = string.Empty;
                isExtendedWord = false;
            }

            if (!string.IsNullOrEmpty(currentWord))
                abbreviatedWords.Add(currentWord);

            return abbreviatedWords.Aggregate((aggregated, next) => aggregated + "_" + next);
        }

        

        public string Unabbreviate(string source)
        {
            var pieces = source.Split('_');

            return
                pieces.Select(piece => _namingStandardsTemplate.Abbreviations.First(x => x.PhysicalText.ToUpper() == piece.ToUpper()).LogicalText)
                      .Aggregate("", (current, translatedPiece) => current + translatedPiece);
        }
    }
}