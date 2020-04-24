using System.Threading.Tasks;

namespace Abp.VueTemplate.MenuManagement
{
    public interface IMenuGrantRuntimeCheckerProvider
    {
        Task<MenuGrantResultEnum> CheckAsync(MenuGrantRuntimeCheckerContent context);
    }
}