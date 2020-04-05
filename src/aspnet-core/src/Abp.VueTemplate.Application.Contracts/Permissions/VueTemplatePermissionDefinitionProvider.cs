using Abp.VueTemplate.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.VueTemplate.Permissions
{
    public class VueTemplatePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(VueTemplatePermissions.GroupName);
            //Define your own permissions here. Example:
            // myGroup.AddPermission("", L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<VueTemplateResource>(name);
        }
    }
}
