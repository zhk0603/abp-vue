using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.VueTemplate.Data
{
    /* This is used if database provider does't define
     * IVueTemplateDbSchemaMigrator implementation.
     */
    public class NullVueTemplateDbSchemaMigrator : IVueTemplateDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}