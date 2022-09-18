﻿using LanguageConsult.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs.InflectionControl
{
    public class StandardCasual : Inflection
    {

        public StandardCasual(Guid verbId) : base(verbId)
        {
            base.Name = "Standard Casual";

            base.Tenses.Add(new Tense(verbId, TENSE_TYPE.CURRENT_FUTURE_POSITIVE, this.GetType().Name));
            base.Tenses.Add(new Tense(verbId, TENSE_TYPE.CURRENT_FUTURE_NEGATIVE, this.GetType().Name));
            base.Tenses.Add(new Tense(verbId, TENSE_TYPE.PAST_POSITIVE, this.GetType().Name));
            base.Tenses.Add(new Tense(verbId, TENSE_TYPE.PAST_NEGATIVE, this.GetType().Name));
        }
    }
}
