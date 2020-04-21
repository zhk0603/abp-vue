using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Abp.VueTemplate.MenuManagement
{
    public class CreateOrUpdatePermissionDto : EntityDto<Guid>
    {
        [Required]
        public string DisplayName { get; set; }
        public string PermissionKey { get; set; } // 此菜单关联的权限key.
      
        [Required]
        public Guid ParentId { get; set; }
    }
}