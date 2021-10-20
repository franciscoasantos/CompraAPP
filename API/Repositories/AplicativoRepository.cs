using System.Collections.Generic;
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
            string query = @"SELECT id Id    
                                 ,nome Nome  
                                 ,preco Preco
                             FROM aplicativos";

            return await _sessao.Connection.QueryAsync<Aplicativo>(query);
        }
    }
}
