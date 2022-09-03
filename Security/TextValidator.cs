using LanguageConsult.Verbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LanguageConsult.Security
{
    internal class TextValidator
    {
        private Regex checkString = new Regex(@"^[a-zA-Z]+$");//^[a-zA-Z]+$  [\\s\\w\\.]*
        private Regex checkHiragana = new Regex(@"^[\u3040-\u309Fー]+$");
         internal bool ValidateText(string unsafeText, out string safeText)
        {
            bool passedSearch = false;
            safeText = String.Empty;

            // space is permitted but is an extra char
            if (!checkString.IsMatch(unsafeText.Replace(" ", String.Empty)))
            {
                safeText = unsafeText.Replace(checkString.ToString(),String.Empty);
            }
            else
            {
                // passed
                safeText = unsafeText;// only time this it permitted
                passedSearch = true;
            }

            return passedSearch;


        }

        internal bool ValidateKanji(string unsafeKanji, out string safeKanji)
        {
            bool passedSearch = false;
            safeKanji = String.Empty;

            // looking for incidence of any standard text
            if (checkString.IsMatch(unsafeKanji))
            {
                safeKanji = unsafeKanji.Replace(checkString.ToString(), String.Empty);
            }
            else
            {
                // passed
                safeKanji = unsafeKanji;// only time this it permitted
                passedSearch = true;
            }

            return passedSearch;


        }

        internal bool ValidateHiragana(string unsafeHirgana, out string safeHiragana)
        {
            bool passedSearch = false;
            safeHiragana = String.Empty;

            if (!checkHiragana.IsMatch(unsafeHirgana))
            {
                safeHiragana = unsafeHirgana.Replace(checkString.ToString(), String.Empty);
            }
            else
            {
                // passed
                safeHiragana = unsafeHirgana;// only time this it permitted
                passedSearch = true;
            }

            return passedSearch;


        }


        internal string GetSafeLanguageString(string unsafeValue, string namingType, LANGUAGE_TYPE languageType = LANGUAGE_TYPE.ENGLISH, bool checkForEmpty = true)
        {
            TextValidator textValidator = new TextValidator();

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
