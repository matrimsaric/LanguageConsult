using LanguageConsult.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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

    internal enum LANGUAGE_TYPE
    {
        ENGLISH = 0,
        HIRAGANA = 1,
        KANJI = 2,
        ONLY_KANJI = 3
    }
    public class Tense
    {
        private TextValidator textValidator = new TextValidator();
        public bool Polite { get; internal protected set; }
        public TENSE_TYPE tenseType = TENSE_TYPE.UNKNOWN;

        public string Kanji { get; internal protected set; }
        public string Hiragana { get; internal protected set; }
        public string Romaji { get; internal protected set; }
        public string Meaning { get; internal protected set; }
        public string Notes { get; internal protected set; }

        public Guid Id { get; internal protected set; }

        public Guid VerbId { get; internal protected set; }

        public string InflectionClass { get; protected set; }

        public Tense(Guid verbId, TENSE_TYPE setTenseType, string inflectionClass )
        {
            VerbId = verbId;
            tenseType = setTenseType;

            Kanji = String.Empty;
            Hiragana = String.Empty;
            Romaji = String.Empty;
            Meaning = String.Empty;
            Notes = String.Empty;
            InflectionClass = inflectionClass;
        }

        public Tense(string unsafeKanji, string unsafeHiragana, string unsafeRomaji, string unsafeMeaning, string unsafeNotes,
            Guid guid, TENSE_TYPE newTense, Guid verbId, string unsafeInlectionClass)
        {
            
            Kanji = textValidator.GetSafeLanguageString(unsafeKanji, "Kanji", languageType: LANGUAGE_TYPE.KANJI);
            Hiragana = textValidator.GetSafeLanguageString(unsafeHiragana, "Hiragana", languageType: LANGUAGE_TYPE.HIRAGANA);
            Romaji = textValidator.GetSafeLanguageString(unsafeRomaji, "Romaji", checkForEmpty: false);
            Meaning = textValidator.GetSafeLanguageString(unsafeMeaning, "Meaning");
            Notes = textValidator.GetSafeLanguageString(unsafeNotes, "Notes", languageType: LANGUAGE_TYPE.ENGLISH, checkForEmpty: false);
            InflectionClass = textValidator.GetSafeLanguageString(unsafeInlectionClass, "InflectionClass");
            Id = guid;

            tenseType = newTense;

            this.VerbId = verbId;
        }

        public void UpdateValues(string unsafeKanji, string unsafeHiragana, string unsafeRomaji, string unsafeMeaning, string unsafeNotes)
        {
            Kanji = textValidator.GetSafeLanguageString(unsafeKanji, "Kanji", languageType: LANGUAGE_TYPE.KANJI);
            Hiragana = textValidator.GetSafeLanguageString(unsafeHiragana, "Hiragana", languageType: LANGUAGE_TYPE.HIRAGANA);
            Romaji = textValidator.GetSafeLanguageString(unsafeRomaji, "Romaji", checkForEmpty: false);
            Meaning = textValidator.GetSafeLanguageString(unsafeMeaning, "Meaning");
            Notes = textValidator.GetSafeLanguageString(unsafeNotes, "Notes", languageType: LANGUAGE_TYPE.ENGLISH, checkForEmpty: false);

            if(Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
        }

        public bool IsValid()
        {
            if(!string.IsNullOrEmpty(Kanji) && !string.IsNullOrEmpty(Hiragana) && !string.IsNullOrEmpty(Meaning))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return tenseType.ToString().PadRight(28) + Meaning;
        }

    }
}
