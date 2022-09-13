using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageConsult.Security;
using LanguageConsult.Verbs.InflectionControl;

namespace LanguageConsult.Verbs
{
    public enum VERB_TYPE{
        UNKNOWN = 0,
        ICHIDAN = 1,
        GODAN = 2,
        EXCEPTION = 3
}
    public abstract class Verb 
    {
        private TextValidator textValidator = new TextValidator();
        public string Kanji { get; internal protected set; }
        public string Hiragana { get; internal protected set; }
        public string Romaji { get; internal protected set; }
        public string Meaning { get; internal protected set; }
        public string KanjiCharacter { get; internal protected set; }
        public bool VerbCurrent { get; internal protected set; }

        public VERB_TYPE verbType = VERB_TYPE.UNKNOWN;

        public Guid Id { get; internal protected set; }

        public List<Inflection> inflections = new List<Inflection>();

        
        public Verb(string unsafeKanji, string unsafeHiragana, string unsafeRomaji, 
            string unsafeMeaning, Guid guid, string unsafeKanjiCharacter, bool verbCurrent)
        {
            Kanji = textValidator.GetSafeLanguageString(unsafeKanji, "Kanji", languageType: LANGUAGE_TYPE.KANJI);
            Hiragana = textValidator.GetSafeLanguageString(unsafeHiragana, "Hiragana", languageType: LANGUAGE_TYPE.HIRAGANA);
            Romaji = textValidator.GetSafeLanguageString(unsafeRomaji, "Romaji", languageType: LANGUAGE_TYPE.ENGLISH);
            Meaning = textValidator.GetSafeLanguageString(unsafeMeaning, "Meaning", languageType: LANGUAGE_TYPE.ENGLISH);
            VerbCurrent = verbCurrent;
            KanjiCharacter = textValidator.GetSafeLanguageString(unsafeKanjiCharacter, "Kanji", languageType: LANGUAGE_TYPE.KANJI);
            if (guid != Guid.Empty)
                Id = guid;
        }

        public void LoadInflections()
        {
            // Inflections do not differentiate on verb type and 
            // are loaded from classes that contain the relevant info
            // and not stored on the db mainly as that info would then get
            // replicated across all the verbs wasting db space..
            // This point will be the one point stop to generate the relevant
            // inflections when added to the class structure within the Verbs>>InflectionControl folder
            inflections.Add(new StandardCasual(Id));
            inflections.Add(new StandardPolite(Id));

        }

        

    }
}
