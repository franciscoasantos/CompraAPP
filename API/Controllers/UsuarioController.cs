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
        public async Task<LoginResponse> Login([FromBody] Login login)
        {
            var retornoLogin = await _usuarioServices.Login(login);

            return retornoLogin;
        }

        [HttpPost("cadastro")]
        public async Task<CadastroResponse> Cadastro([FromBody] Cadastro cadastro)
        {
            var retornoCadastro = await _usuarioServices.Cadastrar(cadastro);

            return retornoCadastro;
        }
    }
}
