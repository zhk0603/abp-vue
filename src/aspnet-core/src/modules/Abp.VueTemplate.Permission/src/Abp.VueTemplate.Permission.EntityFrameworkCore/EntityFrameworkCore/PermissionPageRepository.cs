using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VueTemplate.Permission.EntityFrameworkCore
{
    public class PermissionPageRepository : EfCoreRepository<IPermissionDbContext, PermissionPage, Guid>,
        IPermissionPageRepository
    {
        public PermissionPageRepository(IDbContextProvider<IPermissionDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }
    }
}