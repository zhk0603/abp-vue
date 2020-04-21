using Volo.Abp.Reflection;

namespace Abp.VueTemplate.MenuManagement.Authorization
{
    public class MenuManagementPermissions
    {
        public const string GroupName = "MenuManagement";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(MenuManagementPermissions));
        }
    }
}