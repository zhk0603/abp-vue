using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.MenuManagement
{
    public interface IMenuManagementProvider : ITransientDependency
    {
        string Name { get; }

        Task<MenuGrantInfo> CheckAsync(
            [NotNull] Guid menuId,
            [NotNull] string providerName,
            [NotNull] string providerKey
        );

        Task SetAsync(
            [NotNull] Guid menuId,
            [NotNull] string providerKey,
            bool isGranted
        );
    }

    public abstract class MenuManagementProvider : IMenuManagementProvider
    {
        protected IMenuGrantRepository MenuGrantRepository { get; }
        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }
        protected IGuidGenerator GuidGenerator { get; }
        protected ICurrentTenant CurrentTenant { get; }
        public abstract string Name { get; }

        protected MenuManagementProvider(IMenuGrantRepository menuGrantRepository,
            IPermissionDefinitionManager permissionDefinitionManager,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            MenuGrantRepository = menuGrantRepository;
            PermissionDefinitionManager = permissionDefinitionManager;
            GuidGenerator = guidGenerator;
            CurrentTenant = currentTenant;
        }

        public virtual async Task<MenuGrantInfo> CheckAsync(Guid menuId, string providerName, string providerKey)
        {
            if (providerName != Name)
            {
                return MenuGrantInfo.NonGranted;
            }

            return new MenuGrantInfo(
                await MenuGrantRepository.FindAsync(menuId, providerName, providerKey) != null,
                providerKey
            );
        }

        public virtual Task SetAsync(Guid menuId, string providerKey, bool isGranted)
        {
            return isGranted
                ? GrantAsync(menuId, providerKey)
                : RevokeAsync(menuId, providerKey);
        }

        protected virtual async Task GrantAsync(Guid menuId, string providerKey)
        {
            var menuGrant = await MenuGrantRepository.FindAsync(menuId, Name, providerKey);
            if (menuGrant != null)
            {
                return;
            }

            await MenuGrantRepository.InsertAsync(
                new MenuGrant(
                    GuidGenerator.Create(),
                    menuId,
                    Name,
                    providerKey,
                    CurrentTenant.Id
                )
            );
        }

        protected virtual async Task RevokeAsync(Guid menuId, string providerKey)
        {
            var permissionGrant = await MenuGrantRepository.FindAsync(menuId, Name, providerKey);
            if (permissionGrant == null)
            {
                return;
            }

            await MenuGrantRepository.DeleteAsync(permissionGrant);
        }
    }

    public class MenuGrantInfo
    {
        public static MenuGrantInfo NonGranted { get; } = new MenuGrantInfo(false);

        public virtual bool IsGranted { get; }

        public virtual string ProviderKey { get; }

        public MenuGrantInfo(bool isGranted, [CanBeNull] string providerKey = null)
        {
            IsGranted = isGranted;
            ProviderKey = providerKey;
        }
    }

    public class MenuWithGrantedProviders
    {
        public Guid MenuId { get; }

        public bool IsGranted { get; set; }

        public List<MenuProviderInfo> Providers { get; set; }

        public MenuWithGrantedProviders([NotNull] Guid menuId, bool isGranted)
        {
            Check.NotNull(menuId, nameof(menuId));

            MenuId = menuId;
            IsGranted = isGranted;

            Providers = new List<MenuProviderInfo>();
        }
    }
}
