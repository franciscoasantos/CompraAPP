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
        private readonly ICartaoService _cartaoService;

        public PedidoService(IPedidoRepository pedidoRepository, ICartaoService cartaoService)
        {
            _pedidoRepository = pedidoRepository;
            _cartaoService = cartaoService;
        }

        public async Task<PedidoResponse> ProcessarPedido(string mensagem)
        {
            try
            {
                Pedido pedido = JsonConvert.DeserializeObject<Pedido>(mensagem);
                await _pedidoRepository.CriarPedido(pedido);

                if (pedido.SalvarCartao)
                {
                    Cartao cartao = new()
                    {
                        IdUsuario = pedido.IdUsuario,
                        Numero = pedido.Cartao.Numero,
                        Vencimento = pedido.Cartao.Vencimento,
                        CodigoSeguranca = pedido.Cartao.CodigoSeguranca
                    };
                    await _cartaoService.IncluirCartao(cartao);
                }

                return new PedidoResponse { Sucesso = true };
            }
            catch (Exception ex)
            {
                throw new PedidoException(ex.Message);
            }
        }
    }
}
