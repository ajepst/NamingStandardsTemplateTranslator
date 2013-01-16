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
        [TestCase("CompoundWordWithInnerWord", "CWWIW")]
        [TestCase("CompoundWordWithInnerWordAndThenAnotherInner", "CWWIW_And_Then_Another_Innr")]
        [TestCase("StaffEducationOrgInformation", "Staff_Ed_Org_Info")]
        [TestCase("AddressLine1", "Address_Line1")]
        [TestCase("EnglishUSIEnglish", "Eng_USI_Eng")]
        [TestCase("USIEnglish", "USI_Eng")]
        public void Should_Abbreviate_Compund_CamelCase_Word(string source, string target)
        {  	
            var result = _translator.Abbreviate(source);
            result.ToLowerInvariant().ShouldEqual(target.ToLowerInvariant());	  	
        }

        [Test]
        public void Should_Pass_Through_Words_Not_Matched_In_Named_Abbreviations()
        {
            const string source = "ShouldPassThrough";
            const string correctResult = "Should_Pass_Through";
            var result = _translator.Abbreviate(source);
            result.ShouldEqual(correctResult);
        }

        [Test]
        public void Should_Accept_Passthrough_In_Between_Abbreviations()
        {
            const string source = "EnglishShouldPassThroughAverage";
            const string correctResult = "Eng_Should_Pass_Through_Ave";
            var result = _translator.Abbreviate(source);
            result.ShouldEqual(correctResult);
        }

        [Test]
        public void Should_handle_special_initial_as_separate_term_when_accompanying_word_is_not_matched()
        {
            const string source = "ParentUSI";
            const string correctResult = "Parent_USI";
            var result = _translator.Abbreviate(source);
            result.ShouldEqual(correctResult);
        }
    }
}