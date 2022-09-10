using LanguageConsult.Verbs;
using LanguageConsult.Verbs.InflectionControl;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LanguageConsult.DataAccess.MSSqlDataAccess
{
    public class MsSqlDataAccess : DataAccess
    {
        private SqlDataAccess sqlClient = new SqlDataAccess();

        private Inflection emptyInflection = new StandardCasual("A", Guid.Empty, Guid.Empty);
        private Tense emptyTense = new Tense( "書", "あ", "A", "A", "A", Guid.Empty, TENSE_TYPE.PAST_POSITIVE, Guid.Empty);
        private Verb emptyVerb = new IchidanVerb("書", "あ", "A", "A", Guid.Empty, "書", false);
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
                string inflectionClass = (string)initial["InflectionClass"];

                Inflection inflection = GetInflectionFromClass(verbId, inflectionId, name, inflectionClass);
                if(inflection.Id != Guid.Empty)
                {
                    list.Add(inflection);
                }
            }
            if (list?.Count > 0)
            {
                return Task.FromResult(list);
            }
            list = new List<Inflection>();
            list.Add(emptyInflection);
            return Task.FromResult(list);
        }
        private Inflection GetInflectionFromClass(Guid verbId, Guid inflectionId, string nme, string className)
        {

            switch (className)
            {
                case "StandardCasual":
                    return new StandardCasual(nme, inflectionId, verbId);
            }

            return emptyInflection;
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
                string inflectionClass = (string)initial["InflectionClass"];

                Inflection inflection = GetInflectionFromClass(verbId, inflectionId, name, inflectionClass);

                return Task.FromResult(inflection);


            }
            

            return Task.FromResult(emptyInflection);
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

                switch (verbType)
                {
                    case VERB_TYPE.ICHIDAN:
                        Verb ichidan = new IchidanVerb(kanji, hiragana, romaji, meaning, verbId, kanjiCharacter, verbCurrent);
                        return Task.FromResult(ichidan);
                    case VERB_TYPE.GODAN:
                        Verb godan = new GodanVerb(kanji, hiragana, romaji, meaning, verbId, kanjiCharacter, verbCurrent);
                        return Task.FromResult(godan);
                    case VERB_TYPE.EXCEPTION:

                        break;
                }

                
            }
            
            return Task.FromResult(emptyVerb);


        }

        public override Task<bool> SaveInflection(Inflection inflectionToSave)
        {
            string className = inflectionToSave.GetType().Name;
            var cmd = new SqlCommand("SaveInflection");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = inflectionToSave.Id;
            cmd.Parameters.Add("@VerbId", SqlDbType.UniqueIdentifier).Value = inflectionToSave.VerbId;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 30).Value = inflectionToSave.Name;
            cmd.Parameters.Add("@InflectionClass", SqlDbType.NVarChar, 30).Value = className;
           

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
            cmd.Parameters.Add("@InflectionId", SqlDbType.UniqueIdentifier).Value = tenseToSave.InflectionId;
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

            foreach (Inflection inf in verbToDelete.inflections)
            {
                sql = $"DELETE Inflection WHERE InflectionId =  '{inf.Id}'";

                sqlClient.ExecuteNonQuery(sql);

                sql = $"DELETE Tense WHERE InflectionId =  '{inf.Id}'";

                sqlClient.ExecuteNonQuery(sql);
            }
            


            return Task.FromResult(true);
        }

        public override Task<bool> DeleteInflection(Inflection inflectionToDelete)
        {
            string sql = $"DELETE Inflection WHERE InflectionId =  '{inflectionToDelete.Id}'";

            sqlClient.ExecuteNonQuery(sql);

            sql = $"DELETE Tense WHERE InflectionId =  '{inflectionToDelete.Id}'";

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
