using System.Text;
using System.Threading.Tasks;
using KafkaConsumer.Dominio.Model;
using KafkaConsumer.Dominio.Repositories;
using KafkaConsumer.Services;
using Dapper;

namespace KafkaConsumer.Repositories
{
    class PedidoRepository : IPedidoRepository
    {
        private readonly DbSession _sessao;

        public PedidoRepository(DbSession sessao)
        {
            _sessao = sessao;
        }

        public async Task<long> CriarPedido(Pedido pedido)
        {
            StringBuilder sb = new();

            sb.AppendLine(" INSERT INTO pedidos ");
            sb.AppendLine(" OUTPUT inserted.id  ");
            sb.AppendLine(" VALUES (            ");
            sb.AppendLine("      @IdUsuario     ");
            sb.AppendLine("     ,@IdAplicativo  ");
            sb.AppendLine("     ,'P')           ");

            var parameter = new DynamicParameters(pedido);

            return await _sessao.Connection.QueryFirstAsync<long>(sb.ToString(), parameter, _sessao.Transaction);
        }

        public async Task<int> AtualizarStatusPedido(long idPedido, string statusPedido)
        {
            StringBuilder sb = new();

            sb.AppendLine(" UPDATE pedidos       ");
            sb.AppendLine(" SET status = @status ");
            sb.AppendLine(" WHERE id = @id       ");

            var parameter = new DynamicParameters(new { status = statusPedido, id = idPedido });

            return await _sessao.Connection.ExecuteAsync(sb.ToString(), parameter);
        }
    }
}
