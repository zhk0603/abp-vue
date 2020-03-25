using Volo.Abp.Reflection;

namespace Abp.VueTemplate.Permission.Authorization
{
    public class PermissionPermissions
    {
        public const string GroupName = "Permission";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(PermissionPermissions));
        }
    }
}