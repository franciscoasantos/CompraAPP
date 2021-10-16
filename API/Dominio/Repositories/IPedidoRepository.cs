using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Repositories
{
    public interface IPedidoRepository
    {
        public Task<PedidoResponse> CriarPedido(Pedido pedido);
    }
}
