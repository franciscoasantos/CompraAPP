using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioServices;
        private readonly ILoginService _loginService;

        public UsuarioController(IUsuarioService usuarioService, ILoginService loginService)
        {
            _usuarioServices = usuarioService;
            _loginService = loginService;
        }

        /// <summary>
        /// Realiza login para obtenção de token.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///     {
        ///       "usuario": "45726273044",
        ///       "senha": "Ess@Senh@EhF0rt3"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Token de acesso</response>
        /// <response code="401">Erro no login</response>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var retornoLogin = await _loginService.Login(login);

            if (retornoLogin.Logado)
                return Ok(retornoLogin);
            else
                return Unauthorized(retornoLogin);
        }

        /// <summary>
        /// Realiza cadastro do usuário.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /cadastro
        ///     {
        ///       "nome": "José Exemplo da Silva",
        ///       "dataNascimento": "1998-12-17",
        ///       "cpf": "45726273044",
        ///       "sexo": "M",
        ///       "endereco": {
        ///         "logradouro": "Rua do Amendoim",
        ///         "numero": 100,
        ///         "bairro": "Mangabeiras",
        ///         "cidade": "Belo Horizonte",
        ///         "estado": "MG",
        ///         "complemento": "Casa",
        ///         "pontoDeReferencia": "Muro azul"
        ///       },
        ///         "senha": "Ess@Senh@EhF0rt3"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        /// <response code="201">Usuário criado</response>
        /// <response code="422">Erro na requisição</response>
        [HttpPost("cadastro")]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastro([FromBody] Cadastro cadastro)
        {
            var retornoCadastro = await _usuarioServices.Cadastrar(cadastro);

            if (retornoCadastro.Sucesso)
                return Created("", retornoCadastro);
            else
                return UnprocessableEntity(retornoCadastro);
        }
    }
}
