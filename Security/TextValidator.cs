using System;
using System.Collections.Generic;
using System.Linq;
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


    }
}
