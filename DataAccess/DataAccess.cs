using LanguageConsult.Verbs;
using LanguageConsult.Verbs.InflectionControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.DataAccess
{
    public abstract class DataAccess
    {
        public abstract Task<bool> SaveVerb(Verb verbToSave);

        public abstract Task<Verb> LoadSpecificVerb(Guid verbId);

        public abstract Task<DataTable> LoadAllVerbs();

        public abstract Task<bool> SaveTense(Tense tenseToSave);

        public abstract Task<Tense> LoadSpecificTense(Guid tenseId);

        public abstract Task<List<Tense>> LoadAllTensesForInflection(Guid inflectionId);

        public abstract Task<bool> DeleteVerb(Verb verbToDelete);

        public abstract Task<bool> DeleteTense(Tense tenseToDelete);
    }
}
