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

                if (retorno.Any())
                {
                    if (_criptografiaService.Criptografar(login.Senha) != retorno.FirstOrDefault().Senha)
                        return new LoginResponse { Logado = false, Detalhe = "Usuário ou senha inválidos!" };
                }
                else
                    return new LoginResponse { Logado = false, Detalhe = "Usuario não cadastrado." };

                return new LoginResponse { Logado = true, Detalhe = "Login efetuado com sucesso!" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new LoginResponse { Logado = false, Detalhe = ex.Message };
            }
        }
    }
}
