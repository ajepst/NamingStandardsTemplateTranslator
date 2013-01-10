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
            var notYetMatchedWords = new List<string>();
          //  var currentWord = string.Empty;
          //  var isExtendedWord = false;
            foreach (var word in words)
            {
                notYetMatchedWords.Add(word);
              //  var previousWordPart = currentWord;
              //  currentWord += word;
                var matchedAbbreviation =
                    _namingStandardsTemplate.Abbreviations.FirstOrDefault(
                        abbreviation => abbreviation.LogicalText == string.Join("", notYetMatchedWords));
                if (matchedAbbreviation == null)
                {
                    if (notYetMatchedWords.Count() > 1)
                    {
                        var subMatch = _namingStandardsTemplate.Abbreviations.FirstOrDefault(
                            abbreviation => abbreviation.LogicalText == word);
                        if (subMatch == null)
                        {
                            continue;
                        }
                        matchedAbbreviation = subMatch;
                        notYetMatchedWords.RemoveAt(notYetMatchedWords.Count - 1);
                        abbreviatedWords.Add(string.Join("_",notYetMatchedWords ));
                        abbreviatedWords.Add(matchedAbbreviation.PhysicalText);
                        notYetMatchedWords.Clear();
                    }
                    continue;
                }
                notYetMatchedWords.Clear();
                abbreviatedWords.Add(matchedAbbreviation.PhysicalText);
            }

            if (notYetMatchedWords.Any())
                abbreviatedWords.Add(string.Join("_", notYetMatchedWords));

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