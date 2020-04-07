using System;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;

namespace AbpVueCli.Steps
{
    public class ProjectInfoProviderStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            var appCurDir = Environment.CurrentDirectory;
            var srcPath = @"\src\";
            var srcPathIndex = appCurDir.IndexOf(srcPath, StringComparison.Ordinal);
            if (srcPathIndex == -1)
            {
                throw new NotSupportedException($"未存在 “src” 目录，目录： {Environment.CurrentDirectory}");
            }

            var projectRootPath = appCurDir.Substring(0, srcPathIndex) + srcPath;
            Logger.LogDebug("项目根目录：{dir}", projectRootPath);



            return Done();
        }
    }
}
