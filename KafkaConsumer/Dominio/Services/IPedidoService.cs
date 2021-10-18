using System.Threading.Tasks;
using KafkaConsumer.Dominio.Model;

namespace KafkaConsumer.Dominio.Services
{
    public interface IPedidoService
    {
        public Task<PedidoResponse> ProcessarPedido(string mensagem);
    }
}
