using System.Threading.Tasks;
using KafkaConsumer.Dominio.Model;

namespace KafkaConsumer.Dominio.Repositories
{
    public interface IPedidoRepository
    {
        public Task<long> CriarPedido(Pedido pedido);
        public Task<int> AtualizarStatusPedido(long idPedido, string statusPedido);
    }
}
