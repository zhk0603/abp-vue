using System.Collections.Generic;

namespace Abp.VueTemplate.MenuManagement
{
    public class GetMenuGrantListResultDto
    {
        public List<MenuGrantInfoDto> MenuGrants { get; set; }
        public List<MenuDto> Menus { get; set; }
    }
}