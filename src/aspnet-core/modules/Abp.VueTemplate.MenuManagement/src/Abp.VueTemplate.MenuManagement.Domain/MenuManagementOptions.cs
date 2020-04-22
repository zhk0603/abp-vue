using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Collections;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuManagementOptions
    {
        public ITypeList<IMenuManagementProvider> ManagementProviders { get; }

        public MenuManagementOptions()
        {
            ManagementProviders = new TypeList<IMenuManagementProvider>();
        }
    }
}
