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

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Aplicativo>> Get()
        {
            return await _aplicativoService.Get();
        }
    }
}
