using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AbpVueCli.Commands;
using AbpVueCli.Generator;
using Elsa.Expressions;
using Elsa.Results;
using Elsa.Scripting.JavaScript;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;
using Scriban;

namespace AbpVueCli.Steps
{
    public class ProjectInfoProviderStep : Step
    {
        protected override Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            var startupDirectory = context.GetVariable<string>("StartupDirectory");

            Logger.LogInformation("启动目录：{dir}", startupDirectory);

            var isValidProject = CheckIsValidProject(startupDirectory);

            if (!isValidProject)
            {
                var srcPath = @"\src\";
                var srcPathIndex = startupDirectory.LastIndexOf(srcPath, StringComparison.Ordinal);
                if (srcPathIndex == -1)
                {
                    throw new DirectoryNotFoundException($"未存在 “src” 目录，目录： {Environment.CurrentDirectory}");
                }

                startupDirectory = startupDirectory.Substring(0, srcPathIndex);

                isValidProject = CheckIsValidProject(startupDirectory);
            }

            if (!isValidProject)
            {
                throw new NotSupportedException($"未知的项目结构，目录： {startupDirectory}");
            }

            Logger.LogInformation("项目根目录：{dir}", startupDirectory);
            context.SetVariable("ProjectDirectory", startupDirectory);
            
            return Task.FromResult(Done());
        }

        protected virtual bool CheckIsValidProject(string directory)
        {
            var havePackageFile =
                Directory.EnumerateFiles(directory, "package.json", SearchOption.TopDirectoryOnly).Any();

            var haveSrcDir =
                Directory.EnumerateDirectories(directory, "src", SearchOption.TopDirectoryOnly).Any();

            return havePackageFile && haveSrcDir;
        }
    }

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

            if (!Directory.Exists(tempDir)) throw new DirectoryNotFoundException($"Template group directory {tempDir} does not exist.");

            await GenerateFile(tempDir, projectDir, new 
            {
                Option = option
            }, false);

            return Done();
        }

        private async Task GenerateFile(string groupDirectory, string targetDirectory, object model, bool overwrite)
        {
            foreach (var file in Directory.EnumerateFiles(groupDirectory, "*.sbntxt", SearchOption.AllDirectories))
            {
                Logger.LogDebug($"Generating using template file: {file}");
                var targetFilePathNameTemplate = file.Replace(groupDirectory, targetDirectory);
                var targetFilePathName = targetFilePathNameTemplate.RemovePostFix(".sbntxt");
                if (File.Exists(targetFilePathName) && !overwrite)
                {
                    Logger.LogInformation("File “{targetFilePathName}” already exists, skip generating.",
                        targetFilePathName);
                    continue;
                }

                var templateText = await File.ReadAllTextAsync(file);
                var contents = TextGenerator.GenerateByTemplateText(templateText, model, out TemplateContext context);

                var dir = Path.GetDirectoryName(targetFilePathName);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                await File.WriteAllTextAsync(targetFilePathName, contents);
                Logger.LogInformation("File “{targetFilePathName}” successfully generated.", targetFilePathName);
            }
        }
    }
}
