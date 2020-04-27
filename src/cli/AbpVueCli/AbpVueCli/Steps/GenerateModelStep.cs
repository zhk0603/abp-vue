using System;
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
    public class GenerateModelStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            var moduleInfo = context.GetVariable<ModuleInfo>("ModuleInfo");

            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var tempDir = Path.Combine(appDir, context.GetVariable<string>("TemplateDirectory"), "Generate", "src", "views");
            if (!Directory.Exists(tempDir))
                throw new DirectoryNotFoundException($"模板目录 {tempDir} 不存在。");

            string targetDirectory = moduleInfo.Option.OutputFolder.IsNullOrWhiteSpace()
                ? Path.Combine(context.GetVariable<string>("ProjectDirectory"), "src", "views")
                : Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, moduleInfo.Option.OutputFolder));

            ModuleApiOperation postApi = context.GetVariable<ModuleApiOperation>("PostModuleApi");
            var apiSchema = postApi.Operation.RequestBody.Content.First().Value.Schema;
            
            var model = new GenerateModelModel
            {
                Name = moduleInfo.Name,
                ModuleInfo = moduleInfo,
                RequestBodySchema = apiSchema
            };

            var overwrite = context.GetVariable<bool>("Overwrite");
            await GenerateFiles(tempDir, targetDirectory, model, overwrite);

            return Done();
        }

        private async Task GenerateFiles(string sourceDirectory, string targetDirectory, object model, bool overwrite)
        {
            foreach (var file in Directory.EnumerateFiles(sourceDirectory, "*Config.js.sbntxt", SearchOption.AllDirectories))
            {
                await GenerateFileAsync(sourceDirectory, targetDirectory, file, model, overwrite);
            }
        }
    }
}