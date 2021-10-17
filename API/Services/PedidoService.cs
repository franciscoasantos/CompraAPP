using System;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Services;

namespace API.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IMensageriaService _mensageriaService;
        private readonly ICriptografiaService _criptografiaService;

        public PedidoService(IMensageriaService mensageriaService, ICriptografiaService criptografiaService)
        {
            _mensageriaService = mensageriaService;
            _criptografiaService = criptografiaService;
        }

        public async Task<PedidoResponse> CriarPedido(Pedido pedido)
        {
            try
            {
                pedido.Cartao.Numero = _criptografiaService.Criptografar(pedido.Cartao.Numero);
                pedido.Cartao.Vencimento = _criptografiaService.Criptografar(pedido.Cartao.Vencimento);
                pedido.Cartao.CodigoSeguranca = _criptografiaService.Criptografar(pedido.Cartao.CodigoSeguranca);
                await _mensageriaService.ProcessarPedido(pedido);
                return new PedidoResponse { Sucesso = true };
            }
            catch (Exception ex)
            {
                return new PedidoResponse { Sucesso = false, Detalhe = ex.Message };
            }
        }
    }
}
