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
    public class EditCommand : CommandBase
    {
        public EditCommand(IServiceProvider serviceProvider) : base(serviceProvider, "edit", "只生成Edit视图文件")
        {
            AddArgument(new Argument<string>("module") { Description = "模块名称" });
            AddArgument(new Argument<string>("modulePrefix") { Description = "api接口路径的前缀" });

            AddOption(new Option(new string[] { "-d", "--directory" }, "项目目录")
            {
                Argument = new Argument<string>()
            });
            
            AddOption(new Option(new[] { "-f", "--output-folder" }, "指定文件输出的文件夹，支持绝对路径与相对路径，相对路径以当前执行目录为起点。")
            {
                Argument = new Argument<string>()
            });

            AddOption(new Option(new[] { "-o", "--overwrite" }, "指定覆盖现有文件")
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
                        step.ValueExpression =
                            new JavaScriptExpression<GenerateCommandOptionBasic>($"({options.ToJson()})");
                    })
                    .Then<SetVariable>(
                        step =>
                        {
                            step.VariableName = "Overwrite";
                            step.ValueExpression = new JavaScriptExpression<bool>("Option.Overwrite");
                        })
                    .Then<ProjectFinderStep>()
                    .Then<ProjectInfoProviderStep>()
                    .Then<OpenApiDocumentProviderStep>()

                    .Then<PreGenerateStep>()
                    .Then<PostApiFinderStep>()
                    .Then<GenerateEditViewStep>();

                return builder.Build();
            });
        }
    }
}