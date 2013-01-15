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
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            var abbreviator = new Abbreviator(source, _namingStandardsTemplate);
            return abbreviator.Abbreviate();
            
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