using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace API.Services
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration.GetSection("Configuracoes:stringConexao").Value);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
