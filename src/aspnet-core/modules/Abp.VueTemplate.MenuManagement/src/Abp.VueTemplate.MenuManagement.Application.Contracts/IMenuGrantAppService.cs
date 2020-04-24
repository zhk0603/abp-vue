using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Application.Services;

namespace Abp.VueTemplate.MenuManagement
{
    public interface IMenuGrantAppService : IApplicationService
    {
        Task<GetMenuResultDto> GetListAsync();
        Task<GetMenuGrantListResultDto> GetAsync([NotNull] string providerName, [NotNull] string providerKey);
        Task UpdateAsync([NotNull] string providerName, [NotNull] string providerKey, UpdateMenuGrantsDto input);
    }
}
