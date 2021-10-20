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
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(ILogger<UsuarioService> logger, IUsuarioRepository usuarioRepository, ICriptografiaService criptografiaService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _criptografiaService = criptografiaService;
            _unitOfWork = unitOfWork;
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

                _unitOfWork.BeginTransaction();

                var retorno = await _usuarioRepository.CadastrarUsuario(cadastro);
                await _usuarioRepository.CadastrarSenha(retorno.IdUsuario, cadastro.Senha);

                _unitOfWork.Commit();

                retorno.Sucesso = true;

                return retorno;
            }
            catch (Exception ex)
            {
                if (_unitOfWork.ExisteTransacao())
                    _unitOfWork.Rollback();

                _logger.LogError(ex, ex.Message);
                return new CadastroResponse { Sucesso = false, Detalhe = ex.Message };
            }
        }
    }
}
