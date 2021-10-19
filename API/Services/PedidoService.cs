using System;
using System.Threading.Tasks;
using API.Dominio.Exceptions;
using API.Dominio.Model;
using API.Dominio.Services;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly ILogger<PedidoService> _logger;
        private readonly IMensageriaService _mensageriaService;
        private readonly ICriptografiaService _criptografiaService;

        public PedidoService(ILogger<PedidoService> logger, IMensageriaService mensageriaService, ICriptografiaService criptografiaService)
        {
            _logger = logger;
            _mensageriaService = mensageriaService;
            _criptografiaService = criptografiaService;
        }

        public async Task<PedidoResponse> CriarPedido(Pedido pedido)
        {
            try
            {
                if (!await CartaoEhValido(pedido.Cartao))
                    return new PedidoResponse { Sucesso = false, Detalhe = "O cartão informado não é válido." };

                pedido.Cartao.Numero = _criptografiaService.Criptografar(pedido.Cartao.Numero);
                pedido.Cartao.Vencimento = _criptografiaService.Criptografar(pedido.Cartao.Vencimento);
                pedido.Cartao.CodigoSeguranca = _criptografiaService.Criptografar(pedido.Cartao.CodigoSeguranca);

                await _mensageriaService.ProcessarPedido(pedido);
                return new PedidoResponse { Sucesso = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new PedidoResponse { Sucesso = false, Detalhe = ex.Message };
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
