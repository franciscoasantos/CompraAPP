using System;
using System.Threading.Tasks;
using API.Dominio.Exceptions;
using API.Dominio.Model;
using API.Dominio.Services;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace API.Services
{
    public class MensageriaService : IMensageriaService
    {
        private readonly IConfiguration _config;
        private string KafkaHost;
        private string KafkaTopic;
        public MensageriaService(IConfiguration config)
        {
            _config = config;
            RecuperarConfig();
        }

        private void RecuperarConfig()
        {
            KafkaHost = _config.GetSection("Configuracoes:kafkaHost").Value;
            KafkaTopic = _config.GetSection("Configuracoes:kafkaTopic").Value;
        }

        public async Task<DeliveryResult<Null, string>> ProcessarPedido(Pedido pedido)
        {
            var config = new ProducerConfig { BootstrapServers = KafkaHost };

            using var p = new ProducerBuilder<Null, string>(config).Build();

            try
            {
                var dr = await p.ProduceAsync(KafkaTopic, new Message<Null, string> { Value = JsonConvert.SerializeObject(pedido) });
                return dr;
            }
            catch (ProduceException<Null, string> e)
            {
                throw new MensageriaException(e.Error.Reason);
            }
        }
    }
}
