using System.Threading.Tasks;
using API.Dominio.Model;
using Confluent.Kafka;

namespace API.Dominio.Repositories
{
    public interface IMensageriaRepository
    {
        public Task<DeliveryResult<Null, string>> EnviarPedido(Pedido pedido, string kafkaHost, string kafkaTopic);
    }
}
