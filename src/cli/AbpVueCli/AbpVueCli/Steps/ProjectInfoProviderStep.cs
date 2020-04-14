using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AbpVueCli.Models;
using Elsa.Results;
using Elsa.Services.Models;

namespace AbpVueCli.Steps
{
    public class ProjectInfoProviderStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            var projectDir = context.GetVariable<string>("ProjectDirectory");

            var configFilePath = Path.Combine(projectDir, "abpvue.json");

            if (!File.Exists(configFilePath))
            {
                throw new FileNotFoundException("abpvue.json 文件没有找到。你可以使用 “abpvue init” 初始化项目 ");
            }

            var configText = await File.ReadAllTextAsync(configFilePath, cancellationToken);
            var projectInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectInfo>(configText);

            if (string.IsNullOrWhiteSpace(projectInfo.OpenApiAddr))
            {
                throw new ArgumentNullException(nameof(projectInfo.OpenApiAddr), "请提供 OpenApi 地址。");
            }

            if (!projectInfo.TemplateFileDirectory.IsNullOrWhiteSpace())
            {
                context.SetVariable("TemplateDirectory", projectInfo.TemplateFileDirectory);
            }

            context.SetVariable("ProjectInfo", projectInfo);

            return Done();
        }
    }
}
