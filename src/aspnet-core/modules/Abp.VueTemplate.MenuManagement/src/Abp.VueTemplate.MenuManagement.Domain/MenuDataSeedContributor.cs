using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Abp.VueTemplate.MenuManagement
{
    public interface IMenuDataSeedProvider
    {
        List<Menu> Menus { get; }
        Task<List<Guid>> GetGrantMenuIdsAsync(DataSeedContext context);
    }

    public class NullMenuDataSeeder : IMenuDataSeedProvider
    {
        public static NullMenuDataSeeder Instance = new NullMenuDataSeeder();

        public List<Menu> Menus { get; } = new List<Menu>();

        public Task<List<Guid>> GetGrantMenuIdsAsync(DataSeedContext context)
        {
            return Task.FromResult(new List<Guid>());
        }
    }

    public class MenuDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public static string AdminRoleName = "admin";

        protected IMenuRepository MenuRepository { get; }
        protected IMenuGrantRepository MenuGrantRepository { get; }
        protected IGuidGenerator GuidGenerator { get; }
        protected ICurrentTenant CurrentTenant { get; }
        public IMenuDataSeedProvider MenuDataSeedProvider { get; set; }

        public MenuDataSeedContributor(
            IMenuRepository menuRepository,
            IMenuGrantRepository menuGrantRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant
        )
        {
            MenuRepository = menuRepository;
            MenuGrantRepository = menuGrantRepository;
            MenuDataSeedProvider = NullMenuDataSeeder.Instance;
            GuidGenerator = guidGenerator;
            CurrentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await InsertMenusAsync(context);
            await InsertMenuGrantAsync(context);
        }

        private async Task InsertMenusAsync(DataSeedContext context)
        {
            foreach (var menu in MenuDataSeedProvider.Menus)
            {
                if (MenuRepository.Any(x => x.DisplayName == menu.DisplayName))
                {
                    continue;
                }

                await MenuRepository.InsertAsync(menu);
            }
        }

        private async Task InsertMenuGrantAsync(DataSeedContext context)
        {
            foreach (var menuId in await MenuDataSeedProvider.GetGrantMenuIdsAsync(context))
            {
                if ((await MenuGrantRepository.FindAsync(menuId, "R", AdminRoleName)) != null)
                {
                    continue;
                }

                await MenuGrantRepository.InsertAsync(new MenuGrant(
                    GuidGenerator.Create(),
                    menuId,
                    "R",
                    AdminRoleName,
                    context.TenantId));
            }
        }
    }
}
