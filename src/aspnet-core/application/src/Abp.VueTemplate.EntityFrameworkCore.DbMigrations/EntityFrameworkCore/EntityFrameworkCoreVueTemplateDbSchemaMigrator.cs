using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Abp.VueTemplate.Data;
using Volo.Abp.DependencyInjection;

namespace Abp.VueTemplate.EntityFrameworkCore
{
    public class EntityFrameworkCoreVueTemplateDbSchemaMigrator
        : IVueTemplateDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreVueTemplateDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the VueTemplateMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<VueTemplateMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}