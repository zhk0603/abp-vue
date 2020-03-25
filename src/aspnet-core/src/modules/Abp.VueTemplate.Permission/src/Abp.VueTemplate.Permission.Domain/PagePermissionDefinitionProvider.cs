using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.VueTemplate.Permission.Localization;
using Microsoft.Extensions.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.Threading;

namespace Abp.VueTemplate.Permission
{
    /// <summary>
    /// 将 <see cref="PermissionGroup"/>、<see cref="PermissionPage"/> 附加到 PermissionDefinitionContext 中。
    /// </summary>
    public class PagePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        private readonly IPermissionGroupRepository _groupRepository;
        private readonly IPermissionPageRepository _pageRepository;

        public PagePermissionDefinitionProvider(
            IPermissionGroupRepository groupRepository, 
            IPermissionPageRepository pageRepository)
        {
            _groupRepository = groupRepository;
            _pageRepository = pageRepository;
        }

        public override void Define(IPermissionDefinitionContext context)
        {
            AsyncHelper.RunSync(() => DefineAsync(context));
        }

        protected virtual async Task DefineAsync(IPermissionDefinitionContext context)
        {
            var groups = await _groupRepository.GetListAsync(false);
            var permissions = await _pageRepository.GetListAsync(false);

            foreach (var group in groups)
            {
                AddGroup(context, group, permissions);
            }
        }

        protected virtual void AddGroup(IPermissionDefinitionContext context, PermissionGroup group, List<PermissionPage> permissions)
        {
            var groupDefinition = context.GetGroupOrNull(group.Key);
            if (groupDefinition == null)
            {
                groupDefinition = context.AddGroup(group.Key,L(group.Name));
            }

            var groupPermissions = permissions.Where(x => x.GroupId == group.Id && x.ParentId == null);
            foreach (var permission in groupPermissions)
            {
                var permissionDefinition = groupDefinition.Permissions.SingleOrDefault(x => x.Name == permission.Key);

                if (permissionDefinition == null)
                {
                    permissionDefinition = groupDefinition.AddPermission(permission.Key, L(permission.Name));
                }

                AddPermissionRecursively(permissionDefinition, permission, permissions);
            }
        }

        protected virtual void AddPermissionRecursively(PermissionDefinition permissionDefinition, PermissionPage parentPermission, List<PermissionPage> permissions)
        {
            var groupPermissions = permissions.Where(x => x.GroupId == parentPermission.GroupId && x.ParentId == parentPermission.Id);
            foreach(var permission in groupPermissions)
            {
                var childPermissionDefinition =
                    permissionDefinition.Children.SingleOrDefault(x => x.Name == permission.Key);

                if (childPermissionDefinition == null)
                {
                    childPermissionDefinition = permissionDefinition.AddChild(permission.Key, L(permission.Name));
                }

                AddPermissionRecursively(childPermissionDefinition, permission, permissions);
            }
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PermissionResource>(name);
        }
    }
}
