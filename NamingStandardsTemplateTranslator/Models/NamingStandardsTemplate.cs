using System.Xml.Serialization;

namespace NamingStandardsTemplateTraslator.Models
{
    [XmlRoot(Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
    public class NamingStandardsTemplate
    {
        [XmlArray]
        [XmlArrayItem(typeof(ObjectConvention))]
        public ObjectConvention[] LogicalObjectConventions { get; set; }

        [XmlArray]
        [XmlArrayItem(typeof (ObjectConvention))]
        public ObjectConvention[] PhysicalObjectConventions { get; set; }

        [XmlArray]
        [XmlArrayItem(typeof(Abbreviation))]
        public Abbreviation[] Abbreviations { get; set; }

        [XmlArray]
        [XmlArrayItem(typeof(Order))]
        public Order[] LogicalOrder { get; set; }

        [XmlArray]
        [XmlArrayItem(typeof(Order))]
        public Order[] PhysicalOrder { get; set; }

        [XmlElement]
        public GeneralOptions GeneralOptions { get; set; }

        [XmlAttribute("NSTemplateDesc")]
        public string Description { get; set; }

        [XmlAttribute("NSTemplateName")]
        public string Name { get; set; }
    }
}
