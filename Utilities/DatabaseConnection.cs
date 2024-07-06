using System.Data.SqlClient;

namespace PhoneBook
{
    public class DatabaseConnection
    {
        private static SqlConnection _connection;
        public static string connectionString = "";

        private static readonly object _lockObject = new object();

        private DatabaseConnection() { }

        public static SqlConnection Connect(string connectionString)
        {
            lock (_lockObject)
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(connectionString);
                    _connection.Open();
                }

                return _connection;
            }
        }

        public static void Disconnect()
        {
            lock (_lockObject)
            {
                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
    }
}
