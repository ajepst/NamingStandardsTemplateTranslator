using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using NamingStandardsTemplateTraslator.Models;
using NamingStandardsTemplateTraslator.Extensions;

namespace NamingStandardsTemplateTraslator
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
            return words.Select(
                word =>
                _namingStandardsTemplate.Abbreviations.First(abbreviation => abbreviation.LogicalText == word)
                                        .PhysicalText)
                        .Aggregate((current, next) => current + "_" + next);
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