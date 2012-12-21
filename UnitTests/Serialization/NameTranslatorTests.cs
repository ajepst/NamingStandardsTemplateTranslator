using NUnit.Framework;
using NamingStandardsTemplateTranslator;
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
            result.ShouldEqual(UnabbreviatedName);
        }

        [Test]
        public void Should_Abbreviate()
        {
            var result = _translator.Abbreviate(UnabbreviatedName);
            result.ToLowerInvariant().ShouldEqual(AbbreviatedName.ToLowerInvariant());
        }

        [TestCase("CompoundWord", "CW")]
        [TestCase("CompoundWordAverage", "CW_AVE")]
        [TestCase("AverageCompoundWord", "AVE_CW")]
        public void Should_Abbreviate_Compund_CamelCase_Word(string source, string target)
        {
            var result = _translator.Abbreviate(source);
            result.ToLowerInvariant().ShouldEqual(target.ToLowerInvariant());
        }

        [Test]
        public void Should_Pass_Through_Words_Not_Matched_In_Named_Abbreviations()
        {
            const string source = "ShouldPassThrough";
            var result = _translator.Abbreviate(source);
            result.ShouldEqual(source);
        }
    }
}