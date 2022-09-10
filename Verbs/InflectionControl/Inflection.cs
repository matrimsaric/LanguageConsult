using LanguageConsult.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs.InflectionControl
{
    public abstract class Inflection 
    {
        private TextValidator textValidator = new TextValidator();
        public Guid Id { get; internal protected set; }

        public Guid VerbId { get; internal protected set; }
        public string Name { get; internal protected set; }

        public List<Tense> Tenses { get; internal protected set; }

        public Inflection(string unsafeName, Guid inflectionGuid, Guid verbId)
        {
            Name = textValidator.GetSafeLanguageString(unsafeName, "Kanji", languageType: LANGUAGE_TYPE.ENGLISH); 
            Id = inflectionGuid;
            Tenses = new List<Tense>();
            VerbId = verbId;
        }

        internal void AddTenses(List<Tense> allTenses)
        {
            Tenses = allTenses;
        }





    }
}
