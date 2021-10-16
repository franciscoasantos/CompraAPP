using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dominio.Model;

namespace API.Dominio.Services
{
    public interface IAplicativoService
    {
        public Task<IEnumerable<Aplicativo>> Get();
    }
}
