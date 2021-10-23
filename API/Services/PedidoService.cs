using System;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Dominio.Services;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly ILogger<PedidoService> _logger;
        private readonly IMensageriaService _mensageriaService;
        private readonly ICartaoService _cartaoService;
        private readonly IUsuarioRepository _usuarioRepository;

        public PedidoService(ILogger<PedidoService> logger, IMensageriaService mensageriaService, ICartaoService cartaoService, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _mensageriaService = mensageriaService;
            _cartaoService = cartaoService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<PedidoResponse> CriarPedido(string user, Pedido pedido)
        {
            try
            {
                if (!_cartaoService.CartaoEhValido(pedido.Cartao))
                    return new PedidoResponse { Sucesso = false, Detalhe = "O cartão informado não é válido." };

                var idUsuario = await _usuarioRepository.BuscarIdUsuario(user);

                PedidoMensagem pedidoMensagem = new()
                {
                    IdUsuario = idUsuario,
                    IdAplicativo = pedido.IdAplicativo,
                    SalvarCartao = pedido.SalvarCartao,
                    Cartao = _cartaoService.CriptografarDadosCartao(pedido.Cartao)
                };

                await _mensageriaService.ProcessarPedido(pedidoMensagem);
                return new PedidoResponse { Sucesso = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new PedidoResponse
                {
                    Sucesso = false,
                    Detalhe = ex.Message
                };
            }
        }
    }
}
