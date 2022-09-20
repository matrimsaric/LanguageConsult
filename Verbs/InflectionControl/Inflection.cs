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
        public Guid VerbId { get; internal protected set; }
        public string Name { get; internal protected set; }

        public Tense[] Tenses { get; internal protected set; }

        public Inflection(Guid verbId)
        {
            Tenses = new Tense[4];
            VerbId = verbId;




        }

       internal void AddTenses(List<Tense> allTenses)
        {
            foreach(Tense tense in allTenses)
            {
                // find match in current
                for(int i = 0; i < Tenses.Length; i++)
                {
                    if (Tenses[i].tenseType == tense.tenseType)
                    {
                        Tenses[i] = tense;
                    }
                }
              

            }
        }

        public bool IsValid()
        {
            foreach(Tense tense in Tenses)
            {
                if(tense.IsValid() == false)
                {
                    return false;
                }
            }

            return true;
        }





    }
}
