using System.Xml.Serialization;

namespace NamingStandardsTemplateTranslator.Models
{
    public class WordSeparation
    {
        [XmlAttribute("WordSeperation")]
        public string WordSeparationStrategy { get; set; }

        [XmlAttribute]
        public string WordSeparator { get; set; }
    }
}