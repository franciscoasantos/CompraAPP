using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Login> BuscarDadosLogin(Login login);
        Task<CadastroResponse> CadastrarUsuario(Cadastro cadastro);
        Task<int> CadastrarSenha(long idUsuario, string senha);
    }
}
