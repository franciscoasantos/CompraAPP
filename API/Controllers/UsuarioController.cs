﻿using System.Threading.Tasks;
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
