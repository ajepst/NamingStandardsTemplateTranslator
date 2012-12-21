using System.Xml.Serialization;

namespace NamingStandardsTemplateTranslator.Models
{
    public class GeneralOptions
    {
        [XmlElement]
        public WordSeparation LogicalWordSeparation { get; set; }

        [XmlElement]
        public WordSeparation PhysicalWordSeparation { get; set; }

        [XmlElement]
        public SpecialCharacters SpecialCharacters { get; set; }
    }
}