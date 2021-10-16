using System.Text;
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
            StringBuilder sb = new();

            sb.AppendLine(" INSERT INTO users                   ");
            sb.AppendLine(" OUTPUT inserted.codigoUsuario Id    ");
            sb.AppendLine(" VALUES (                            ");
            sb.AppendLine("     @Nome                           ");
            sb.AppendLine("     ,@Cpf                           ");
            sb.AppendLine("     ,@DataNascimento                ");
            sb.AppendLine("     ,@Sexo                          ");
            sb.AppendLine("     ,@Endereco                      ");
            sb.AppendLine("     ,@Senha)                        ");

            var template = new { cadastro.Nome, cadastro.Cpf, cadastro.DataNascimento, cadastro.Sexo, Endereco = cadastro.Endereco.ToString(), cadastro.Senha };

            var parameters = new DynamicParameters(template);

            return await _sessao.Connection.QueryFirstAsync<CadastroResponse>(sb.ToString(), parameters);
        }

        public async Task<LoginResponse> Login(Login login)
        {
            return await _sessao.Connection.QueryFirstAsync<LoginResponse>("select 'true' Logado");
        }
    }
}
