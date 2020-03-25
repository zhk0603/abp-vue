using Abp.VueTemplate.Permission.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.VueTemplate.Permission.Authorization
{
    public class PermissionPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(PermissionPermissions.GroupName, L("Permission:Permission"));

            var p = moduleGroup.AddPermission("");
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PermissionResource>(name);
        }
    }
}