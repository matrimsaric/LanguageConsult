using LanguageConsult.Verbs.InflectionControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs
{
    public class IchidanVerb : Verb
    {
        
        public IchidanVerb(string unsafeKanji, string unsafeHiragana, string unsafeRomaji, string unsafeMeaning, Guid verbGuid, string unsafeKanjiCharacter, bool current)
            : base (unsafeKanji, unsafeHiragana, unsafeRomaji, unsafeMeaning, verbGuid, unsafeKanjiCharacter, current)  
        {
            this.verbType = VERB_TYPE.ICHIDAN;
            base.verbColor = Color.Green;
            base.verbString = "Ichidan Verb";
        }

       

        public override string ToString()
        {
            return $"Kanji: {Kanji} Hiragana: {Hiragana} Romaji: {Romaji} Meaning: {Meaning} VerbType: Ichidan";
        }
    }
}
