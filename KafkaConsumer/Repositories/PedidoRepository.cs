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
            string query = @"INSERT INTO pedidos
                             OUTPUT inserted.id 
                             VALUES (           
                                  @IdUsuario    
                                 ,@IdAplicativo 
                                 ,'P')";

            var parameter = new DynamicParameters(pedido);

            return await _sessao.Connection.QueryFirstAsync<long>(query, parameter, _sessao.Transaction);
        }

        public async Task<int> AtualizarStatusPedido(long idPedido, string statusPedido)
        {
            string query = @"UPDATE pedidos      
                             SET status = @status
                             WHERE id = @id";

            var parameter = new DynamicParameters(new { status = statusPedido, id = idPedido });

            return await _sessao.Connection.ExecuteAsync(query, parameter);
        }
    }
}
