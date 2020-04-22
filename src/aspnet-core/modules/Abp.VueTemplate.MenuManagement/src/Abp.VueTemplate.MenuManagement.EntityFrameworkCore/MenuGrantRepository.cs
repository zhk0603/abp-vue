using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Abp.VueTemplate.MenuManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuGrantRepository : EfCoreRepository<IMenuManagementDbContext, MenuGrant, Guid>, IMenuGrantRepository
    {
        public MenuGrantRepository(IDbContextProvider<IMenuManagementDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public async Task<MenuGrant> FindAsync(Guid menuId, string providerName, string providerKey,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .FirstOrDefaultAsync(s =>
                        s.MenuId == menuId &&
                        s.ProviderName == providerName &&
                        s.ProviderKey == providerKey,
                    GetCancellationToken(cancellationToken)
                );
        }

        public async Task<List<MenuGrant>> GetListAsync(string providerName, string providerKey,
            CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(x => x.ProviderKey == providerKey &&
                                          x.ProviderName == providerName)
                .ToListAsync(GetCancellationToken(cancellationToken));

            //return await (
            //    from g in DbSet
            //    join m in DbContext.Set<Menu>() on g.MenuId equals m.Id
            //    where g.ProviderKey == providerKey && g.ProviderName == providerName
            //    select new MenuGrantInfoDto
            //    {
            //        Id = g.MenuId,
            //        Name = m.Name,
            //        DisplayName = m.DisplayName,
            //        PermissionKey = m.PermissionKey,
            //        IsGranted = true
            //    }
            //).ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
