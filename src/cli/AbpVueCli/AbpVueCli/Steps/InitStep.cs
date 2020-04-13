using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AbpVueCli.Commands;
using AbpVueCli.Generator;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;
using Scriban;

namespace AbpVueCli.Steps
{
    public class InitStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            var projectDir = context.GetVariable<string>("ProjectDirectory");

            var haveAbpVueJsonFile =
                Directory.EnumerateFiles(projectDir, "abpvue.json", SearchOption.TopDirectoryOnly).Any();

            if (haveAbpVueJsonFile)
            {
                Logger.LogWarning("项目已经初始化，请勿重复执行。");
                return Done();
            }

            var option = context.GetVariable<InitCommandOption>("Option");

            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var tempDir = Path.Combine(appDir, context.GetVariable<string>("TemplateDirectory"), "Init");

            if (!Directory.Exists(tempDir))
                throw new DirectoryNotFoundException($"Template group directory {tempDir} does not exist.");

            await GenerateFiles(tempDir, projectDir, new
            {
                Option = option
            }, false);

            return Done();
        }

        private async Task GenerateFiles(string sourceDirectory, string targetDirectory, object model, bool overwrite)
        {
            foreach (var file in Directory.EnumerateFiles(sourceDirectory, "*.sbntxt", SearchOption.AllDirectories))
            {
                await GenerateFileAsync(sourceDirectory, targetDirectory, file, model, overwrite);
            }
        }
    }
}