using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Dominio.Services;

namespace API.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public Task<PedidoResponse> CriarPedido(Pedido pedido)
        {
            throw new System.NotImplementedException();
        }
    }
}
