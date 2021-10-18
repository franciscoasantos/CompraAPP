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
        public Task<int> CriarPedido(Pedido pedido)
        {
            StringBuilder sb = new();

            sb.AppendLine(" INSERT INTO pedidos ");
            sb.AppendLine(" VALUES (            ");
            sb.AppendLine("      @IdUsuario     ");
            sb.AppendLine("     ,@IdAplicativo  ");
            sb.AppendLine("     ,'P')            ");

            var parameter = new DynamicParameters(pedido);

            return _sessao.Connection.ExecuteAsync(sb.ToString(), parameter);
        }
    }
}
