using System;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Services;

namespace API.Dominio.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DbSession _sessao;

        public PedidoRepository(DbSession sessao)
        {
            _sessao = sessao;
        }

        public Task<PedidoResponse> CriarPedido(Pedido pedido)
        {
            throw new NotImplementedException();
        }
    }
}
