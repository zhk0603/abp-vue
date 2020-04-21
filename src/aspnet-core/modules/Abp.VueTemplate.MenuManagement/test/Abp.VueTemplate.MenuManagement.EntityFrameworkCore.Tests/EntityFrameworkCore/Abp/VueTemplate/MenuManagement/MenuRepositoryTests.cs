using System;
using System.Threading.Tasks;
using Abp.VueTemplate.MenuManagement;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Abp.VueTemplate.MenuManagement.EntityFrameworkCore.Abp.VueTemplate.MenuManagement
{
    public class MenuRepositoryTests : MenuManagementEntityFrameworkCoreTestBase
    {
        private readonly IRepository<Menu, Guid> _menuRepository;

        public MenuRepositoryTests()
        {
            _menuRepository = GetRequiredService<IRepository<Menu, Guid>>();
        }

        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
    }
}
