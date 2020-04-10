using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AbpVueCli.Models;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;
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
            //if (diagnostic.Errors.Count > 0)
            //{
            //    throw new InvalidOperationException();
            //}

            Logger.LogInformation("读取 OpenApi 文档成功。");
            context.SetVariable("OpenApiDocument", openApiDocument);
            return Done();
        }
    }
}
