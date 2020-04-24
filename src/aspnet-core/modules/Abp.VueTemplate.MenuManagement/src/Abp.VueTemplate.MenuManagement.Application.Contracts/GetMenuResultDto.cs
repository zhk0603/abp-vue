using System.Collections.Generic;

namespace Abp.VueTemplate.MenuManagement
{
    public class GetMenuResultDto
    {
        public List<string> PermissionGrants { get; set; }
        public List<VueMenu> Menus { get; set; }
    }
}