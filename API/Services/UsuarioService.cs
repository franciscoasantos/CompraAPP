using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Dominio.Services;

namespace API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICriptografiaService _criptografiaService;

        public UsuarioService(IUsuarioRepository usuarioRepository, ICriptografiaService criptografiaService )
        {
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
                    throw new Exception("Já existe um usuário cadastrado com os parâmetros informados.");

                var retorno = await _usuarioRepository.CadastrarUsuario(cadastro);
                await _usuarioRepository.CadastrarSenha(retorno.IdUsuario, cadastro.Senha);

                //Todo criar Enum para mensagens do sistema
                retorno.Detalhe = "Usuário criado com sucesso!";

                return retorno;
            }
            catch (Exception ex)
            {
                return new CadastroResponse { IdUsuario = -1, Detalhe = ex.Message };
            }
        }

        public async Task<LoginResponse> Login(Login login)
        {
            try
            {
                var retorno = await _usuarioRepository.BuscarDadosLogin(login);

                if (retorno != null && login.Senha == _criptografiaService.Descriptografar(retorno.Senha))
                    return new LoginResponse { Logado = true, Detalhe = "Login efetuado com sucesso!" };

                return new LoginResponse { Logado = false, Detalhe = "Usuário ou senha inválidos!" };
            }
            catch (Exception ex)
            {
                return new LoginResponse { Logado = false, Detalhe = ex.Message };
            }
        }
    }
}
