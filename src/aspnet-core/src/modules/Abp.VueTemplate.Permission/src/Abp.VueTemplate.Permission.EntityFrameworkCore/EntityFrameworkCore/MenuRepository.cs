using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VueTemplate.Permission.EntityFrameworkCore
{
    public class MenuRepository : EfCoreRepository<IPermissionDbContext, Menu, Guid>, IMenuRepository
    {
        public MenuRepository(IDbContextProvider<IPermissionDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
