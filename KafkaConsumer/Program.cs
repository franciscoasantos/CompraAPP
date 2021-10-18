using KafkaConsumer.Dominio.Repositories;
using KafkaConsumer.Dominio.Services;
using KafkaConsumer.Repositories;
using KafkaConsumer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KafkaConsumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<DbSession>();
                    services.AddSingleton<IUnitOfWork, UnitOfWork>();
                    //services.AddSingleton<ICriptografiaService, CriptografiaService>();
                    services.AddSingleton<IPedidoRepository, PedidoRepository>();
                    services.AddSingleton<IPedidoService, PedidoService>();
                    services.AddSingleton<ICartaoRepository, CartaoRepository>();
                    services.AddSingleton<ICartaoService, CartaoService>();
                });
    }
}
