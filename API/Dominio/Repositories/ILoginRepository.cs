using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Repositories
{
    public interface ILoginRepository
    {
        Task<IEnumerable<Login>> BuscarDadosLogin(Login login);
    }
}
