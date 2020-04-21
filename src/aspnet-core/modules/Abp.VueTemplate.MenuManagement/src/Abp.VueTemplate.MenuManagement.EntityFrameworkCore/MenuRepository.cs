using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.VueTemplate.MenuManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuRepository : EfCoreRepository<IMenuManagementDbContext, Menu, Guid>, IMenuRepository
    {
        public MenuRepository(IDbContextProvider<MenuManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
