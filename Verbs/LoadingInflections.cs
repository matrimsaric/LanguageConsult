using LanguageConsult.Verbs.InflectionControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs
{
    internal interface LoadingInflections
    {
        internal void LoadInflections(List<Inflection> allInflections);
    }
}
