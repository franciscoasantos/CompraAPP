using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServices;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioServices = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var retornoLogin = await _usuarioServices.Login(login);

            if (retornoLogin.Logado)
                return Ok(retornoLogin);
            else
                return Unauthorized(retornoLogin);
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastro([FromBody] Cadastro cadastro)
        {
            var retornoCadastro = await _usuarioServices.Cadastrar(cadastro);

            if (retornoCadastro.IdUsuario == -1)
                return Conflict(retornoCadastro);
            else
                return Created("", retornoCadastro);
        }
    }
}
