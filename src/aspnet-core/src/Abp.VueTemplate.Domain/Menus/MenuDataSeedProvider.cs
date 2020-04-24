using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.VueTemplate.MenuManagement;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.Menus
{
    public class MenuDataSeedProvider : IMenuDataSeedProvider, ITransientDependency
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ICurrentTenant _currentTenant;

        public MenuDataSeedProvider(
            IMenuRepository menuRepository,
            ICurrentTenant currentTenant
        )
        {
            _menuRepository = menuRepository;
            _currentTenant = currentTenant;
        }

        public List<Menu> Menus => new List<Menu>
        {
            new Menu("UserIndex", "用户管理", MenuEnumType.Menu)
            {
                Sort = "1",
                PermissionKey = "AbpIdentity.Users",
                Children = new Collection<Menu>
                {
                    new Menu("", "创建", MenuEnumType.Permission)
                    {
                        PermissionKey = "AbpIdentity.Users.Create"
                    },
                    new Menu("", "更新", MenuEnumType.Permission)
                    {
                        PermissionKey = "AbpIdentity.Users.Update"
                    },
                    new Menu("", "删除", MenuEnumType.Permission)
                    {
                        PermissionKey = "AbpIdentity.Users.Delete"
                    },
                    new Menu("", "管理权限", MenuEnumType.Permission)
                    {
                        PermissionKey = "AbpIdentity.Users.ManagePermissions"
                    }
                }
            },
            new Menu("RoleIndex", "角色管理", MenuEnumType.Menu)
            {
                Sort = "2",
                PermissionKey = "AbpIdentity.Roles",
                Children = new Collection<Menu>
                {
                    new Menu("", "创建", MenuEnumType.Permission)
                    {
                        PermissionKey = "AbpIdentity.Roles.Create"
                    },
                    new Menu("", "更新", MenuEnumType.Permission)
                    {
                        PermissionKey = "AbpIdentity.Roles.Update"
                    },
                    new Menu("", "删除", MenuEnumType.Permission)
                    {
                        PermissionKey = "AbpIdentity.Roles.Delete"
                    },
                    new Menu("", "管理权限", MenuEnumType.Permission)
                    {
                        PermissionKey = "AbpIdentity.Roles.ManagePermissions"
                    }
                }
            },
            new Menu("TenantIndex", "租户管理", MenuEnumType.Menu, MultiTenancySides.Host)
            {
                Sort = "3",
                PermissionKey = "AbpTenantManagement.Tenants",
                Children = new Collection<Menu>
                {
                    new Menu("", "创建", MenuEnumType.Permission, MultiTenancySides.Host)
                    {
                        PermissionKey = "AbpTenantManagement.Tenants.Create"
                    },
                    new Menu("", "更新", MenuEnumType.Permission, MultiTenancySides.Host)
                    {
                        PermissionKey = "AbpTenantManagement.Tenants.Update"
                    },
                    new Menu("", "删除", MenuEnumType.Permission, MultiTenancySides.Host)
                    {
                        PermissionKey = "AbpTenantManagement.Tenants.Delete"
                    },
                    new Menu("", "管理功能", MenuEnumType.Permission,
                        MultiTenancySides.Host)
                    {
                        PermissionKey = "AbpTenantManagement.Tenants.ManageFeatures"
                    },
                    new Menu("", "管理链接字符串", MenuEnumType.Permission,
                        MultiTenancySides.Host)
                    {
                        PermissionKey = "AbpTenantManagement.Tenants.ManageConnectionStrings"
                    }
                }
            },
            new Menu("MenuIndex", "菜单管理", MenuEnumType.Menu, MultiTenancySides.Host)
            {
                Sort = "4",
                PermissionKey = "MenuManagement.Menus",
                Children = new Collection<Menu>
                {
                    new Menu("", "创建", MenuEnumType.Permission, MultiTenancySides.Host)
                    {
                        PermissionKey = "MenuManagement.Menus.Create"
                    },
                    new Menu("", "更新", MenuEnumType.Permission, MultiTenancySides.Host)
                    {
                        PermissionKey = "MenuManagement.Menus.Update"
                    },
                    new Menu("", "删除", MenuEnumType.Permission, MultiTenancySides.Host)
                    {
                        PermissionKey = "MenuManagement.Menus.Delete"
                    }
                }
            }
        };

        public Task<List<Guid>> GetGrantMenuIdsAsync(DataSeedContext context)
        {
            var query = _menuRepository.AsQueryable();

            if (_currentTenant.GetMultiTenancySide() == MultiTenancySides.Tenant)
            {
                query = query.Where(x => x.MultiTenancySide != MultiTenancySides.Host);
            }

            return Task.FromResult(query.Select(x => x.Id).ToList());
        }
    }
}