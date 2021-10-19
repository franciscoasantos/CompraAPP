using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Dominio.Services;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class AplicativoService : IAplicativoService
    {
        private readonly ILogger<AplicativoService> _logger;
        private readonly IAplicativoRepository _aplicativoRepository;
        public AplicativoService(ILogger<AplicativoService> logger, IAplicativoRepository aplicativoRepository)
        {
            _logger = logger;
            _aplicativoRepository = aplicativoRepository;
        }
        public Task<IEnumerable<Aplicativo>> Get()
        {
            try
            {
            return _aplicativoRepository.Get();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Task.FromResult<IEnumerable<Aplicativo>>(null);
            }
        }
    }
}
