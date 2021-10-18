using System;
using System.Threading.Tasks;
using KafkaConsumer.Dominio.Exceptions;
using KafkaConsumer.Dominio.Model;
using KafkaConsumer.Dominio.Repositories;
using KafkaConsumer.Dominio.Services;
using Newtonsoft.Json;

namespace KafkaConsumer.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoResponse> ProcessarPedido(string mensagem)
        {
            try
            {
                Pedido pedido = JsonConvert.DeserializeObject<Pedido>(mensagem);
                await _pedidoRepository.CriarPedido(pedido);

                if (pedido.SalvarCartao)
                    await _pedidoRepository.IncluirCartao(pedido.Cartao);

                return new PedidoResponse { Sucesso = true };
            }
            catch (Exception ex)
            {
                throw new PedidoException(ex.Message);
            }
        }
    }
}
