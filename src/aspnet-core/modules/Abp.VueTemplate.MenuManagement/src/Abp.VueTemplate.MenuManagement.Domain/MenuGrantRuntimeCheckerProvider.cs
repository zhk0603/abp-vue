using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;

namespace Abp.VueTemplate.MenuManagement
{
    public abstract class MenuGrantRuntimeCheckerProvider : IMenuGrantRuntimeCheckerProvider
    {
        protected IMenuGrantChecker MenuGrantChecker { get; }

        protected MenuGrantRuntimeCheckerProvider(IMenuGrantChecker menuGrantChecker)
        {
            MenuGrantChecker = menuGrantChecker;
        }

        public abstract Task<MenuGrantResultEnum> CheckAsync(MenuGrantRuntimeCheckerContent context);
    }

    public class RoleMenuGrantRuntimeCheckerProvider : MenuGrantRuntimeCheckerProvider, ITransientDependency
    {
        public RoleMenuGrantRuntimeCheckerProvider(IMenuGrantChecker menuGrantChecker) : base(menuGrantChecker)
        {
        }

        public override async Task<MenuGrantResultEnum> CheckAsync(MenuGrantRuntimeCheckerContent context)
        {
            var roles = context.Principal?.FindAll(AbpClaimTypes.Role).Select(c => c.Value).ToArray();

            if (roles == null || !roles.Any())
            {
                return MenuGrantResultEnum.Undefined;
            }

            foreach (var role in roles)
            {
                var result = await MenuGrantChecker.CheckAsync(context.Menu.Id, "R", role);
                if (result.IsGranted)
                {
                    return MenuGrantResultEnum.Granted;
                }
            }

            return MenuGrantResultEnum.Undefined;
        }
    }

    public class UserMenuGrantRuntimeCheckerProvider : MenuGrantRuntimeCheckerProvider, ITransientDependency
    {
        public UserMenuGrantRuntimeCheckerProvider(IMenuGrantChecker menuGrantChecker) : base(menuGrantChecker)
        {
        }

        public override async Task<MenuGrantResultEnum> CheckAsync(MenuGrantRuntimeCheckerContent context)
        {
            var userId = context.Principal?.FindFirst(AbpClaimTypes.UserId)?.Value;
            var result = await MenuGrantChecker.CheckAsync(context.Menu.Id, "U", userId);
            return result.IsGranted ? MenuGrantResultEnum.Granted : MenuGrantResultEnum.Undefined;
        }
    }
}