using System.Threading.Tasks;
using API.Dominio.Model;
using Confluent.Kafka;

namespace API.Dominio.Services
{
    public interface IMensageriaService
    {
        public Task<bool> ProcessarPedido(Pedido pedido);
    }
}
