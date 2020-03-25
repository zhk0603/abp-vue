using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.VueTemplate.Navigation
{
    public class Menu : AggregateRoot<Guid>, Volo.Abp.MultiTenancy.IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string ComponentPath { get; set; }
        public virtual string RouterPath { get; set; }
        public virtual Guid? ParentId { get; set; }
        public virtual Menu Parent { get; set; }
        public virtual string Icon { get; set; }
        public virtual string Sort { get; set; }
        public virtual Collection<Menu> Children { get; set; }

        protected Menu()
        {
            
        }

        public Menu(Guid id, string name, string displayName)
        {
            Id = id;
            Name = name;
            DisplayName = displayName;
        }
    }
}
