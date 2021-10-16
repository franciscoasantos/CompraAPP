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
        public async Task<Login> BuscarDadosLogin(Login login)
        {
            StringBuilder sb = new();

            sb.AppendLine(" SELECT cpf Cpf                                          ");
            sb.AppendLine("     ,senha Senha                                        ");
            sb.AppendLine(" FROM users                                              ");
            sb.AppendLine(" INNER JOIN senhas ON users.idUsuario = senhas.idUsuario ");
            sb.AppendLine(" WHERE cpf = @Cpf                                        ");

            var template = new { Cpf = login.Usuario };
            var parameters = new DynamicParameters(template);

            return await _sessao.Connection.QueryFirstAsync<Login>(sb.ToString(), parameters);
        }
    }
}
