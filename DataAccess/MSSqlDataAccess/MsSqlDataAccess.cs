using LanguageConsult.Verbs;
using LanguageConsult.Verbs.InflectionControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LanguageConsult.DataAccess.MSSqlDataAccess
{
    public class MsSqlDataAccess : DataAccess
    {
        private SqlDataAccess sqlClient = new SqlDataAccess();
        public override Task<List<Inflection>> LoadAllInflectionsForVerb(Guid verbId)
        {
            string sql = $"SELECT * FROM Inflection WHERE VerbId =  '{verbId}'";

            DataTable response = sqlClient.GetData(sql);
            
            List<Inflection> list = new List<Inflection>();

            foreach(DataRow inflRow in response.Rows)
            {
                DataRow initial = response.Rows[0];
                Guid inflectionId = (Guid)initial["InflectionId"];
                string name = (string)initial["InflectionName"];

                Inflection inflec = new Inflection(name, inflectionId, verbId);

                list.Add(inflec);
            }
            if (list?.Count > 0)
            {
                return Task.FromResult(list);
            }
            Inflection emptyResponse = new Inflection(String.Empty, Guid.Empty, Guid.Empty);
            list = new List<Inflection>();
            list.Add(emptyResponse);
            return Task.FromResult(list);
        }

        public override Task<List<Tense>> LoadAllTensesForInflection(Guid inflectionId)
        {
            string sql = $"SELECT * FROM Tense WHERE InflectionId =  '{inflectionId}'";

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
                bool polite = (bool)tenseRow["Polite"];
                TENSE_TYPE tenseType = (TENSE_TYPE)tenseRow["TenseType"];

                Tense tense = new Tense(kanji, hiragana, romaji, meaning, notes, tenseId, tenseType, inflectionId, polite: polite);

                list.Add(tense);
            }
            if (list?.Count > 0)
            {
                return Task.FromResult(list);
            }
            Tense emptyTense = new Tense(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, Guid.Empty, TENSE_TYPE.UNKNOWN, Guid.Empty);
            list = new List<Tense>();
            list.Add(emptyTense);
            return Task.FromResult(list);
        }

        public override Task<DataTable> LoadAllVerbs()
        {
            throw new NotImplementedException();
        }

        public override Task<Inflection> LoadSpecificInflection(Guid inflectionId)
        {
            string sql = $"SELECT * FROM Inflection WHERE InflectionId =  '{inflectionId}'";

            DataTable response = sqlClient.GetData(sql);


            if (response.Rows.Count > 0)
            {
                // get first row and populate fields
                DataRow initial = response.Rows[0];
                Guid verbId = (Guid)initial["VerbId"];
                string name = (string)initial["InflectionName"];

                Inflection inflec = new Inflection(name, inflectionId, verbId);

                return Task.FromResult(inflec);


            }
            Inflection emptyResponse = new Inflection(String.Empty, Guid.Empty, Guid.Empty);

            return Task.FromResult(emptyResponse);
        }

        public override Task<Tense> LoadSpecificTense(Guid tenseId)
        {
            string sql = $"SELECT * FROM Tense WHERE TenseId =  '{tenseId}'";

            DataTable response = sqlClient.GetData(sql);


            if (response.Rows.Count > 0)
            {
                // get first row and populate fields
                DataRow initial = response.Rows[0];
                Guid inflectionId = (Guid)initial["InflectionId"];
                string kanji = (string)initial["Kanji"];
                string hiragana = (string)initial["Hiragana"];
                string romaji = (string)initial["Romaji"];
                string meaning = (string)initial["Meaning"];
                string notes = (string)initial["Notes"];
                bool polite = (bool)initial["Polite"];
                TENSE_TYPE tenseType = (TENSE_TYPE)initial["TenseType"];

                Tense tense = new Tense(kanji, hiragana,romaji,meaning, notes, tenseId, tenseType, inflectionId, polite: polite);

                return Task.FromResult(tense);



            }
            Tense emptyTense = new Tense(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, Guid.Empty, TENSE_TYPE.UNKNOWN, Guid.Empty);
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
                VERB_TYPE verbType = (VERB_TYPE)initial["VerbType"];

                switch (verbType)
                {
                    case VERB_TYPE.ICHIDAN:
                        Verb ichidan = new IchidanVerb(kanji, hiragana, romaji, meaning, verbId);
                        return Task.FromResult(ichidan);
                    case VERB_TYPE.GODAN:
                        Verb godan = new GodanVerb(kanji, hiragana, romaji, meaning, verbId);
                        return Task.FromResult(godan);
                    case VERB_TYPE.EXCEPTION:

                        break;
                }

                
            }
            Verb emptyResponse = new IchidanVerb(String.Empty, String.Empty, String.Empty, String.Empty, Guid.Empty);
            return Task.FromResult(emptyResponse);


        }

        public override Task<bool> SaveInflection(Inflection inflectionToSave)
        {
            var cmd = new SqlCommand("SaveInflection");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = inflectionToSave.Id;
            cmd.Parameters.Add("@VerbId", SqlDbType.UniqueIdentifier).Value = inflectionToSave.VerbId;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 30).Value = inflectionToSave.Name;
           

            string response = String.Empty;

            try
            {
                sqlClient.ExecuteCommand(cmd);
            }
            catch (Exception exc)
            {
                throw new Exception("SaveInflection failed", exc);
            }

            return Task.FromResult(true);
        }

        public override Task<bool> SaveTense(Tense tenseToSave)
        {
            var cmd = new SqlCommand("SaveTense");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = tenseToSave.Id;
            cmd.Parameters.Add("@VerbId", SqlDbType.UniqueIdentifier).Value = tenseToSave.InflectionId;
            cmd.Parameters.Add("@Kanji", SqlDbType.NVarChar, 30).Value = tenseToSave.Kanji;
            cmd.Parameters.Add("@Hiragana", SqlDbType.NVarChar, 30).Value = tenseToSave.Hiragana;
            cmd.Parameters.Add("@Romaji", SqlDbType.NVarChar, 30).Value = tenseToSave.Romaji;
            cmd.Parameters.Add("@Meaning", SqlDbType.NVarChar, 50).Value = tenseToSave.Meaning;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value = tenseToSave.Notes;
            cmd.Parameters.Add("@Polite", SqlDbType.Bit).Value = tenseToSave.Polite;
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
    }
}
