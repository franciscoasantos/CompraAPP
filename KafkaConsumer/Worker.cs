using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using KafkaConsumer.Dominio.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KafkaConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly IPedidoService _pedidoService;

        private string KafkaHost;
        private string KafkaTopic;

        public Worker(ILogger<Worker> logger, IConfiguration config, IPedidoService pedidoService)
        {
            _logger = logger;
            _config = config;
            _pedidoService = pedidoService;
            RecuperarConfig();
        }

        private void RecuperarConfig()
        {
            KafkaHost = _config.GetSection("Configuracoes:kafkaHost").Value;
            KafkaTopic = _config.GetSection("Configuracoes:kafkaTopic").Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = KafkaHost,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(conf).Build();

            consumer.Subscribe(KafkaTopic);

            var cancellationToken = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cancellationToken.Cancel();
            };

            try
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumerResult = consumer.Consume(cancellationToken.Token);

                        await _pedidoService.ProcessarPedido(consumerResult.Message.Value);

                        consumer.Commit(consumerResult);
                        
                        Console.WriteLine($"Consumed message '{consumerResult.Message.Value}' at: '{consumerResult.TopicPartitionOffset}'.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}
