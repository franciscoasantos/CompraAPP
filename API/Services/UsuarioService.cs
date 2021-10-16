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

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<CadastroResponse> Cadastrar(Cadastro cadastro)
        {
            try
            {
                //Algoritomo para criptografar senha
                var criptografia = new CriptografiaService();
                cadastro.Senha = criptografia.Criptografar(cadastro.Senha);

                var retorno = await _usuarioRepository.Cadastrar(cadastro);
                //Todo criar Enum para mensagens do sistema
                retorno.Detalhe = "Usuário criado com sucesso!";

                return retorno;
            }
            catch (SqlException ex)
            {
                //Todo Enum para codigos de erro SQL
                if (ex.Number == 2627)
                    return new CadastroResponse { Id = -1, Detalhe = "Já existe um usuário cadastrado com os parâmetros informados." };
                else
                    return new CadastroResponse { Id = -1, Detalhe = ex.Message };
            }
            catch (Exception ex)
            {
                return new CadastroResponse { Id = -1, Detalhe = ex.Message };
            }
        }

        public async Task<LoginResponse> Login(Login login)
        {
            return await _usuarioRepository.Login(login);
        }
    }
}
