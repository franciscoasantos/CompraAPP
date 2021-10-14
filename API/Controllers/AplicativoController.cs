using API.Dominio.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AplicativoController : ControllerBase
    {
        [HttpGet]
        public Aplicativo Get()
        {
            return new Aplicativo
            {
                Nome = "Teste",
                Preco = 199.90
            };
        }
    }
}
