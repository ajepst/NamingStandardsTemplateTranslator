using System.Xml.Serialization;

namespace NamingStandardsTemplateTranslator.Models
{
    public class Abbreviation
    {
        [XmlAttribute]
        public string IsClass { get; set; }

        [XmlAttribute]
        public string IsIllegal { get; set; }

        [XmlAttribute]
        public string IsPrime { get; set; }

        [XmlAttribute]
        public string IsQualifier { get; set; }

        [XmlAttribute]
        public string LogicalText { get; set; }

        [XmlAttribute]
        public string PhysicalText { get; set; }

        [XmlAttribute]
        public string Priority { get; set; }
    }
}