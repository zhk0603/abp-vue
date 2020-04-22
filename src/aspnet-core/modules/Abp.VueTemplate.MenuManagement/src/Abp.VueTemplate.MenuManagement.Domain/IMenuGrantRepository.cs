using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VueTemplate.MenuManagement
{
    public interface IMenuGrantRepository : IBasicRepository<MenuGrant, Guid>
    {
        Task<MenuGrant> FindAsync(Guid menuId,
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default);

        Task<List<MenuGrant>> GetListAsync(
            string providerName,
            string providerKey,
            CancellationToken cancellationToken = default
        );
    }
}
