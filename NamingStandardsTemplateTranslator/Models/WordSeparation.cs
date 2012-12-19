using System.Xml.Serialization;

namespace NamingStandardsTemplateTraslator.Models
{
    public class WordSeparation
    {
        [XmlAttribute("WordSeperation")]
        public string WordSeparationStrategy { get; set; }

        [XmlAttribute]
        public string WordSeparator { get; set; }
    }
}