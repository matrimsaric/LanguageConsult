using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs.InflectionControl
{
    public class Inflection 
    {
        internal Guid Id { get; set; }
        public string Name { get; internal protected set; }

        public List<Tense> Tenses { get; internal protected set; }

        public Inflection(string name, Guid inflectionGuid)
        {
            Name = name;
            Id = inflectionGuid;
            Tenses = new List<Tense>();
        }

        internal void AddTenses(List<Tense> allTenses)
        {
            Tenses = allTenses;
        }





    }
}
