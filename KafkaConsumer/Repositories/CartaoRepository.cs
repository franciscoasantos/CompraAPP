using System.Threading.Tasks;
using Dapper;
using KafkaConsumer.Dominio.Model;
using KafkaConsumer.Dominio.Repositories;
using KafkaConsumer.Services;

namespace KafkaConsumer.Repositories
{
    class CartaoRepository : ICartaoRepository
    {
        private readonly DbSession _sessao;

        public CartaoRepository(DbSession sessao)
        {
            _sessao = sessao;
        }
        public async Task<int> IncluirCartao(Cartao cartao)
        {
            string query = @"INSERT INTO cartoes   
                             VALUES (              
                                  @IdUsuario       
                                 ,@Numero          
                                 ,@Vencimento      
                                 ,@CodigoSeguranca)";

            var parameter = new DynamicParameters(cartao);

            return await _sessao.Connection.ExecuteAsync(query, parameter, _sessao.Transaction);
        }

        public async Task<int> ExisteCartaoCadastrado(Cartao cartao)
        {
            string query = @"SELECT COUNT(*)             
                             FROM cartoes                
                             WHERE IdUsuario = @IdUsuario
                                 AND numero = @Numero";

            var parameters = new DynamicParameters(cartao);

            return await _sessao.Connection.QueryFirstAsync<int>(query, parameters, _sessao.Transaction);
        }
    }
}
