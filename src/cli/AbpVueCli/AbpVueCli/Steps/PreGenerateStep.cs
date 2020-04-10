using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AbpVueCli.Commands;
using AbpVueCli.Models;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AbpVueCli.Steps
{
    public class PreGenerateStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            var openApiDocument = context.GetVariable<OpenApiDocument>("OpenApiDocument");
            var options = context.GetVariable<GenerateCommandOptionBasic>("Option");

            var moduleApiPathItems =
                (from item in openApiDocument.Paths
                 let haveTag = item.Value.Operations.Any(x => x.Value.Tags
                     .Any(y => y.Name.Equals(options.Module, StringComparison.OrdinalIgnoreCase) ))
                 where haveTag
                 select item).ToList();

            //var moduleApiPathItems =
            //    (from item in openApiDocument.Paths
            //        let match = item.Key.StartsWith(options.ModulePrefix)
            //        where match
            //        select item).ToList();

            if (moduleApiPathItems.Count == 0)
            {
                Logger.LogWarning("找不到模块：{module} 的任何接口。", options.Module);
                return base.Fault("");
            }

            var projectInfo = context.GetVariable<ProjectInfo>("ProjectInfo");
            var modelInfo = new ModuleInfo(moduleApiPathItems)
            {
                Option = options,
                ProjectInfo = projectInfo
            };
            context.SetVariable("ModuleInfo", modelInfo);
            return Done();
        }
    }
}