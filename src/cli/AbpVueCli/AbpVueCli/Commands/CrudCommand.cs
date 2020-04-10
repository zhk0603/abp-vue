using Microsoft.Extensions.Logging;
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
    public class CrudCommand : CommandBase
    {
        public CrudCommand(IServiceProvider serviceProvider) : base(serviceProvider, "crud", "crud")
        {
            AddArgument(new Argument<string>("module") {Description = "模块名称"});
            AddArgument(new Argument<string>("modulePrefix") {Description = "模块api路径的前缀"});

            AddOption(new Option(new string[] { "-d", "--directory" }, "项目目录。")
            {
                Argument = new Argument<string>()
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
                    .Then<ProjectFinderStep>()
                    .Then<ProjectInfoProviderStep>()
                    .Then<OpenApiDocumentProviderStep>()
                    .Then<PreGenerateStep>()
                    .Then<GenerateApiStep>()
                    .Then<PostApiFinderStep>()
                    .Then<GenerateModelStep>()
                    .Then<GenerateListViewStep>();

                return builder.Build();
            });
        }
    }

    public class GenerateCommandOptionBasic
    {
        public string ModulePrefix { get; set; }
        public string Module { get; set; }
        public string Directory { get; set; }
    }
}