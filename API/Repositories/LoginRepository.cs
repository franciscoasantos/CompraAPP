using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Services;
using Dapper;

namespace API.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DbSession _sessao;
        public LoginRepository(DbSession sessao)
        {
            _sessao = sessao;
        }
        public async Task<IEnumerable<Login>> BuscarDadosLogin(Login login)
        {
            string query = @"SELECT cpf Cpf                                  
                                 ,senha Senha                                
                             FROM users                                      
                             INNER JOIN senhas ON users.id = senhas.idUsuario
                             WHERE cpf = @Cpf";

            var template = new { Cpf = login.Usuario };
            var parameters = new DynamicParameters(template);

            return await _sessao.Connection.QueryAsync<Login>(query, parameters);
        }
    }
}
