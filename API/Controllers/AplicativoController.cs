using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AplicativoController : ControllerBase
    {
        private readonly IAplicativoService _aplicativoService;

        public AplicativoController(IAplicativoService aplicativoService)
        {
            _aplicativoService = aplicativoService;
        }

        /// <summary>
        /// Lista os aplicativos para compra.
        /// </summary>
        /// <returns>Lista de aplicativos disponiveis</returns>
        /// <response code="200">Lista de aplicativos disponiveis</response>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Aplicativo>> Get()
        {
            return await _aplicativoService.Get();
        }
    }
}
