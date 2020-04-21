using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.MenuManagement
{
    public class Permission : AuditedEntity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        public virtual string Key { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual Guid GroupId { get; protected set; }
        public virtual PermissionGroup Group { get; protected set; }
        public virtual Guid? ParentId { get; set; }
        public virtual Permission Parent { get; set; }
        public virtual Collection<Permission> Children { get; set; }

        protected Permission()
        {
        }

        public Permission(Guid id, string key, string name, Guid groupId)
        {
            Id = id;
            Key = key;
            Name = name;
            GroupId = groupId;
            Children = new Collection<Permission>();
        }

        public virtual Permission AddChildren(Guid id, string key, string name)
        {
            if (Children.Any(x => x.Key == key))
            {
                throw new Volo.Abp.UserFriendlyException("权限名重复");
            }

            var page = new Permission(id, key, name, this.GroupId);
            page.Parent = this;
            page.ParentId = this.Id;
            Children.Add(page);

            return page;
        }
    }
}
