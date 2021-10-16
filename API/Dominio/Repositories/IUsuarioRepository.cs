using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Repositories
{
    public interface IUsuarioRepository
    {
        Task<CadastroResponse> CadastrarUsuario(Cadastro cadastro);
        Task<int> CadastrarSenha(long idUsuario, string senha);
        Task<int> ValidarUsuarioExistente(string cpf);
    }
}
