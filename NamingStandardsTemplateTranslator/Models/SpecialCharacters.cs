using System.Xml.Serialization;

namespace NamingStandardsTemplateTraslator.Models
{
    public class SpecialCharacters
    {
        [XmlAttribute]
        public string InvalidCharHandling { get; set; }

        [XmlAttribute]
        public string InvalidCharReplacement { get; set; }

        [XmlAttribute]
        public string InvalidCharacters { get; set; }
    }
}