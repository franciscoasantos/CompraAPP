using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Services;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Cria um novo pedido.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /pedido
        ///     {
        ///       "idAplicativo": 1,
        ///       "cartao": {
        ///         "numero": "1234567890123456",
        ///         "vencimento": "2021-10",
        ///         "codigoSeguranca": "123"
        ///       },
        ///       "salvarCartao": true
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Sucesso</response>
        /// <response code="422">Erro na requisição</response>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            return Ok(await _pedidoService.CriarPedido(User.Identity.Name, pedido));
        }
    }
}
