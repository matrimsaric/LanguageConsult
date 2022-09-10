using LanguageConsult.DataAccess.MSSqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageConsult.DataAccess
{
    public  class DataAccessProvider
    {
        public DataAccess GetLiveDataAccess()
        {
            return new MsSqlDataAccess();
        }
    }
}
