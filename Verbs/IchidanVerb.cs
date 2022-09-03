using LanguageConsult.Verbs.InflectionControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs
{
    public class IchidanVerb : Verb
    {

        public IchidanVerb(string kanji, string hiragana, string romaji, string meaning, Guid verbGuid) : base (kanji, hiragana, romaji, meaning, verbGuid)  
        {
            this.verbType = VERB_TYPE.ICHIDAN;
        }

        public override void LoadInflections(List<Inflection> allInflections)
        {
            this.inflections = allInflections;
        }

        public override string ToString()
        {
            return $"Kanji: {Kanji} Hiragana: {Hiragana} Romaji: {Romaji} Meaning: {Meaning} VerbType: Ichidan";
        }
    }
}
