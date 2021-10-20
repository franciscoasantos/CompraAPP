using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dominio.Model;
using API.Dominio.Repositories;
using API.Dominio.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class AplicativoService : IAplicativoService
    {
        private readonly ILogger<AplicativoService> _logger;
        private readonly IMemoryCache _cache;
        private readonly IAplicativoRepository _aplicativoRepository;
        private readonly long cacheExpiration;
        public AplicativoService(ILogger<AplicativoService> logger, IConfiguration config, IMemoryCache memoryCache, IAplicativoRepository aplicativoRepository)
        {
            _logger = logger;
            _cache = memoryCache;
            _aplicativoRepository = aplicativoRepository;
            cacheExpiration = Convert.ToInt64(config.GetSection("Configuracoes:cacheExpirationEmSegundos").Value);
        }
        public async Task<IEnumerable<Aplicativo>> Get()
        {
            try
            {
                string cacheKey = "app_cache_key";

                if (!_cache.TryGetValue(cacheKey, out IEnumerable<Aplicativo> cacheEntry))
                {
                    cacheEntry = await _aplicativoRepository.Get();

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheExpiration));

                    _cache.Set(cacheKey, cacheEntry, cacheEntryOptions);
                }
                return cacheEntry;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult<IEnumerable<Aplicativo>>(null);
            }
        }
    }
}
