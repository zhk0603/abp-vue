using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Threading.Tasks;
using AbpVueCli.Steps;

namespace AbpVueCli.Commands
{
    public class InitCommand : CommandBase
    {
        public InitCommand(IServiceProvider serviceProvider) : base(serviceProvider, "init", "")
        {
            AddOption(new Option(new string[] {"-o", "--openApiAddr"}, "接口地址。")
            {
                Argument = new Argument<string>()
            });

            Handler = CommandHandler.Create((CommandOption optionType) => Run(optionType));
        }

        private async Task Run(CommandOption option)
        {
            Logger.LogDebug(option.OpenApiAddr);

            await RunWorkflow(builder =>
            {
                builder.StartWith<ProjectInfoProviderStep>();

                return builder.Build();
            });
        }

        private class CommandOption
        {
            /// <summary>
            ///     接口地址
            /// </summary>
            public string OpenApiAddr { get; set; }
        }
    }
}
