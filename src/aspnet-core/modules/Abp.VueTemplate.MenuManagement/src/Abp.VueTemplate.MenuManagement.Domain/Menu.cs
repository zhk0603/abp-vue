using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.MenuManagement
{
    public class Menu : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; protected set; }
        public virtual string DisplayName { get; protected set; }
        public virtual MenuEnumType MenuType { get; protected set; }
        public virtual string ComponentPath { get; set; }
        public virtual string RouterPath { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual Menu Parent { get; set; }
        public virtual string Icon { get; set; }
        public virtual string Sort { get; set; }
        public virtual string TargetUrl { get; set; } // window.open _blank 
        public virtual string PermissionKey { get; set; } // 此菜单关联的权限key.
        public virtual MultiTenancySides MultiTenancySide { get; set; }
        public virtual Collection<Menu> Children { get; set; }

        protected Menu()
        {
        }

        public Menu(
            string name,
            string displayName,
            MenuEnumType menuType,
            MultiTenancySides multiTenancySide = MultiTenancySides.Both
        )
        {
            Name = name;
            DisplayName = displayName;
            MenuType = menuType;
            MultiTenancySide = multiTenancySide;

            Children = new Collection<Menu>();
        }
    }
}
