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
        private readonly IPagamentoService _pagamentoService;

        public PedidoService(IPedidoRepository pedidoRepository, ICartaoService cartaoService, IPagamentoService pagamentoService)
        {
            _pedidoRepository = pedidoRepository;
            _cartaoService = cartaoService;
            _pagamentoService = pagamentoService;
        }

        public async Task<PedidoResponse> ProcessarPedido(string mensagem)
        {
            try
            {
                Pedido pedido = JsonConvert.DeserializeObject<Pedido>(mensagem);
                var idPedido = await _pedidoRepository.CriarPedido(pedido);

                Cartao cartao = new()
                {
                    IdUsuario = pedido.IdUsuario,
                    Numero = pedido.Cartao.Numero,
                    Vencimento = pedido.Cartao.Vencimento,
                    CodigoSeguranca = pedido.Cartao.CodigoSeguranca
                };

                if (pedido.SalvarCartao)
                    await _cartaoService.IncluirCartao(cartao);

                _pagamentoService.ProcessarPagamentoCartao(idPedido, cartao);

                return new PedidoResponse { Sucesso = true };
            }
            catch (Exception ex)
            {
                throw new PedidoException(ex.Message, ex.InnerException);
            }
        }
    }
}
