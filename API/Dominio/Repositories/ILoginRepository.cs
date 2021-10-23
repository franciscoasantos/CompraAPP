using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Repositories
{
    public interface ILoginRepository
    {
        Task<Login> BuscarDadosLogin(Login login);
    }
}
