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

            await GenerateFile(tempDir, targetDirectory, model, true);

            return Done();
        }

        private async Task GenerateFile(string groupDirectory, string targetDirectory, object model, bool overwrite)
        {
            foreach (var file in GetTemplateFiles(groupDirectory))
            {
                Logger.LogDebug("Generating using template file: {file}", file);
                var targetFilePathNameTemplate = file.Replace(groupDirectory, targetDirectory);
                var targetFilePathName = TextGenerator.GenerateByTemplateText(targetFilePathNameTemplate, model).RemovePostFix(".sbntxt");
                if (File.Exists(targetFilePathName) && !overwrite)
                {
                    Logger.LogInformation("File “{targetFilePathName}” already exists, skip generating.",
                        targetFilePathName);
                    continue;
                }

                var templateText = await File.ReadAllTextAsync(file);
                var contents = TextGenerator.GenerateByTemplateText(templateText, model);

                var dir = Path.GetDirectoryName(targetFilePathName);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                await File.WriteAllTextAsync(targetFilePathName, contents);
                Logger.LogInformation("File “{targetFilePathName}” successfully generated.", targetFilePathName);
            }
        }

        private IEnumerable<string> GetTemplateFiles(string templatePath)
        {
            // 不支持这样搜索： *CreateOrEditForm.vue.sbntxt OR *CreateDialog.vue.sbntxt

            yield return Directory
                .EnumerateFiles(templatePath, "*CreateOrEditForm.vue.sbntxt", SearchOption.AllDirectories).First();

            yield return Directory.EnumerateFiles(templatePath, "*CreateDialog.vue.sbntxt", SearchOption.AllDirectories)
                .First();
        }
    }
}