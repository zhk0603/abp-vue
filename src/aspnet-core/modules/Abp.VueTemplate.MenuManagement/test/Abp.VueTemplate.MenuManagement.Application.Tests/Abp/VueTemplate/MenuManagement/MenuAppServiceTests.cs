using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Abp.VueTemplate.MenuManagement
{
    public class MenuAppServiceTests : MenuManagementApplicationTestBase
    {
        private readonly IMenuAppService _menuAppService;

        public MenuAppServiceTests()
        {
            _menuAppService = GetRequiredService<IMenuAppService>();
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
