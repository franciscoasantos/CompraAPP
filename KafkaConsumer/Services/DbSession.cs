using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace KafkaConsumer.Services
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IConfiguration configuration)
        {
            var DbServer = Environment.GetEnvironmentVariable("DbServer");
            var DbUser = Environment.GetEnvironmentVariable("DbUser");
            var Password = Environment.GetEnvironmentVariable("Password");
            var Database = Environment.GetEnvironmentVariable("Database");

            if (DbServer == null)
                Connection = new SqlConnection(configuration.GetSection("Configuracoes:stringConexao").Value);
            else
                Connection = new SqlConnection($"Server={DbServer};Database={Database};User Id={DbUser};Password={Password};");

            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
