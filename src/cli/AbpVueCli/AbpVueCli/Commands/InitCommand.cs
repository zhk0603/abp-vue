using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text;
using System.Threading.Tasks;
using AbpVueCli.Extensions;
using AbpVueCli.Steps;
using AbpVueCli.Utils;
using Elsa.Activities;
using Elsa.Expressions;
using Elsa.Scripting.JavaScript;

namespace AbpVueCli.Commands
{
    public class InitCommand : CommandBase
    {
        public InitCommand(IServiceProvider serviceProvider) : base(serviceProvider, "init", "初始化项目，添加AbpVue运行时需要的文件")
        {
            AddOption(new Option(new string[] {"-o", "--openApiAddr"}, "接口地址。")
            {
                Argument = new Argument<string>()
            });

            AddOption(new Option(new string[] { "-u", "--userName" }, "用户名。")
            {
                Argument = new Argument<string>()
            });

            AddOption(new Option(new string[] { "-e", "--email" }, "邮箱。")
            {
                Argument = new Argument<string>()
            });

            AddOption(new Option(new string[] { "-d", "--directory" }, "项目目录。")
            {
                Argument = new Argument<string>()
            });

            Handler = CommandHandler.Create((InitCommandOption optionType) => Run(optionType));
        }

        private async Task Run(InitCommandOption option)
        {
            await RunWorkflow(builder =>
            {
                builder
                    .SetStartupDirectoryVariable(option.Directory)
                    .InitRequiredVariable()
                    .Then<SetVariable>(step =>
                    {
                        step.VariableName = "Option";
                        step.ValueExpression = new JavaScriptExpression<InitCommandOption>($"({option.ToJson()})");
                    })
                    .Then<ProjectFinderStep>()
                    .Then<InitStep>();

                return builder.Build();
            });
        }
    }

    public class InitCommandOption
    {
        /// <summary>
        ///     接口地址
        /// </summary>
        public string OpenApiAddr { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Directory { get; set; }
    }
}
