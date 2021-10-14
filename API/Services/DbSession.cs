using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace API.Services
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession()
        {
            Connection = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
