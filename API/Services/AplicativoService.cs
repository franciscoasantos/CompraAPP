using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Dominio.Services;

namespace API.Services
{
    public class AplicativoService : IAplicativoService
    {
        public readonly IAplicativoRepository _aplicativoRepository;
        public AplicativoService(IAplicativoRepository aplicativoRepository)
        {
            _aplicativoRepository = aplicativoRepository;
        }
        public Task<IEnumerable<Aplicativo>> Get()
        {
            return _aplicativoRepository.Get();
        }
    }
}
