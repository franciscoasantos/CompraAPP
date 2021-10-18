using System.Threading.Tasks;
using KafkaConsumer.Dominio.Model;

namespace KafkaConsumer.Dominio.Repositories
{
    public interface IPedidoRepository
    {
        public Task<int> CriarPedido(Pedido pedido);
        public Task<int> IncluirCartao(Cartao cartao);
    }
}
