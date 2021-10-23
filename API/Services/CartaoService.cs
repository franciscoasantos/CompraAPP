using API.Dominio.Model;
using API.Dominio.Services;

namespace API.Services
{
    public class CartaoService : ICartaoService
    {
        private readonly ICriptografiaService _criptografiaService;

        public CartaoService(ICriptografiaService criptografiaService)
        {
            _criptografiaService = criptografiaService;
        }
        public Cartao CriptografarDadosCartao(Cartao cartaoPedido)
        {
            Cartao cartao = new();
            cartao.Numero = _criptografiaService.Criptografar(cartaoPedido.Numero);
            cartao.Vencimento = _criptografiaService.Criptografar(cartaoPedido.Vencimento);
            cartao.CodigoSeguranca = _criptografiaService.Criptografar(cartaoPedido.CodigoSeguranca);

            return cartao;
        }

        public bool CartaoEhValido(Cartao cartao)
        {
            if (cartao.Numero.Length == 16)
            {
                return true;
            }
            return false;
        }

    }
}
