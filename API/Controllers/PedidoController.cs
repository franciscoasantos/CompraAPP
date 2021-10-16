using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            return Ok(await _pedidoService.CriarPedido(pedido));
        }
    }
}
