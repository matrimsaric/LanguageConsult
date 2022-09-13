using LanguageConsult.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs.InflectionControl
{
    public class StandardPolite : Inflection
    {

        public StandardPolite(Guid verbId) : base(verbId)
        {
            base.Name = "Standard Polite";

            base.Tenses.Add(new Tense(verbId, TENSE_TYPE.CURRENT_FUTURE_POSITIVE));
            base.Tenses.Add(new Tense(verbId, TENSE_TYPE.CURRENT_FUTURE_NEGATIVE));
            base.Tenses.Add(new Tense(verbId, TENSE_TYPE.PAST_POSITIVE));
            base.Tenses.Add(new Tense(verbId, TENSE_TYPE.PAST_NEGATIVE));
        }
    }
}
