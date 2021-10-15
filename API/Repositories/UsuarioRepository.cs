using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Services;
using Dapper;

namespace API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbSession _sessao;

        public UsuarioRepository(DbSession sessao)
        {
            _sessao = sessao;
        }

        public async Task<CadastroResponse> Cadastrar(Cadastro cadastro)
        {
            return await _sessao.Connection.QueryFirstAsync<CadastroResponse>("select 1 Id");
        }

        public async Task<LoginResponse> Login(Login login)
        {
            return await _sessao.Connection.QueryFirstAsync<LoginResponse>("select 'true' Logado");
        }
    }
}
