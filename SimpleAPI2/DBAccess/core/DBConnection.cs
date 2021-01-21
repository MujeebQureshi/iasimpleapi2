using SimpleAPI2.Utility.Constants;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Configuration;
using System.Data.SqlClient;

namespace SimpleAPI2.DBAccess.core
{
    public static class DBConnection
    {
        //overrides can be created here if needed
        public static QueryFactory GetDBConnection()
        {
            var connection = new SqlConnection(ConfigurationManager.AppSettings[AppConstants.DBConnection].ToString());
            var compiler = new SqlServerCompiler();
            return new QueryFactory(connection, compiler);
        }
    }
}