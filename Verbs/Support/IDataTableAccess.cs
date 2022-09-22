using LanguageConsult.Verbs.InflectionControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.Verbs.Support
{
    public class IDataTableAccess
    {
        private const string INFLECTION_COLUMN = "InflectionName";
        private const string TENSE_COLUMN = "TenseName";
        private const string KANJI_COLUMN = "Kanji";
        private const string HIRAGANA_COLUMN = "Hiragana";
        private const string MEANING_COLUMN = "Meaning";


        public DataTable GetVerbAsDataTable(List<Inflection> inflections)
        {
            DataTable result = new DataTable();

            CreateColumns(result);

            // collect data
            foreach (Inflection inf in inflections)
            {
                

                // loop tenses
                for(int i = 0; i < inf.Tenses.Length; i++)
                {
                    DataRow newRow = result.NewRow();
                    newRow[INFLECTION_COLUMN] = inf.Name;
                    Tense loadedTense = inf.Tenses[i];
                    newRow[TENSE_COLUMN] = loadedTense.tenseType;
                    newRow[KANJI_COLUMN] = loadedTense.Kanji;
                    newRow[HIRAGANA_COLUMN] = loadedTense.Hiragana;
                    newRow[MEANING_COLUMN] = loadedTense.Meaning;
                    result.Rows.Add(newRow);
                }
            }

            return result;
        }

        private void CreateColumns(DataTable target)
        {
            DataColumn inflectionCol = new DataColumn(INFLECTION_COLUMN, typeof(string));
            DataColumn tenseNameCol = new DataColumn(TENSE_COLUMN, typeof(string));
            DataColumn kanjiCol = new DataColumn(KANJI_COLUMN, typeof(string));
            DataColumn hirganaCol = new DataColumn(HIRAGANA_COLUMN, typeof(string));
            DataColumn meaningCol = new DataColumn(MEANING_COLUMN, typeof(string));

            target.Columns.Add(inflectionCol);
            target.Columns.Add(tenseNameCol);
            target.Columns.Add(kanjiCol);
            target.Columns.Add(hirganaCol);
            target.Columns.Add(meaningCol);
        }
    }
}
