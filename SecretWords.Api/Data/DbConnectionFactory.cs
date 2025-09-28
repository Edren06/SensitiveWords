using Microsoft.Data.SqlClient;
using SensitiveWords.Api.Interfaces;
using System.Data;

namespace SensitiveWords.Api.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;
        public DbConnectionFactory(IConfiguration config) { _config = config; }
        public IDbConnection CreateConnection()
        {
            var cs = _config.GetConnectionString("DefaultConnection");
            return new SqlConnection(cs);
        }
    }
}
