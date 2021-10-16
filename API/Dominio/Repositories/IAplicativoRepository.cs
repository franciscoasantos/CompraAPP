using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Repositories
{
    public interface IAplicativoRepository
    {
        public Task<IEnumerable<Aplicativo>> Get();
    }
}
