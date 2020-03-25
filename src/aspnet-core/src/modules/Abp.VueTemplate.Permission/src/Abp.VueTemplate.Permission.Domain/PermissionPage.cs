using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.Permission
{
    public class PermissionPage : AuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        public virtual string Key { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual Guid GroupId { get; protected set; }
        public virtual PermissionGroup Group { get; protected set; }
        public virtual string LocalizableKey { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual PermissionPage Parent { get; set; }
        public virtual Collection<PermissionPage> Children { get; set; }
        protected PermissionPage()
        {
        }

        public PermissionPage(Guid id, string key, string name, PermissionGroup group)
        {
            Id = id;
            Key = key;
            Name = name;
            Group = group;
            GroupId = group.Id;
            Children = new Collection<PermissionPage>();
        }

        public virtual PermissionPage AddChildren(Guid id, string key, string name)
        {
            if (Children.Any(x => x.Key == key))
            {
                throw new Volo.Abp.UserFriendlyException("分组名重复");
            }

            var page = new PermissionPage(id, key, name, this.Group);
            page.Parent = this;
            page.ParentId = this.Id;
            Children.Add(page);

            return page;
        }
    }
}
