using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuGrantCacheItemInvalidator : 
        ILocalEventHandler<EntityCreatedEventData<MenuGrant>>,
        ILocalEventHandler<EntityDeletedEventData<MenuGrant>>,
        ITransientDependency
    {
        public MenuGrantCacheItemInvalidator(
            ICurrentTenant currentTenant,
            IDistributedCache<MenuGrantCacheItem> cache
        )
        {
            CurrentTenant = currentTenant;
            Cache = cache;
        }

        protected ICurrentTenant CurrentTenant { get; }

        protected IDistributedCache<MenuGrantCacheItem> Cache { get; }
       
        public async Task HandleEventAsync(EntityCreatedEventData<MenuGrant> eventData)
        {
            var cacheKey = CalculateCacheKey(
                eventData.Entity.MenuId,
                eventData.Entity.ProviderName,
                eventData.Entity.ProviderKey
            );

            using (CurrentTenant.Change(eventData.Entity.TenantId))
            {
                await Cache.SetAsync(cacheKey, new MenuGrantCacheItem(eventData.Entity.MenuId, true));
            }
        }

        public async Task HandleEventAsync(EntityDeletedEventData<MenuGrant> eventData)
        {
            var cacheKey = CalculateCacheKey(
                eventData.Entity.MenuId,
                eventData.Entity.ProviderName,
                eventData.Entity.ProviderKey
            );

            using (CurrentTenant.Change(eventData.Entity.TenantId))
            {
                await Cache.SetAsync(cacheKey, new MenuGrantCacheItem(eventData.Entity.MenuId, false));
            }
        }

        protected virtual string CalculateCacheKey(
            Guid menuId,
            string providerName,
            string providerKey
        )
        {
            return MenuGrantCacheItem.CalculateCacheKey(menuId, providerName, providerKey);
        }
    }
}