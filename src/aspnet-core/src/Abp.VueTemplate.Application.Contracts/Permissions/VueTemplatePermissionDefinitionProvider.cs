using Abp.VueTemplate.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.Permissions
{
    public class VueTemplatePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(VueTemplatePermissions.GroupName);
            //Define your own permissions here. Example:
            // myGroup.AddPermission("", L("Permission:MyPermission1"));

            var roleGroup = context.GetGroup(IdentityPermissions.GroupName);
            var rolesPermission = roleGroup.GetPermissionOrNull(IdentityPermissions.Roles.Default);
            rolesPermission.AddChild("AbpIdentity.Roles.Custom", L("Custom"),
                MultiTenancySides.Tenant);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<VueTemplateResource>(name);
        }
    }
}
