using System.Xml.Serialization;

namespace NamingStandardsTemplateTraslator.Models
{
    public  class ObjectConvention
    {
        [XmlAttribute]
        public string CaseConvention { get; set; }

        [XmlAttribute]
        public string ConstraintConvention { get; set; }

        [XmlAttribute]
        public string MaxLength { get; set; }

        [XmlAttribute]
        public string ObjectType { get; set; }

        [XmlAttribute]
        public string Prefix { get; set; }

        [XmlAttribute]
        public string Suffix { get; set; }
    }
}