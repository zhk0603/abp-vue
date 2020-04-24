using System;
using System.Collections.Generic;
using Volo.Abp.PermissionManagement;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuGrantInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string PermissionKey { get; set; }
        public bool IsGranted { get; set; }
        public List<MenuProviderInfo> GrantedProviders { get; set; }
    }
}