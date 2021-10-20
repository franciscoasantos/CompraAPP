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

        public async Task<CadastroResponse> CadastrarUsuario(Cadastro cadastro)
        {
            StringBuilder sb = new();

            sb.AppendLine(" INSERT INTO users                   ");
            sb.AppendLine(" OUTPUT inserted.id IdUsuario        ");
            sb.AppendLine(" VALUES (                            ");
            sb.AppendLine("     @Nome                           ");
            sb.AppendLine("     ,@Cpf                           ");
            sb.AppendLine("     ,@DataNascimento                ");
            sb.AppendLine("     ,@Sexo                          ");
            sb.AppendLine("     ,@Endereco)                     ");

            var template = new { cadastro.Nome, cadastro.Cpf, cadastro.DataNascimento, cadastro.Sexo, Endereco = cadastro.Endereco.ToString() };
            var parameters = new DynamicParameters(template);

            return await _sessao.Connection.QueryFirstAsync<CadastroResponse>(sb.ToString(), parameters);
        }

        public async Task<int> CadastrarSenha(long idUsuario, string senha)
        {
            StringBuilder sb = new();

            sb.AppendLine(" INSERT INTO senhas ");
            sb.AppendLine(" VALUES (           ");
            sb.AppendLine("     @idUsuario     ");
            sb.AppendLine("     ,@senha)       ");

            var template = new { idUsuario, senha };
            var parameters = new DynamicParameters(template);

            return await _sessao.Connection.ExecuteAsync(sb.ToString(), parameters);
        }

        public async Task<int> ValidarUsuarioExistente(string cpf)
        {
            StringBuilder sb = new();

            sb.AppendLine(" SELECT COUNT(*) qtd ");
            sb.AppendLine(" FROM users          ");
            sb.AppendLine(" WHERE cpf = @Cpf    ");

            var template = new { Cpf = cpf };
            var parameters = new DynamicParameters(template);

            return await _sessao.Connection.QueryFirstAsync<int>(sb.ToString(), parameters);
        }

        public async Task<long> BuscarIdUsuario(string cpf)
        {
            StringBuilder sb = new();

            sb.AppendLine(" SELECT id idUsuario ");
            sb.AppendLine(" FROM users          ");
            sb.AppendLine(" WHERE cpf = @Cpf    ");

            var template = new { Cpf = cpf };
            var parameters = new DynamicParameters(template);

            return await _sessao.Connection.QueryFirstAsync<long>(sb.ToString(), parameters);
        }
    }
}
