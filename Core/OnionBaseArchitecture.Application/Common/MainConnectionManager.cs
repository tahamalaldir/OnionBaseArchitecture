using OnionBaseArchitecture.Application.Abstractions;
using System.Data;
using System.Data.SqlClient;

namespace OnionBaseArchitecture.Application.Common
{
    public class MainConnectionManager : IConnectionManager
    {
        private readonly ConnectionConfigs _config;

        public MainConnectionManager(ConnectionConfigs connections)
        {
            _config = connections;
        }

        public IDbConnection Connection
        {
            get
            {
                var conn = SqlClientFactory.Instance.CreateConnection();
                conn.ConnectionString = _config.SqlConnection;
                conn.Open();
                return conn;
            }
        }
    }
}
