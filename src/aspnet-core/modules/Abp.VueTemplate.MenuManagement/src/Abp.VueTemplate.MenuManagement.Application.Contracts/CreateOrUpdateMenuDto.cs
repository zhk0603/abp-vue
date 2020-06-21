using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.MenuManagement
{
    public class CreateOrUpdateMenuDto : EntityDto , IValidatableObject
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ComponentPath { get; set; }
        public string RouterPath { get; set; }
        public Guid? ParentId { get; set; }
        public MenuEnumType MenuType { get; set; }
        public string Icon { get; set; }
        public string Sort { get; set; }
        public string TargetUrl { get; set; } // window.open _blank 
        public string PermissionKey { get; set; } // 此菜单关联的权限key.
        public MultiTenancySides MultiTenancySide { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MenuType == MenuEnumType.Menu)
            {
                if (Name.IsNullOrWhiteSpace())
                {
                    yield return new ValidationResult("Name 不能为空", new[] {"Name"});
                }

                if (DisplayName.IsNullOrWhiteSpace())
                {
                    yield return new ValidationResult("DisplayName 不能为空", new[] { "DisplayName" });
                }

                if (RouterPath.IsNullOrWhiteSpace())
                {
                    yield return new ValidationResult("RouterPath 不能为空", new[] { "RouterPath" });
                }

                if (ComponentPath.IsNullOrWhiteSpace())
                {
                    yield return new ValidationResult("ComponentPath 不能为空", new[] { "ComponentPath" });
                }
            }
            else if (MenuType == MenuEnumType.Permission)
            {
                if (DisplayName.IsNullOrWhiteSpace())
                {
                    yield return new ValidationResult("DisplayName 不能为空", new[] { "DisplayName" });
                }

                if (!ParentId.HasValue)
                {
                    yield return new ValidationResult("ParentId 不能为空", new[] { "ParentId" });
                }
            }
        }
    }
}