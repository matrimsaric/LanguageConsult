using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs
{
    public enum TENSE_TYPE
    {
        UNKNOWN = 0,
        CURRENT_FUTURE_POSITIVE = 1,
        CURRENT_FUTURE_NEGATIVE = 2,
        PAST_POSITIVE = 3,
        PAST_NEGATIVE = 4,
        COMMAND = 5,
        REQUEST = 6,
        OTHER = 7

    }
    public class Tense
    {
        public bool Polite { get; internal protected set; }
        public TENSE_TYPE tenseType = TENSE_TYPE.UNKNOWN;

        public string Kanji { get; set; }
        public string Hiragana { get; set; }
        public string Romaji { get; set; }
        public string Meaning { get; set; }
        public string Notes { get; set; }

        internal Guid Id { get; set; }

        public Tense(string kanji, string hiragana, string romaji, string meaning, string notes, Guid guid)
        { 
            Kanji = kanji;
            Hiragana = hiragana;
            Romaji = romaji;
            Meaning = meaning;
            Notes = notes;
            Id = guid;
        }
    }
}
