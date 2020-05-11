using System.Threading.Tasks;


namespace Abp.VueTemplate.Data
{
    public interface IVueTemplateDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
