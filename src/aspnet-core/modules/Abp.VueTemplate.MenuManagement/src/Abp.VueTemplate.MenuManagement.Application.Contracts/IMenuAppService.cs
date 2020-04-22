using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VueTemplate.MenuManagement
{
    public interface
        IMenuAppService : ICrudAppService<MenuDto, Guid, MenuRequestDto, CreateOrUpdateMenuDto>
    {
        Task<PagedResultDto<MenuDto>> GetAllListAsync(MenuRequestDto input);
        Task<GetMenuGrantListResultDto> GetGrantAsync([NotNull] string providerName, [NotNull] string providerKey);
        Task UpdateGrantAsync([NotNull] string providerName, [NotNull] string providerKey, UpdateMenuGrantsDto input);
    }
}
