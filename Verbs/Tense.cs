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
        KANJI = 2
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

        internal Guid Id { get; set; }

        public Tense(string unsafeKanji, string unsafeHiragana, string unsafeRomaji, string unsafeMeaning, string unsafeNotes,
            Guid guid, TENSE_TYPE setTenseType, bool polite = false)
        {
            
            if(guid == Guid.Empty)
            {
                throw new ArgumentException(nameof(guid));
            }
            if(setTenseType == TENSE_TYPE.UNKNOWN)
            {
                throw new ArgumentException(nameof(setTenseType));
            }
            Kanji = GetSafeString(unsafeKanji, "Kanji", languageType: LANGUAGE_TYPE.KANJI);
            Hiragana = GetSafeString(unsafeHiragana, "Hiragana", languageType: LANGUAGE_TYPE.HIRAGANA);
            Romaji = GetSafeString(unsafeRomaji, "Romaji");
            Meaning = GetSafeString(unsafeMeaning, "Meaning");
            Notes = GetSafeString(unsafeNotes, "Notes", languageType: LANGUAGE_TYPE.ENGLISH, checkForEmpty: false);
            Id = guid;
            Polite = polite;
            tenseType = setTenseType;
        }

        private string GetSafeString(string unsafeValue, string namingType, LANGUAGE_TYPE languageType = LANGUAGE_TYPE.ENGLISH,   bool checkForEmpty = true)
        {


            if (checkForEmpty == true)
            {
                if (string.IsNullOrWhiteSpace(unsafeValue))
                {
                    throw new ArgumentNullException(nameof(namingType));
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(unsafeValue))
                {
                    // dont care about empty for this field so pass the check and carry on
                    return String.Empty;
                }

            }


            string safeValue = String.Empty;
            bool passedTest = false;

            switch (languageType)
            {
                case LANGUAGE_TYPE.HIRAGANA:
                    passedTest = textValidator.ValidateHiragana(unsafeValue, out safeValue);
                    break;
                case LANGUAGE_TYPE.KANJI:
                    passedTest = textValidator.ValidateKanji(unsafeValue, out safeValue);
                    break;
                default:
                    passedTest = textValidator.ValidateText(unsafeValue, out safeValue);
                    break;
            }
            


            // security check
            if (!passedTest)
            {
                throw new AuthenticationException(namingType);
            }

            return safeValue;
        }
    }
}
