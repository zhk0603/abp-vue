using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Abp.VueTemplate.Permission
{
    public class MenuGrant : Entity<Guid>, Volo.Abp.MultiTenancy.IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        [NotNull]
        public virtual Guid MenuId { get; protected set; }

        [NotNull]
        public virtual string ProviderName { get; protected set; }

        [CanBeNull]
        public virtual string ProviderKey { get; protected internal set; }

        protected MenuGrant()
        {
        }

        public MenuGrant(
            Guid id,
            [NotNull] Guid menuId,
            [NotNull] string providerName,
            [CanBeNull] string providerKey,
            Guid? tenantId = null)
        {
            Check.NotNull(menuId, nameof(menuId));

            Id = id;
            MenuId = menuId;
            ProviderName = Check.NotNullOrWhiteSpace(providerName, nameof(providerName));
            ProviderKey = providerKey;
            TenantId = tenantId;
        }
    }
}
