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

        [Test]
        public void Should_Pass_Through_Words_Not_Matched_In_Named_Abbreviations()
        {
            const string source = "ShouldPassThrough";
            var result = _translator.Abbreviate(source);
            result.ShouldEqual(source);
        }

        [Test]
        public void Should_Accept_Passthrough_In_Between_Abbreviations()
        {
            const string correctResult = "Eng_ShouldPassThrough_Ave";
            const string source = "EnglishShouldPassThroughAverage";
            var result = _translator.Abbreviate(source);
            result.ShouldEqual(correctResult);
        }

        [Test]
        public void Should_handle_special_initial_as_separate_term_when_accompanying_word_is_not_matched()
        {
            const string correctResult = "ParentUSI";
            const string source = "Parent_USI";
            var result = _translator.Abbreviate(source);
            result.ShouldEqual(correctResult);
        }
    }
}