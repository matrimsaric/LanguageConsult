﻿using LanguageConsult.Verbs.InflectionControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs
{
    public class GodanVerb : Verb
    {
        public GodanVerb(string kanji, string hiragana, string romaji, string meaning, Guid verbGuid) : base(kanji, hiragana, romaji, meaning, verbGuid)
        {
            this.verbType = "Godan Verb";// TODO LOCALISE HC MESSAGE
        }

        public override void LoadInflections(List<Inflection> allInflections)
        {
            this.inflections = allInflections;
        }
    }
}
