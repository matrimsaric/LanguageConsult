using LanguageConsult.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs.InflectionControl
{
    public class PotentialPolite : Inflection
    {

        public PotentialPolite(Guid verbId) : base(verbId)
        {
            base.Name = "Potential Polite";

            base.Tenses[0] = new Tense(verbId, TENSE_TYPE.CURRENT_FUTURE_POSITIVE, this.GetType().Name);
            base.Tenses[1] = new Tense(verbId, TENSE_TYPE.CURRENT_FUTURE_NEGATIVE, this.GetType().Name);
            base.Tenses[2] = new Tense(verbId, TENSE_TYPE.PAST_POSITIVE, this.GetType().Name);
            base.Tenses[3] = new Tense(verbId, TENSE_TYPE.PAST_NEGATIVE, this.GetType().Name);
        }
    }
}
