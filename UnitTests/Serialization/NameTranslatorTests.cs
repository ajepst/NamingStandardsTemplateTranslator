using NUnit.Framework;
using NamingStandardsTemplateTraslator;
using Should;

namespace UnitTests.Serialization
{
    [TestFixture]
    public class NameTranslatorTests
    {
        private readonly Translator _translator;
        private const string PathToNamingStandardsTemplateFile = "Test.xml";
        private const string AbbreviatedName = "ENG_AVE";
        private const string UnabbreviatedName = "EnglishAverage";

        public NameTranslatorTests()
        {
            _translator = new Translator(PathToNamingStandardsTemplateFile);
        }

        [Test]
        public void Should_Unabbreviate()
        {
            var result = _translator.Unabbreviate(AbbreviatedName);
            result.ToLowerInvariant().ShouldEqual(UnabbreviatedName.ToLowerInvariant());
        }

        [Test]
        public void Should_Abbreviate()
        {
            var result = _translator.Abbreviate(UnabbreviatedName);
            result.ShouldEqual(AbbreviatedName);
        }
    }
}