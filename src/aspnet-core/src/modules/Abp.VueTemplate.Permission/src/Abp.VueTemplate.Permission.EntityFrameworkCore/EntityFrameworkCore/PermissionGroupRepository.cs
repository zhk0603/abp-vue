using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VueTemplate.Permission.EntityFrameworkCore
{
    public class PermissionGroupRepository : EfCoreRepository<IPermissionDbContext, PermissionGroup, Guid>,
        IPermissionGroupRepository
    {
        public PermissionGroupRepository(IDbContextProvider<IPermissionDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }
    }
}