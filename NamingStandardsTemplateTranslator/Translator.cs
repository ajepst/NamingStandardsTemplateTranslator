using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NamingStandardsTemplateTraslator.Models;

namespace NamingStandardsTemplateTraslator
{
    public class Translator
    {
        public string Execute(NamingStandardsTemplate template, string shortName)
        {
            var pieces = shortName.Split('_');

            return
                pieces.Select(piece => template.Abbreviations.First(x => x.PhysicalText.ToUpper() == piece.ToUpper()).LogicalText)
                      .Aggregate("", (current, translatedPiece) => current + translatedPiece);
        }
    }
}