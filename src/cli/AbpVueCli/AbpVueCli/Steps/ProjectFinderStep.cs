using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Expressions;
using Elsa.Results;
using Elsa.Scripting.JavaScript;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;

namespace AbpVueCli.Steps
{
    public class ProjectFinderStep : Step
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
}
