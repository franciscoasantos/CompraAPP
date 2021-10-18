using System.Threading.Tasks;
using KafkaConsumer.Dominio.Model;
using KafkaConsumer.Dominio.Repositories;

namespace KafkaConsumer.Repositories
{
    class PedidoRepository : IPedidoRepository
    {
        public Task<int> CriarPedido(Pedido pedido)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> IncluirCartao(Cartao cartao)
        {
            throw new System.NotImplementedException();
        }
    }
}
