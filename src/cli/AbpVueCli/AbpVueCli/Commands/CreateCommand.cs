using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using AbpVueCli.Extensions;
using AbpVueCli.Steps;
using AbpVueCli.Utils;
using Elsa.Activities;
using Elsa.Scripting.JavaScript;

namespace AbpVueCli.Commands
{
    public class CreateCommand : CommandBase
    {
        public CreateCommand(IServiceProvider serviceProvider) : base(serviceProvider, "create", "只生成Create视图文件")
        {
            AddArgument(new Argument<string>("module") { Description = "模块名称" });
            AddArgument(new Argument<string>("modulePrefix") { Description = "模块api路径的前缀" });

            AddOption(new Option(new string[] { "-d", "--directory" }, "项目目录。")
            {
                Argument = new Argument<string>()
            });

            AddOption(new Option(new[] { "--no-overwrite" }, "指定不覆盖现有文件")
            {
                Argument = new Argument<bool>()
            });

            Handler = CommandHandler.Create((GenerateCommandOptionBasic options) => Run(options));
        }

        private async Task Run(GenerateCommandOptionBasic options)
        {
            await RunWorkflow(builder =>
            {
                builder
                    .SetStartupDirectoryVariable(options.Directory)
                    .InitRequiredVariable()
                    .Then<SetVariable>(step =>
                    {
                        step.VariableName = "Option";
                        step.ValueExpression = new JavaScriptExpression<GenerateCommandOptionBasic>($"({options.ToJson()})");
                    })
                    .Then<SetVariable>(
                        step =>
                        {
                            step.VariableName = "Overwrite";
                            step.ValueExpression = new JavaScriptExpression<bool>("!Option.NoOverwrite");
                        })
                    .Then<ProjectFinderStep>()
                    .Then<ProjectInfoProviderStep>()
                    .Then<OpenApiDocumentProviderStep>()

                    .Then<PreGenerateStep>()
                    .Then<PostApiFinderStep>()
                    .Then<GenerateCreateViewStep>();

                return builder.Build();
            });
        }

    }
}