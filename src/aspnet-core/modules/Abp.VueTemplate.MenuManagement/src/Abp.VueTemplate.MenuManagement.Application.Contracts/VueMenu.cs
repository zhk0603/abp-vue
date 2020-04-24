using System;
using System.Collections.Generic;

namespace Abp.VueTemplate.MenuManagement
{
    public class VueMenu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Component { get; set; }
        public string Path { get; set; }
        public string TargetUrl { get; set; } // window.open _blank 
        public string PermissionKey { get; set; } // 此菜单关联的权限key.
        public MenuMeta Meta { get; set; }
        public List<VueMenu> Children { get; set; } = new List<VueMenu>();
    }
}