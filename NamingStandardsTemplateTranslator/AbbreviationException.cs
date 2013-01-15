using System;

namespace NamingStandardsTemplateTranslator
{
    public class AbbreviationException : Exception
    {
        public AbbreviationException()
        {
        }

        public AbbreviationException(string message) : base(message)
        {
        }

        public AbbreviationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}