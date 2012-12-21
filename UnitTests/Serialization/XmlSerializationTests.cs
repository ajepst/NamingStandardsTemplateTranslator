using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using NamingStandardsTemplateTranslator.Models;
using Should;

namespace UnitTests.Serialization
{
    [TestFixture]
    public class XmlSerializationTests
    {
         [Test]
         public void Should_Deserialize_To_Model_From_Xml()
         {
             var serializer = new XmlSerializer(typeof (NamingStandardsTemplate));
             const string xmlPath = "Test.xml";
             var reader = new StreamReader(xmlPath);
             var template = (NamingStandardsTemplate) serializer.Deserialize(reader);

             template.ShouldNotBeNull();
             template.Name.ShouldNotBeEmpty();
             template.Description.ShouldNotBeEmpty();
             template.LogicalObjectConventions.ShouldNotBeEmpty();
             template.PhysicalObjectConventions.ShouldNotBeEmpty();
             template.Abbreviations.ShouldNotBeEmpty();
             template.LogicalOrder.ShouldNotBeEmpty();
             template.PhysicalOrder.ShouldNotBeEmpty();
             template.GeneralOptions.ShouldNotBeNull();
         }
    }
}