using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public MenuEnumType MenuType { get; set; }
        public string ComponentPath { get; set; }
        public string RouterPath { get; set; }
        public Guid? ParentId { get; set; }
        public string Icon { get; set; }
        public string Sort { get; set; }
        public string TargetUrl { get; set; } // window.open _blank 
        public string PermissionKey { get; set; } // 此菜单关联的权限key.
        public List<MenuDto> Children { get; set; } = new List<MenuDto>();
    }
}
