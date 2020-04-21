using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.Permission
{
    public class Menu : AuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
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

        //public virtual Guid ApplicationId { get; set; }
        //public virtual Application Application { get; set; }
        public virtual string PermissionKey { get; set; } // 此菜单关联的权限key.
        public virtual Collection<Menu> Children { get; set; }

        protected Menu()
        {
        }

        public Menu(Guid id, string name, string displayName, MenuEnumType menuType, Guid? tenantId)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
            MenuType = menuType;
            TenantId = tenantId;

            Children = new Collection<Menu>();
        }
    }
}
