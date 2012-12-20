using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using NamingStandardsTemplateTraslator;
using NamingStandardsTemplateTraslator.Models;
using Should;

namespace UnitTests.Serialization
{
    [TestFixture]
    public class NameTranslatorTests
    {
        [Test]
        public void Should_Translate_To_Long_string()
        {
            var serializer = new XmlSerializer(typeof (NamingStandardsTemplate));
            const string xmlPath = "Test.xml";
            var reader = new StreamReader(xmlPath);
            var template = (NamingStandardsTemplate) serializer.Deserialize(reader);

            var shortName = "ENG_AVE";

            var translator = new Translator();
            var result = translator.Execute(template, shortName);
            result.ShouldEqual("EnglishAverage");
        }
    }
}