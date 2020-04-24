using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuWithGrantedProviders
    {
        public Guid MenuId { get; }

        public bool IsGranted { get; set; }

        public List<MenuProviderInfo> Providers { get; set; }

        public MenuWithGrantedProviders([NotNull] Guid menuId, bool isGranted)
        {
            Check.NotNull(menuId, nameof(menuId));

            MenuId = menuId;
            IsGranted = isGranted;

            Providers = new List<MenuProviderInfo>();
        }
    }
}