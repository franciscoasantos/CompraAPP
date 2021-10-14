using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Repositories
{
    public interface IUsuarioRepository
    {
        Task<LoginResponse> Login(Login login);
        Task<CadastroResponse> Cadastrar(Cadastro cadastro);
    }
}
