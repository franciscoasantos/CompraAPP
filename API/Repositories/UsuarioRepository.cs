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
            string query = @"INSERT INTO users            
                             OUTPUT inserted.id IdUsuario 
                             VALUES (                     
                                 @Nome                    
                                 ,@Cpf                    
                                 ,@DataNascimento         
                                 ,@Sexo                   
                                 ,@Endereco)";

            var template = new { cadastro.Nome, cadastro.Cpf, cadastro.DataNascimento, cadastro.Sexo, Endereco = cadastro.Endereco.ToString() };
            var parameters = new DynamicParameters(template);

            return await _sessao.Connection.QueryFirstAsync<CadastroResponse>(query, parameters, _sessao.Transaction);
        }

        public async Task<int> CadastrarSenha(long idUsuario, string senha)
        {
            string query = @"INSERT INTO senhas
                             VALUES (          
                                 @idUsuario    
                                 ,@senha)";

            var template = new { idUsuario, senha };
            var parameters = new DynamicParameters(template);

            var retorno =  await _sessao.Connection.ExecuteAsync(query, parameters, _sessao.Transaction);
            return retorno;
        }

        public async Task<int> ExisteUsuarioCadastrado(string cpf)
        {
            string query = @"SELECT COUNT(*) qtd
                             FROM users         
                             WHERE cpf = @Cpf";

            var template = new { Cpf = cpf };
            var parameters = new DynamicParameters(template);

            return await _sessao.Connection.QueryFirstAsync<int>(query, parameters);
        }

        public async Task<long> BuscarIdUsuario(string cpf)
        {
            string query = @"SELECT id idUsuario
                             FROM users         
                             WHERE cpf = @Cpf";

            var template = new { Cpf = cpf };
            var parameters = new DynamicParameters(template);

            return await _sessao.Connection.QueryFirstAsync<long>(query, parameters);
        }
    }
}
