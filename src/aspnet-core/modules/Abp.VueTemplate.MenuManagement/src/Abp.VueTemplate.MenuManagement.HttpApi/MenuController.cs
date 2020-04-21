using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Abp.VueTemplate.MenuManagement
{
    [RemoteService]
    [Route("api/menus")]
    public class MenuController : MenuManagementController, IMenuAppService
    {
        private readonly IMenuAppService _menuAppService;

        public MenuController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<MenuDto> GetAsync(Guid id)
        {
            return _menuAppService.GetAsync(id);
        }

        [NonAction]
        public Task<PagedResultDto<MenuDto>> GetListAsync(MenuRequestDto input)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<MenuDto> CreateAsync(CreateOrUpdateMenuDto input)
        {
            return _menuAppService.CreateAsync(input);
        }

        [HttpPut("{id}")]
        public Task<MenuDto> UpdateAsync(Guid id, CreateOrUpdateMenuDto input)
        {
            return _menuAppService.UpdateAsync(id, input);
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _menuAppService.DeleteAsync(id);
        }

        [HttpGet("all")]
        public Task<PagedResultDto<MenuDto>> GetAllListAsync(MenuRequestDto input)
        {
            return _menuAppService.GetAllListAsync(input);
        }
    }
}
