using Abp.VueTemplate.MenuManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.MenuManagement.Authorization
{
    public class MenuManagementPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(MenuManagementPermissions.GroupName, L("MenuManagement:MenuManagement"));
            var permission = moduleGroup.AddPermission(MenuManagementPermissions.Menus.Default, L("MenuManagement:MenuManagement"), multiTenancySide: MultiTenancySides.Host);
            permission.AddChild(MenuManagementPermissions.Menus.Create, L("MenuManagement:Create"), multiTenancySide: MultiTenancySides.Host);
            permission.AddChild(MenuManagementPermissions.Menus.Update, L("MenuManagement:Update"), multiTenancySide: MultiTenancySides.Host);
            permission.AddChild(MenuManagementPermissions.Menus.Delete, L("MenuManagement:Delete"), multiTenancySide: MultiTenancySides.Host);
            permission.AddChild(MenuManagementPermissions.Menus.CreatePermission, L("MenuManagement:CreatePermission"), multiTenancySide: MultiTenancySides.Host);
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MenuManagementResource>(name);
        }
    }
}