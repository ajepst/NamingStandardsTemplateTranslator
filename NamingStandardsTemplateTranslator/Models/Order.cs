using System.Xml.Serialization;

namespace NamingStandardsTemplateTranslator.Models
{
    public class Order
    {
        [XmlAttribute]
        public string ObjectType { get; set; }

        [XmlAttribute]
        public string Part1 { get; set; }

        [XmlAttribute]
        public string Part2 { get; set; }

        [XmlAttribute]
        public string Part3 { get; set; }
    }
}