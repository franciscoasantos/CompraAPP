using System.Text;
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
            StringBuilder sb = new();

            sb.AppendLine(" INSERT INTO cartoes    ");
            sb.AppendLine(" VALUES (               ");
            sb.AppendLine("      @IdUsuario        ");
            sb.AppendLine("     ,@Numero           ");
            sb.AppendLine("     ,@Vencimento       ");
            sb.AppendLine("     ,@CodigoSeguranca) ");

            var parameter = new DynamicParameters(cartao);

            return await _sessao.Connection.ExecuteAsync(sb.ToString(), parameter, _sessao.Transaction);
        }

        public async Task<int> ValidarCartaoExistente(Cartao cartao)
        {
            StringBuilder sb = new();

            sb.AppendLine(" SELECT COUNT(*)              ");
            sb.AppendLine(" FROM cartoes                 ");
            sb.AppendLine(" WHERE IdUsuario = @IdUsuario ");
            sb.AppendLine("     AND numero = @Numero     ");

            var parameters = new DynamicParameters(cartao);

            return await _sessao.Connection.QueryFirstAsync<int>(sb.ToString(), parameters, _sessao.Transaction);
        }
    }
}
