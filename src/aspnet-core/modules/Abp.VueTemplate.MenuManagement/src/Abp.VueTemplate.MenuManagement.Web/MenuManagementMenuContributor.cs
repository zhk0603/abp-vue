using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Abp.VueTemplate.MenuManagement.Localization;
using Volo.Abp.UI.Navigation;

namespace Abp.VueTemplate.MenuManagement.Web
{
    public class MenuManagementMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenu(context);
            }
        }

        private Task ConfigureMainMenu(MenuConfigurationContext context)
        {
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<MenuManagementResource>>();            //Add main menu items.

            context.Menu.AddItem(
                new ApplicationMenuItem("Menu", l["Menu:Menu"], "/Abp/VueTemplate/MenuManagement/Menu")
            );
            return Task.CompletedTask;
        }
    }
}
