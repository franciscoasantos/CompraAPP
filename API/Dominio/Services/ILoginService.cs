using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(Login login);
    }
}
