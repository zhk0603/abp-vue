using System;

namespace AbpVueCli.Commands
{
    public class InfoCommand : CommandBase
    {
        public InfoCommand(IServiceProvider serviceProvider) : base(serviceProvider, "info", "info")
        {
        }
    }
}