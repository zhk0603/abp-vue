using System;
using Volo.Abp.Domain.Repositories;

namespace Abp.VueTemplate.Permission
{
    public interface IPermissionPageRepository : IBasicRepository<PermissionPage, Guid>
    {
    }
}