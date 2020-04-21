using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VueTemplate.MenuManagement
{
    public class CreateOrUpdateMenuDto : EntityDto
    {
        [Required]
        public string Name { get; set; }
      
        [Required]
        public string DisplayName { get; set; }
      
        [Required]
        public string ComponentPath { get; set; }
       
        [Required]
        public string RouterPath { get; set; }
        public Guid? ParentId { get; set; }
        public MenuEnumType MenuType { get; set; }
        public string Icon { get; set; }
        public string Sort { get; set; }
        public string TargetUrl { get; set; } // window.open _blank 
        public string PermissionKey { get; set; } // 此菜单关联的权限key.
    }
}