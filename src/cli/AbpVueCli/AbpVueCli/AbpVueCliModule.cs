using System;
using System.Collections.Generic;
using System.Text;
using AbpVueCli.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpVueCli
{
    [DependsOn(typeof(AbpAutofacModule))]
    public class AbpVueCliModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services
                .AddElsa()
                .AddAllActivities();
        }
    }
}
