using System;
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
        private readonly ILoginRepository _loginRepository;
        private readonly ICriptografiaService _criptografiaService;

        public LoginService(ILogger<LoginService> logger, ILoginRepository loginRepository, ICriptografiaService criptografiaService)
        {
            _logger = logger;
            _loginRepository = loginRepository;
            _criptografiaService = criptografiaService;
        }

        public async Task<LoginResponse> Login(Login login)
        {
            try
            {
                var retorno = await _loginRepository.BuscarDadosLogin(login);

                if (retorno != null && _criptografiaService.Criptografar(login.Senha) == retorno.Senha)
                    return new LoginResponse { Logado = true, Detalhe = "Login efetuado com sucesso!" };

                return new LoginResponse { Logado = false, Detalhe = "Usuário ou senha inválidos!" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new LoginResponse { Logado = false, Detalhe = ex.Message };
            }
        }
    }
}
