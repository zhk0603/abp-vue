using System;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AbpVueCli.Generator;
using Elsa.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AbpVueCli.Steps
{
    public abstract class Step : Activity
    {
        protected Step()
        {
            Logger = NullLogger<Step>.Instance;
        }

        public ILogger<Step> Logger { get; set; }

        protected void LogInput<TParameter>(Expression<Func<TParameter>> parameterExpression,
            object? customValue = null)
        {
            LogParameter("Input", parameterExpression, customValue);
        }

        protected void LogOutput<TParameter>(Expression<Func<TParameter>> parameterExpression,
            object? customValue = null)
        {
            LogParameter("Output", parameterExpression, customValue);
        }

        private void LogParameter<TParameter>(string parameterType, Expression<Func<TParameter>> parameterExpression,
            object? customValue = null)
        {
            var memberExpr = (MemberExpression) parameterExpression.Body;
            var value = customValue ?? $"'{parameterExpression.Compile().Invoke()}'";
            Logger.LogDebug($"{Type} {parameterType} [{memberExpr.Member.Name}]: {value}");
        }

        protected async Task GenerateFileAsync(string sourceDirectory, string targetDirectory, string file, object model,
            bool overwrite)
        {
            Logger.LogDebug("使用模板文件生成: {file}", file);
            var targetFilePathNameTemplate = file.Replace(sourceDirectory, targetDirectory);
            var targetFilePathName = TextGenerator.GenerateByTemplateText(targetFilePathNameTemplate, model)
                .RemovePostFix(".sbntxt");
            if (File.Exists(targetFilePathName) && !overwrite)
            {
                Logger.LogInformation("文件 “{targetFilePathName}” 已经存在，跳过生成。",
                    targetFilePathName);
                return;
            }

            var templateText = await File.ReadAllTextAsync(file);
            var contents = TextGenerator.GenerateByTemplateText(templateText, model);

            var dir = Path.GetDirectoryName(targetFilePathName);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            await File.WriteAllTextAsync(targetFilePathName, contents);
            Logger.LogInformation("文件 “{targetFilePathName}” 成功生成。", targetFilePathName);
        }
    }
}