using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageConsult.Verbs.InflectionControl;

namespace LanguageConsult.Verbs
{
    public abstract class Verb : LoadingInflections
    {
        public string Kanji { get; internal protected set; }
        public string Hiragana { get; internal protected set; }
        public string Romaji { get; internal protected set; }
        public string Meaning { get; internal protected set; }

        public string verbType = "Verb";// TODO LOCALISE HC MESSAGE

        internal Guid Id { get; set; }

        public List<Inflection> inflections = new List<Inflection>();

        public Verb(string kanji, string hiragana, string romaji, string meaning, Guid guid)
        {
            Kanji = kanji;
            Hiragana = hiragana;
            Romaji = romaji;
            Meaning = meaning;
            Id = guid;
        }

        public abstract void LoadInflections(List<Inflection> allInflections);


    }
}
