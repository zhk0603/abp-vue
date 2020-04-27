using Microsoft.Extensions.Logging;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using AbpVueCli.Extensions;
using AbpVueCli.Steps;
using AbpVueCli.Utils;
using Elsa;
using Elsa.Activities;
using Elsa.Activities.ControlFlow.Activities;
using Elsa.Scripting.JavaScript;

namespace AbpVueCli.Commands
{
    public class CrudCommand : CommandBase
    {
        public CrudCommand(IServiceProvider serviceProvider) : base(serviceProvider, "crud", "根据指定的模块生成CRUD的相关文件")
        {
            AddArgument(new Argument<string>("module") {Description = "模块名称"});
            AddArgument(new Argument<string>("modulePrefix") {Description = "模块api路径的前缀"});

            AddOption(new Option(new string[] { "-d", "--directory" }, "项目目录。")
            {
                Argument = new Argument<string>()
            });

            AddOption(new Option(new[] { "-o", "--overwrite" }, "指定覆盖现有文件")
            {
                Argument = new Argument<bool>()
            });

            AddOption(new Option(new[] { "--no-permission-control" }, "不生成权限控制。")
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
                    .Then<IfElse>(
                        step => step.ConditionExpression =
                            new JavaScriptExpression<bool>("EmptyModule"),
                        ifElse =>
                        {
                            ifElse.When(OutcomeNames.False)
                                .Then<GenerateApiStep>()

                                .Then<PostApiFinderStep>()
                                .Then<GenerateModelStep>()
                                .Then<GenerateCreateViewStep>()
                                .Then<GenerateEditViewStep>()

                                .Then<GetListApiFinderStep>()
                                .Then<GenerateListViewStep>();

                            ifElse.When(OutcomeNames.True)
                                .Then<GenerateEmptyModuleStep>();
                        }
                    );

                    //.Then<GenerateApiStep>()

                    //.Then<PostApiFinderStep>()
                    //.Then<GenerateModelStep>()
                    //.Then<GenerateCreateViewStep>()
                    //.Then<GenerateEditViewStep>()

                    //.Then<GetListApiFinderStep>()
                    //.Then<GenerateListViewStep>();

                return builder.Build();
            });
        }
    }
}