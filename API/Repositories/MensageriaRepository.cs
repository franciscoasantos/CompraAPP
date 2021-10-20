using System.Threading.Tasks;
using API.Dominio.Exceptions;
using API.Dominio.Model;
using API.Dominio.Repositories;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace API.Repositories
{
    public class MensageriaRepository : IMensageriaRepository
    {
        public async Task<DeliveryResult<Null, string>> EnviarPedido(PedidoMensagem pedido, string kafkaHost, string kafkaTopic)
        {
            var config = new ProducerConfig { BootstrapServers = kafkaHost };

            using var p = new ProducerBuilder<Null, string>(config).Build();

            try
            {
                var dr = await p.ProduceAsync(kafkaTopic, new Message<Null, string> { Value = JsonConvert.SerializeObject(pedido) });
                return dr;
            }
            catch (ProduceException<Null, string> e)
            {
                throw new MensageriaException(e.Error.Reason, e.InnerException);
            }
        }
    }
}
