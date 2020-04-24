using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuGrantChecker : IMenuGrantChecker, ITransientDependency
    {
        private readonly IDistributedCache<MenuGrantCacheItem> _cache;
        private readonly ILogger<MenuGrantChecker> _logger;
        private readonly IMenuGrantRepository _menuGrantRepository;

        public MenuGrantChecker(
            IDistributedCache<MenuGrantCacheItem> cache,
            ILogger<MenuGrantChecker> logger,
            IMenuGrantRepository menuGrantRepository
        )
        {
            _cache = cache;
            _logger = logger;
            _menuGrantRepository = menuGrantRepository;
        }

        public async Task<MenuGrantCacheItem> CheckAsync(Guid menuId, string providerName, string providerKey)
        {
            var cacheKey = MenuGrantCacheItem.CalculateCacheKey(menuId, providerName, providerKey);

            _logger.LogDebug("MenuGrantCheckerCache.CheckAsync: {cacheKey}", cacheKey);

            var cacheItem = await _cache.GetAsync(cacheKey);

            if (cacheItem != null)
            {
                _logger.LogDebug("Found in the cache: {cacheKey}", cacheKey);
                return cacheItem;
            }

            _logger.LogDebug("Not found in the cache, getting from the repository: {cacheKey}", cacheKey);

            cacheItem = new MenuGrantCacheItem(
                menuId,
                (await _menuGrantRepository.FindAsync(menuId, providerName, providerKey)) != null
            );

            _logger.LogDebug("Setting the cache item: {cacheKey}", cacheKey);

            await _cache.SetAsync(
                cacheKey,
                cacheItem
            );

            _logger.LogDebug("Finished setting the cache item: {cacheKey}", cacheKey);

            return cacheItem;
        }
    }
}