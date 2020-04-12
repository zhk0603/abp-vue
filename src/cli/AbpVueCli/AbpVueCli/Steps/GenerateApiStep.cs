using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AbpVueCli.Generator;
using AbpVueCli.Models;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AbpVueCli.Steps
{
    public class GenerateApiStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            var modelInfo = context.GetVariable<ModuleInfo>("ModuleInfo");

            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var tempDir = Path.Combine(appDir, context.GetVariable<string>("TemplateDirectory"), "Generate", "src",
                "api");
            if (!Directory.Exists(tempDir))
                throw new DirectoryNotFoundException($"Template group directory {tempDir} does not exist.");
            var targetDirectory = Path.Combine(context.GetVariable<string>("ProjectDirectory"), "src", "api");

            await GenerateFile(tempDir, targetDirectory, modelInfo, true);

            return Done();
        }

        private async Task GenerateFile(string groupDirectory, string targetDirectory, object model, bool overwrite)
        {
            foreach (var file in Directory.EnumerateFiles(groupDirectory, "*.sbntxt", SearchOption.AllDirectories))
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
    }
}