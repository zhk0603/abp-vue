using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Abp.VueTemplate.Permission
{
    public interface IMenuRepository : IBasicRepository<Menu, Guid>
    {
    }
}
