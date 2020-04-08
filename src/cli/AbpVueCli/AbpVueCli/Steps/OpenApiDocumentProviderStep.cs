using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AbpVueCli.Commands;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace AbpVueCli.Steps
{
    public class OpenApiDocumentProviderStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient();

            var projectInfo = context.GetVariable<ProjectInfo>("ProjectInfo");

            Logger.LogInformation("正在获取 OpenApi 文档，url:{url}。", projectInfo.OpenApiAddr);
            var responseStream = await httpClient.GetStreamAsync(projectInfo.OpenApiAddr);
            var openApiDocument = new OpenApiStreamReader().Read(responseStream, out OpenApiDiagnostic diagnostic);
            if (diagnostic.Errors.Count > 0)
            {
                throw new InvalidOperationException();
            }

            Logger.LogInformation("读取 OpenApi 文档成功。");
            context.SetVariable("OpenApiDocument", openApiDocument);
            return Done();
        }
    }

    public class PreGenerateStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            var openApiDocument = context.GetVariable<OpenApiDocument>("OpenApiDocument");
            var options = context.GetVariable<GenerateCommandOptionBasic>("Option");
            var moduleApiPathItems =
                (from item in openApiDocument.Paths
                    let haveTag = item.Value.Operations.Any(x => x.Value.Tags.Any(y => y.Name == options.Module))
                    where haveTag
                    select item).ToList();
            
            if(moduleApiPathItems.Count == 0)
            {
                Logger.LogWarning("找不到模块：{module} 的任何接口。", options.Module);
                return base.Fault("");
            }

            var modelInfo = new ModuleInfo(moduleApiPathItems);
            modelInfo.Validate();
            context.SetVariable("ModuleInfo", modelInfo);

            return Done();
        }
    }

    public class ModuleInfo
    {
        private readonly IEnumerable<KeyValuePair<string, OpenApiPathItem>> _pairs;

        public ModuleInfo(IEnumerable<KeyValuePair<string,OpenApiPathItem>> pairs)
        {
            _pairs = pairs;
        }

        public void Validate()
        {
            throw new NotImplementedException();
        }
    }

    public class GenerateStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            return Done();
        }
    }
}
