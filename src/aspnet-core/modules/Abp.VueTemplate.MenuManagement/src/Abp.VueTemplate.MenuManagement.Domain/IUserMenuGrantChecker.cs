using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.MenuManagement
{
    public interface IUserMenuGrantChecker
    {
        Task<bool> CheckAsync(ClaimsPrincipal claimsPrincipal, Menu menu);
    }

    public class UserMenuGrantChecker : IUserMenuGrantChecker, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IEnumerable<IMenuGrantRuntimeCheckerProvider> _checkerProviders;

        public UserMenuGrantChecker(
            ICurrentTenant currentTenant,
            IEnumerable<IMenuGrantRuntimeCheckerProvider> checkerProviders
        )
        {
            _currentTenant = currentTenant;
            _checkerProviders = checkerProviders;
        }

        public async Task<bool> CheckAsync(ClaimsPrincipal claimsPrincipal, Menu menu)
        {
            var multiTenancySide = claimsPrincipal?.GetMultiTenancySide()
                                   ?? _currentTenant.GetMultiTenancySide();

            if (!menu.MultiTenancySide.HasFlag(multiTenancySide))
            {
                return false;
            }

            if (menu.PermissionKey.IsNullOrWhiteSpace())
            {
                return true;
            }

            var isGranted = false;
            var content = new MenuGrantRuntimeCheckerContent
            {
                Menu = menu,
                Principal = claimsPrincipal
            };

            foreach (var provider in _checkerProviders)
            {
                var result = await provider.CheckAsync(content);
                if (result == MenuGrantResultEnum.Granted)
                {
                    isGranted = true;
                    break;
                }
            }

            return isGranted;
        }
    }
}