using KafkaConsumer.Dominio.Repositories;
using KafkaConsumer.Dominio.Services;
using KafkaConsumer.Repositories;
using KafkaConsumer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace KafkaConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/consumer-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddSerilog();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<DbSession>();
                    services.AddSingleton<IUnitOfWork, UnitOfWork>();
                    services.AddSingleton<IPedidoRepository, PedidoRepository>();
                    services.AddSingleton<IPedidoService, PedidoService>();
                    services.AddSingleton<ICartaoRepository, CartaoRepository>();
                    services.AddSingleton<ICartaoService, CartaoService>();
                    services.AddSingleton<IPagamentoService, PagamentoService>();
                });
    }
}
