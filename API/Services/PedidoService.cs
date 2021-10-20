using System;
using System.Threading.Tasks;
using API.Dominio.Exceptions;
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
        private readonly ICriptografiaService _criptografiaService;
        private readonly IUsuarioRepository _usuarioRepository;

        public PedidoService(ILogger<PedidoService> logger, IMensageriaService mensageriaService, ICriptografiaService criptografiaService, IUsuarioRepository usuarioRepository)
        {
            _logger = logger;
            _mensageriaService = mensageriaService;
            _criptografiaService = criptografiaService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<PedidoResponse> CriarPedido(string user, Pedido pedido)
        {
            try
            {
                if (!await CartaoEhValido(pedido.Cartao))
                    return new PedidoResponse { Sucesso = false, Detalhe = "O cartão informado não é válido." };

                var idUsuario = await _usuarioRepository.BuscarIdUsuario(user);

                pedido.Cartao.Numero = _criptografiaService.Criptografar(pedido.Cartao.Numero);
                pedido.Cartao.Vencimento = _criptografiaService.Criptografar(pedido.Cartao.Vencimento);
                pedido.Cartao.CodigoSeguranca = _criptografiaService.Criptografar(pedido.Cartao.CodigoSeguranca);

                PedidoMensagem pedidoMensagem = new()
                {
                    IdUsuario = idUsuario,
                    IdAplicativo = pedido.IdAplicativo,
                    SalvarCartao = pedido.SalvarCartao,
                    Cartao = pedido.Cartao
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
        public async Task<bool> CartaoEhValido(Cartao cartao)
        {
            if (cartao.Numero.Length == 16)
            {
                return true;
            }
            return false;
        }
    }
}
