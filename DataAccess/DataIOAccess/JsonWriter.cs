using LanguageConsult.Verbs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.DataAccess.DataIOAccess
{
    internal class JsonWriter
    {
        internal void WriteVerb(string folderPath, Verb verbToWrite, bool append = false) 
        {
            string fileName = folderPath + Path.DirectorySeparatorChar + verbToWrite.Romaji + ".json";
            // switch contents to write to Json object
            var contentsToWrite = JsonConvert.SerializeObject(verbToWrite);
            using (TextWriter writer = new StreamWriter(fileName, append))
            {
                writer.Write(contentsToWrite);
            }

            
        }
    }
}
