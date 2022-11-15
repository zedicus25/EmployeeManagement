using System.Configuration;
using System.Data.SqlClient;


namespace Server.Utilities
{
    internal class SqlConnectionSingleton
    {
        public static SqlConnection GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if(_instance == null)
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
                        _instance = new SqlConnection(_connectionString);
                        _instance.Open();
                    }
                }
            }
            if (_instance.State != System.Data.ConnectionState.Open)
                _instance.Open();
            return _instance;
        }
        private static SqlConnection _instance;
        private static string _connectionString;
        private static readonly object _lock = new object();

        private SqlConnectionSingleton()
        {
        }
    }
}
