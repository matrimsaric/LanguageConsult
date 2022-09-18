using LanguageConsult.Verbs;
using LanguageConsult.Verbs.InflectionControl;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LanguageConsult.DataAccess.MSSqlDataAccess
{
    public class MsSqlDataAccess : DataAccess
    {
        private SqlDataAccess sqlClient = new SqlDataAccess();

        private Inflection emptyInflection = new StandardCasual(Guid.Empty);
        private Tense emptyTense = new Tense( "書", "あ", "A", "A", "A", Guid.Empty, TENSE_TYPE.PAST_POSITIVE,Guid.Empty, "MADEUP");
        private Verb emptyVerb = new IchidanVerb("書", "あ", "A", "A", Guid.Empty, "書", false);
        

        public override Task<List<Tense>> LoadAllTensesForInflection(Guid verbId)
        {
            string sql = $"SELECT * FROM Tense WHERE VerbId =  '{verbId}'";

            DataTable response = sqlClient.GetData(sql);

            List<Tense> list = new List<Tense>();

            foreach (DataRow tenseRow in response.Rows)
            {
                Guid tenseId = (Guid)tenseRow["TenseId"];
                string kanji = (string)tenseRow["Kanji"];
                string hiragana = (string)tenseRow["Hiragana"];
                string romaji = (string)tenseRow["Romaji"];
                string meaning = (string)tenseRow["Meaning"];
                string notes = (string)tenseRow["Notes"];
                TENSE_TYPE tenseType = (TENSE_TYPE)tenseRow["TenseType"];
                string inflectionClass = (string)tenseRow["InflectionClass"];

                Tense tense = new Tense(kanji, hiragana, romaji, meaning, notes, tenseId, tenseType, verbId,inflectionClass);

                list.Add(tense);
            }
            if (list?.Count > 0)
            {
                return Task.FromResult(list);
            }
            
            list = new List<Tense>();
            list.Add(emptyTense);
            return Task.FromResult(list);
        }

        public override Task<DataTable> LoadAllVerbs()
        {
            // TODO Add in search parameter options so can restrict data and possibly a delegate to report when loaded..
            string sql = $"SELECT * FROM Verb ";

            DataTable response = sqlClient.GetData(sql);

            return Task.FromResult(response);
        }

       

        public override Task<Tense> LoadSpecificTense(Guid tenseId)
        {
            string sql = $"SELECT * FROM Tense WHERE TenseId =  '{tenseId}'";

            DataTable response = sqlClient.GetData(sql);


            if (response.Rows.Count > 0)
            {
                // get first row and populate fields
                DataRow initial = response.Rows[0];
                Guid verbId = (Guid)initial["VerbId"];
                string kanji = (string)initial["Kanji"];
                string hiragana = (string)initial["Hiragana"];
                string romaji = (string)initial["Romaji"];
                string meaning = (string)initial["Meaning"];
                string notes = (string)initial["Notes"];
                string inflectionClass = (string)initial["InflectionClass"];

                TENSE_TYPE tenseType = (TENSE_TYPE)initial["TenseType"];

                Tense tense = new Tense(kanji, hiragana,romaji,meaning, notes, tenseId, tenseType, verbId, inflectionClass);

                return Task.FromResult(tense);



            }
            return Task.FromResult(emptyTense);
        }

        public override Task<Verb> LoadSpecificVerb(Guid verbId)
        {
            string sql = $"SELECT * FROM Verb WHERE VerbId =  '{verbId}'";

            DataTable response = sqlClient.GetData(sql);
            

            if (response.Rows.Count > 0)
            {
                // get first row and populate fields
                DataRow initial = response.Rows[0];
                string kanji = (string)initial["Kanji"];
                string hiragana = (string)initial["Hiragana"];
                string romaji = (string)initial["Romaji"];
                string meaning = (string)initial["Meaning"];
                string kanjiCharacter = (string)initial["KanjiCharacter"];
                bool verbCurrent = (bool)initial["VerbCurrent"];
                
                VERB_TYPE verbType = (VERB_TYPE)initial["VerbType"];
                Verb createdVerb = null;

                switch (verbType)
                {
                    case VERB_TYPE.ICHIDAN:
                        createdVerb = new IchidanVerb(kanji, hiragana, romaji, meaning, verbId, kanjiCharacter, verbCurrent);
                        break;
                    case VERB_TYPE.GODAN:
                        createdVerb = new GodanVerb(kanji, hiragana, romaji, meaning, verbId, kanjiCharacter, verbCurrent);
                        break;
                    case VERB_TYPE.EXCEPTION:

                        break;
                }

                // load tenses for verb
                Task<List<Tense>> tenseList = this.LoadAllTensesForInflection(verbId);
                // loop inflections in verb - note if inflection class is removed this prevents dodgy data appearing
                // as nothing for the tense to attach to
                if(createdVerb != null && tenseList.Result != null)
                {
                    List<Tense> allTenses = tenseList.Result;
                    foreach (Inflection inf in createdVerb.inflections)
                    {
                        // use relection to get the class name
                        string inflectionClass = inf.GetType().Name.ToString();

                        // search for this among tenses

                        List<Tense> foundTenses = allTenses.Where(x => x.InflectionClass == inflectionClass).ToList();

                        if(foundTenses != null)
                        {
                            inf.AddTenses(foundTenses);
                        }
                    }
                }
                return Task.FromResult(createdVerb);



            }
            
            return Task.FromResult(emptyVerb);


        }

       
        public override Task<bool> SaveTense(Tense tenseToSave)
        {
            var cmd = new SqlCommand("SaveTense");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = tenseToSave.Id;
            cmd.Parameters.Add("@VerbId", SqlDbType.UniqueIdentifier).Value = tenseToSave.VerbId;
            cmd.Parameters.Add("@InflectionClass", SqlDbType.NVarChar, 30).Value = tenseToSave.InflectionClass;
            cmd.Parameters.Add("@Kanji", SqlDbType.NVarChar, 30).Value = tenseToSave.Kanji;
            cmd.Parameters.Add("@Hiragana", SqlDbType.NVarChar, 30).Value = tenseToSave.Hiragana;
            cmd.Parameters.Add("@Romaji", SqlDbType.NVarChar, 30).Value = tenseToSave.Romaji;
            cmd.Parameters.Add("@Meaning", SqlDbType.NVarChar, 50).Value = tenseToSave.Meaning;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value = tenseToSave.Notes;
            cmd.Parameters.Add("@TenseType", SqlDbType.Int).Value = tenseToSave.tenseType;

            string response = String.Empty;

            try
            {
                sqlClient.ExecuteCommand(cmd);
            }
            catch (Exception exc)
            {
                throw new Exception("SaveTense failed", exc);
            }

            return Task.FromResult(true);
        }

        public override Task<bool> SaveVerb(Verb verbToSave)
        {
            var cmd = new SqlCommand("SaveVerb");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = verbToSave.Id;
            cmd.Parameters.Add("@Kanji", SqlDbType.NVarChar, 30).Value = verbToSave.Kanji;
            cmd.Parameters.Add("@Hiragana", SqlDbType.NVarChar, 30).Value = verbToSave.Hiragana;
            cmd.Parameters.Add("@Romaji", SqlDbType.NVarChar, 30).Value = verbToSave.Romaji;
            cmd.Parameters.Add("@Meaning", SqlDbType.NVarChar, 50).Value = verbToSave.Meaning;
            cmd.Parameters.Add("@VerbType", SqlDbType.Int).Value = verbToSave.verbType;
            cmd.Parameters.Add("@KanjiCharacter", SqlDbType.NVarChar, 10).Value = verbToSave.KanjiCharacter;
            cmd.Parameters.Add("@VerbCurrent", SqlDbType.Bit).Value = verbToSave.VerbCurrent;

            string response = String.Empty;

            try
            {
                sqlClient.ExecuteCommand(cmd);
            }
            catch(Exception exc)
            {
                throw new Exception("SaveVerb failed", exc);
            }

            return Task.FromResult(true);
            


        }

        public override Task<bool> DeleteVerb(Verb verbToDelete)
        {
            string sql = $"DELETE Verb WHERE VerbId =  '{verbToDelete.Id}'";
            sqlClient.ExecuteNonQuery(sql);



            sql = $"DELETE Tense WHERE VerbId =  '{verbToDelete.Id}'";

            sqlClient.ExecuteNonQuery(sql);



            return Task.FromResult(true);
        }


        public override Task<bool> DeleteTense(Tense tenseToDelete)
        {
            string sql = $"DELETE Tense WHERE TenseId =  '{tenseToDelete.Id}'";

            sqlClient.ExecuteNonQuery(sql);


            return Task.FromResult(true);
        }
    }
}
