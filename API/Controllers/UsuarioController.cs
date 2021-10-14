using API.Dominio.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("login")]
        public LoginResponse Login([FromBody] Login login)
        {
            if (login.Senha == "1234")
            {
                return new LoginResponse { Logado = true };
            }
            else
            {
                return new LoginResponse { Logado = false };
            }
        }
        [HttpPost("cadastro")]
        public CadastroResponse Cadastro([FromBody] Cadastro cadastro)
        {
            return new CadastroResponse { Id = 1 };
        }
    }
}
