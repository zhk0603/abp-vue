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

}
