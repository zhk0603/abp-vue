using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.VueTemplate.MenuManagement;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Abp.VueTemplate.Menus
{
    public class MenuDataSeedProvider : IMenuDataSeedProvider, ITransientDependency
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;

        public MenuDataSeedProvider(
            IMenuRepository menuRepository,
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator
        )
        {
            _menuRepository = menuRepository;
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
        }

        public List<Menu> Menus => new List<Menu>
        {
            new Menu(_guidGenerator.Create(), "SystemManager", "系统管理", MenuEnumType.Menu)
            {
                Sort = "1",
                RouterPath = "/systems",
                ComponentPath = "Layout",
                Children = new Collection<Menu>
                {
                    new Menu(_guidGenerator.Create(), "UserIndex", "用户管理", MenuEnumType.Menu)
                    {
                        Sort = "1",
                        PermissionKey = "AbpIdentity.Users",
                        RouterPath = "users",
                        ComponentPath = "users/index",
                        Children = new Collection<Menu>
                        {
                            new Menu(_guidGenerator.Create(), "", "创建", MenuEnumType.Permission)
                            {
                                PermissionKey = "AbpIdentity.Users.Create"
                            },
                            new Menu(_guidGenerator.Create(), "", "更新", MenuEnumType.Permission)
                            {
                                PermissionKey = "AbpIdentity.Users.Update"
                            },
                            new Menu(_guidGenerator.Create(), "", "删除", MenuEnumType.Permission)
                            {
                                PermissionKey = "AbpIdentity.Users.Delete"
                            },
                            new Menu(_guidGenerator.Create(), "", "管理权限", MenuEnumType.Permission)
                            {
                                PermissionKey = "AbpIdentity.Users.ManagePermissions"
                            }
                        }
                    },
                    new Menu(_guidGenerator.Create(), "RoleIndex", "角色管理", MenuEnumType.Menu)
                    {
                        Sort = "2",
                        PermissionKey = "AbpIdentity.Roles",
                        RouterPath = "roles",
                        ComponentPath = "role/index",
                        Children = new Collection<Menu>
                        {
                            new Menu(_guidGenerator.Create(), "", "创建", MenuEnumType.Permission)
                            {
                                PermissionKey = "AbpIdentity.Roles.Create"
                            },
                            new Menu(_guidGenerator.Create(), "", "更新", MenuEnumType.Permission)
                            {
                                PermissionKey = "AbpIdentity.Roles.Update"
                            },
                            new Menu(_guidGenerator.Create(), "", "删除", MenuEnumType.Permission)
                            {
                                PermissionKey = "AbpIdentity.Roles.Delete"
                            },
                            new Menu(_guidGenerator.Create(), "", "管理权限", MenuEnumType.Permission)
                            {
                                PermissionKey = "AbpIdentity.Roles.ManagePermissions"
                            }
                        }
                    },
                    new Menu(_guidGenerator.Create(), "TenantIndex", "租户管理", MenuEnumType.Menu, MultiTenancySides.Host)
                    {
                        Sort = "3",
                        PermissionKey = "AbpTenantManagement.Tenants",
                        RouterPath = "tenants",
                        ComponentPath = "tenant/index",
                        Children = new Collection<Menu>
                        {
                            new Menu(_guidGenerator.Create(), "", "创建", MenuEnumType.Permission, MultiTenancySides.Host)
                            {
                                PermissionKey = "AbpTenantManagement.Tenants.Create"
                            },
                            new Menu(_guidGenerator.Create(), "", "更新", MenuEnumType.Permission, MultiTenancySides.Host)
                            {
                                PermissionKey = "AbpTenantManagement.Tenants.Update"
                            },
                            new Menu(_guidGenerator.Create(), "", "删除", MenuEnumType.Permission, MultiTenancySides.Host)
                            {
                                PermissionKey = "AbpTenantManagement.Tenants.Delete"
                            },
                            new Menu(_guidGenerator.Create(), "", "管理功能", MenuEnumType.Permission,
                                MultiTenancySides.Host)
                            {
                                PermissionKey = "AbpTenantManagement.Tenants.ManageFeatures"
                            },
                            new Menu(_guidGenerator.Create(), "", "管理链接字符串", MenuEnumType.Permission,
                                MultiTenancySides.Host)
                            {
                                PermissionKey = "AbpTenantManagement.Tenants.ManageConnectionStrings"
                            }
                        }
                    },
                    new Menu(_guidGenerator.Create(), "MenuIndex", "菜单管理", MenuEnumType.Menu, MultiTenancySides.Host)
                    {
                        Sort = "4",
                        PermissionKey = "MenuManagement.Menus",
                        RouterPath = "menus",
                        ComponentPath = "menu/index",
                        Children = new Collection<Menu>
                        {
                            new Menu(_guidGenerator.Create(), "", "创建", MenuEnumType.Permission, MultiTenancySides.Host)
                            {
                                PermissionKey = "MenuManagement.Menus.Create"
                            },
                            new Menu(_guidGenerator.Create(), "", "创建权限", MenuEnumType.Permission, MultiTenancySides.Host)
                            {
                                PermissionKey = "MenuManagement.Menus.CreatePermission"
                            },
                            new Menu(_guidGenerator.Create(), "", "更新", MenuEnumType.Permission, MultiTenancySides.Host)
                            {
                                PermissionKey = "MenuManagement.Menus.Update"
                            },
                            new Menu(_guidGenerator.Create(), "", "删除", MenuEnumType.Permission, MultiTenancySides.Host)
                            {
                                PermissionKey = "MenuManagement.Menus.Delete"
                            }
                        }
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