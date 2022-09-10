using LanguageConsult.Verbs.InflectionControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs
{
    public class GodanVerb : Verb
    {
        public GodanVerb(string unsafeKanji, string unsafeHiragana, string unsafeRomaji, string unsafeMeaning, Guid verbGuid, string unsafeKanjiCharacter, bool current)
            : base(unsafeKanji, unsafeHiragana, unsafeRomaji, unsafeMeaning, verbGuid, unsafeKanjiCharacter, current)
        {
            this.verbType = VERB_TYPE.GODAN;
        }

        public override void LoadInflections(List<Inflection> allInflections)
        {
            this.inflections = allInflections;
        }


        public override string ToString()
        {
            return $"Kanji: {Kanji} Hiragana: {Hiragana} Romaji: {Romaji} Meaning: {Meaning} VerbType: Godan";
        }
    }
}
