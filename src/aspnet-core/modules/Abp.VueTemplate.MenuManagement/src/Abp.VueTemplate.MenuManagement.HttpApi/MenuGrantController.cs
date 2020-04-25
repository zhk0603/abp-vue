using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace Abp.VueTemplate.MenuManagement
{
    [RemoteService]
    [Route("api/menu-grant")]
    public class MenuGrantController : MenuManagementController, IMenuGrantAppService
    {
        private readonly IMenuGrantAppService _menuGrantAppService;

        public MenuGrantController(IMenuGrantAppService menuGrantAppService)
        {
            _menuGrantAppService = menuGrantAppService;
        }

        [HttpGet("list")]
        public virtual Task<GetMenuResultDto> GetListAsync()
        {
            return _menuGrantAppService.GetListAsync();
        }

        [HttpGet]
        public virtual Task<GetMenuGrantListResultDto> GetAsync(string providerName, string providerKey)
        {
            return _menuGrantAppService.GetAsync(providerName, providerKey);
        }

        [HttpPut]
        public virtual Task UpdateAsync(string providerName, string providerKey, UpdateMenuGrantsDto input)
        {
            return _menuGrantAppService.UpdateAsync(providerName, providerKey, input);
        }
    }
}
