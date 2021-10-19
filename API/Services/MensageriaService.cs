using System;
using System.Threading.Tasks;
using API.Dominio.Exceptions;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Dominio.Services;
using Microsoft.Extensions.Configuration;

namespace API.Services
{
    public class MensageriaService : IMensageriaService
    {
        private readonly IConfiguration _config;
        private readonly IMensageriaRepository _mensageriaRepository;

        public MensageriaService(IConfiguration config, IMensageriaRepository mensageriaRepository)
        {
            _config = config;
            _mensageriaRepository = mensageriaRepository;
        }

        public async Task<bool> ProcessarPedido(Pedido pedido)
        {
            try
            {
                var kafkaHost = _config.GetSection("Configuracoes:kafkaHost").Value;
                var kafkaTopic = _config.GetSection("Configuracoes:kafkaTopic").Value;

                if (kafkaHost == null || kafkaTopic == null)
                    throw new MensageriaException("As keys kafkaHost e/ou kafkaTopic não foram configuradas corretamente no appsettings.json.");

                await _mensageriaRepository.EnviarPedido(pedido, kafkaHost, kafkaTopic);

                return true;
            }
            catch (Exception ex)
            {
                throw new MensageriaException(ex.Message, ex.InnerException);
            }
        }
    }
}
