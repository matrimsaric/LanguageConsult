using LanguageConsult.Verbs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.DataAccess.DataIOAccess
{
    public class DataIoAccess : DataAccess
    {
        private JsonWriter writer = new JsonWriter();

        private string parentFolder = String.Empty;
        private string verbsFolder = String.Empty;
        private string indexesFolder = String.Empty;
        private string tensesFolder = String.Empty;

        private const string PARENT_NAME = "DataIo";
        private const string VERBS_NAME = "Verbs";
        private const string TENSES_NAME = "Tenses";
        private const string INDEXES_NAME = "Indexes";
        private const string INDEX_FILE_NAME = "AllVerbs.json";
        public DataIoAccess()
        {
            ValidateIoStructure();
        }
        /// <summary>
        /// If using file system ensure that our requested folder structure exists
        /// </summary>
        private void ValidateIoStructure()
        {
            string? operatingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // code needs a parent folder
            parentFolder = operatingDirectory + Path.DirectorySeparatorChar + PARENT_NAME;

            if (!Directory.Exists(parentFolder))
            {
                Directory.CreateDirectory(parentFolder);
            }

            // and a verbs folder 
            verbsFolder = parentFolder + Path.DirectorySeparatorChar + VERBS_NAME;

            if (!Directory.Exists(verbsFolder))
            {
                Directory.CreateDirectory(verbsFolder);
            }

            // and an indexing folder
            indexesFolder = parentFolder + Path.DirectorySeparatorChar + INDEXES_NAME;

            if (!Directory.Exists(indexesFolder))
            {
                Directory.CreateDirectory(indexesFolder);
            }

            // and a Tenses folder
            tensesFolder = parentFolder + Path.DirectorySeparatorChar + TENSES_NAME;

            if (!Directory.Exists(tensesFolder))
            {
                Directory.CreateDirectory(tensesFolder);
            }

        }
        public override Task<bool> DeleteTense(Tense tenseToDelete)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DeleteVerb(Verb verbToDelete)
        {
            throw new NotImplementedException();
        }

        public override Task<List<Tense>> LoadAllTensesForInflection(Guid inflectionId)
        {
            throw new NotImplementedException();
        }

        public override Task<DataTable> LoadAllVerbs()
        {
            // need to see if any records exist
            string fileName = indexesFolder + Path.DirectorySeparatorChar + INDEX_FILE_NAME;
            DataTable response = new DataTable();

            if(!File.Exists(fileName))
            {
                // no index file so return empty DataTable
                return Task.FromResult(response);
            }
            else
            {
                // deserialize file
            }
            

          

            return Task.FromResult(response);
        }

        public override Task<DataTable> LoadFilteredVerbs(int verbType, int searchField, string searchValue)
        {
            throw new NotImplementedException();
        }

        public override Task<Tense> LoadSpecificTense(Guid tenseId)
        {
            throw new NotImplementedException();
        }

        public override Task<Verb> LoadSpecificVerb(Guid verbId)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> SaveTense(Tense tenseToSave)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> SaveVerb(Verb verbToSave)
        {
            // TODO need to Write the verb AND either add to the index file or adjust - needs some thought as to best way
            writer.WriteVerb(verbsFolder, verbToSave, false);

           

            return Task.FromResult(true);
        }
    }
}
