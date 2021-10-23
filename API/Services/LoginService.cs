using System;
using System.Linq;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Dominio.Services;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> _logger;
        private readonly ITokenService _tokenService;
        private readonly ILoginRepository _loginRepository;
        private readonly ICriptografiaService _criptografiaService;

        public LoginService(ILogger<LoginService> logger, ITokenService tokenService, ILoginRepository loginRepository, ICriptografiaService criptografiaService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _loginRepository = loginRepository;
            _criptografiaService = criptografiaService;
        }

        public async Task<LoginResponse> Login(Login login)
        {
            try
            {
                var cadastro = await _loginRepository.BuscarDadosLogin(login);

                if (cadastro != null)
                {
                    if (!SenhaValida(login.Senha, cadastro.Senha))
                        return new LoginResponse { Logado = false, Detalhe = "Usuário ou senha inválidos!" };
                }
                else
                    return new LoginResponse { Logado = false, Detalhe = "Usuario não cadastrado." };

                var token = _tokenService.GerarToken(login);

                return new LoginResponse { Logado = true, Token = token};
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new LoginResponse { Logado = false, Detalhe = ex.Message };
            }
        }

        private bool SenhaValida(string senhaInformada, string senhaCadastrada)
        {
            return _criptografiaService.Criptografar(senhaInformada) == senhaCadastrada;
        }
    }
}
