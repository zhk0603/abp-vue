using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AbpVueCli.Generator;
using AbpVueCli.Models;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;

namespace AbpVueCli.Steps
{
    public class GenerateCreateViewStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            var modelInfo = context.GetVariable<ModuleInfo>("ModuleInfo");

            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var tempDir = Path.Combine(appDir, context.GetVariable<string>("TemplateDirectory"), "Generate", "src", "views");
            if (!Directory.Exists(tempDir))
                throw new DirectoryNotFoundException($"Template group directory {tempDir} does not exist.");
            var targetDirectory = Path.Combine(context.GetVariable<string>("ProjectDirectory"), "src", "views");

            ModuleApiOperation postApi = context.GetVariable<ModuleApiOperation>("PostModuleApi");
            var apiSchema = postApi.Operation.RequestBody.Content.First().Value.Schema;

            var model = new GenerateCreateModel
            {
                Name = modelInfo.Name,
                ModuleInfo = modelInfo,
                ProjectInfo = modelInfo.ProjectInfo,
                Properties = apiSchema.Properties
            };

            var overwrite = context.GetVariable<bool>("Overwrite");
            await GenerateFiles(tempDir, targetDirectory, model, overwrite);

            return Done();
        }

        private async Task GenerateFiles(string sourceDirectory, string targetDirectory, object model, bool overwrite)
        {
            foreach (var file in GetTemplateFiles(sourceDirectory))
            {
                await GenerateFileAsync(sourceDirectory, targetDirectory, file, model, overwrite);
            }
        }

        private IEnumerable<string> GetTemplateFiles(string templatePath)
        {
            // 不支持这样搜索： *CreateOrEditForm.vue.sbntxt OR *CreateDialog.vue.sbntxt
            string[] tempFiles = {"*CreateOrEditForm.vue.sbntxt", "*CreateDialog.vue.sbntxt"};

            foreach (var temp in tempFiles)
            {
                yield return Directory
                    .EnumerateFiles(templatePath, temp, SearchOption.AllDirectories).First();
            }
        }
    }
}