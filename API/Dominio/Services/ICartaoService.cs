using API.Dominio.Model;

namespace API.Dominio.Services
{
    public interface ICartaoService
    {
        bool CartaoEhValido(Cartao cartao);
        Cartao CriptografarDadosCartao(Cartao cartaoPedido);
    }
}
