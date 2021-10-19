using System;
using System.Threading.Tasks;
using API.Dominio.Exceptions;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Dominio.Services;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ILogger<UsuarioService> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICriptografiaService _criptografiaService;

        public UsuarioService(ILogger<UsuarioService> logger, IUsuarioRepository usuarioRepository, ICriptografiaService criptografiaService)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _criptografiaService = criptografiaService;
        }

        public async Task<CadastroResponse> Cadastrar(Cadastro cadastro)
        {
            try
            {
                //Algoritomo para criptografar senha
                cadastro.Senha = _criptografiaService.Criptografar(cadastro.Senha);

                //Validar se usuário já existe
                if (await _usuarioRepository.ValidarUsuarioExistente(cadastro.Cpf) > 0)
                    throw new CadastroException("Já existe um usuário cadastrado com os parâmetros informados.");

                var retorno = await _usuarioRepository.CadastrarUsuario(cadastro);
                await _usuarioRepository.CadastrarSenha(retorno.IdUsuario, cadastro.Senha);

                retorno.Sucesso = true;

                return retorno;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CadastroResponse { Sucesso = false, Detalhe = ex.Message };
            }
        }
    }
}
