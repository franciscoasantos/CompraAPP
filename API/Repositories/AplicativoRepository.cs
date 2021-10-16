using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Services;
using Dapper;

namespace API.Repositories
{
    public class AplicativoRepository : IAplicativoRepository
    {
        public readonly DbSession _sessao;

        public AplicativoRepository(DbSession sessao)
        {
            _sessao = sessao;
        }

        public async Task<IEnumerable<Aplicativo>> Get()
        {
            StringBuilder sb = new();

            sb.AppendLine(" SELECT id Id     ");
            sb.AppendLine("     ,nome Nome   ");
            sb.AppendLine("     ,preco Preco ");
            sb.AppendLine(" FROM aplicativos ");

            return await _sessao.Connection.QueryAsync<Aplicativo>(sb.ToString());
        }
    }
}
