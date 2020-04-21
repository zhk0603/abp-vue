using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Abp.VueTemplate.MenuManagement
{
    public interface IMenuRepository : IRepository<Menu, Guid>
    {
    }
}
