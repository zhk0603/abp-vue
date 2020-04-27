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
                throw new DirectoryNotFoundException($"模板目录 {tempDir} 不存在。");

            string targetDirectory = modelInfo.Option.OutputFolder.IsNullOrWhiteSpace() ?
                    Path.Combine(context.GetVariable<string>("ProjectDirectory"), "src", "api") : 
                    Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, modelInfo.Option.OutputFolder));
            
            var overwrite = context.GetVariable<bool>("Overwrite");
            await GenerateFiles(tempDir, targetDirectory, modelInfo, overwrite);

            return Done();
        }

        private async Task GenerateFiles(string sourceDirectory, string targetDirectory, object model, bool overwrite)
        {
            string[] whiteFiles = {"ExtensionApi.js.sbntxt", "index.js.sbntxt"};
            foreach (var file in Directory.EnumerateFiles(sourceDirectory, "*.sbntxt", SearchOption.AllDirectories))
            {
                var o = overwrite;
                if (whiteFiles.Any(w => file.EndsWith(w)))
                {
                    o = false;
                }

                await GenerateFileAsync(sourceDirectory, targetDirectory, file, model, o);
            }
        }
    }
}