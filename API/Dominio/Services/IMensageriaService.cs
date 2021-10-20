using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Services
{
    public interface IMensageriaService
    {
        public Task<bool> ProcessarPedido(PedidoMensagem pedido);
    }
}
