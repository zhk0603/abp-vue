using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AbpVueCli.Commands;
using AbpVueCli.Generator;
using Elsa.Results;
using Elsa.Services.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Scriban;

namespace AbpVueCli.Steps
{
    public class InitStep : Step
    {
        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context,
            CancellationToken cancellationToken)
        {
            var option = context.GetVariable<InitCommandOption>("Option");
            var projectDir = context.GetVariable<string>("ProjectDirectory");

            var haveAbpVueJsonFile =
                Directory.EnumerateFiles(projectDir, "abpvue.json", SearchOption.TopDirectoryOnly).Any();

            if (haveAbpVueJsonFile && !option.Overwrite)
            {
                Logger.LogWarning("项目已经初始化，请勿重复执行。");
                return Done();
            }

            var tempDir = Path.Combine(context.GetVariable<string>("TemplateDirectory"), "Init");

            if (!Directory.Exists(tempDir))
                throw new DirectoryNotFoundException($"模板目录 {tempDir} 不存在。");


            string templatePath = null;
            if (!option.SaveTemplates.IsNullOrWhiteSpace())
            {
                // 保存模板到指定文件夹。
                templatePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, option.SaveTemplates, "Templates"));
                if (!Directory.Exists(templatePath))
                {
                    var dirInfo = Directory.CreateDirectory(templatePath);
                    templatePath = dirInfo.FullName;
                }

                // 将所有模板文件copy到指定目录。
                await CopyFilesAsync(context.GetVariable<string>("TemplateDirectory"), templatePath, option.Overwrite);
            }

            await GenerateFiles(tempDir, projectDir, new
            {
                Option = option,
                TemplateFileDirectory = templatePath
            }, option.Overwrite);

            await ImportCss(projectDir);

            return Done();
        }

        private static async Task ImportCss(string projectDir)
        {
            var cssFile = Directory.EnumerateFiles(projectDir, "index.scss", SearchOption.AllDirectories).FirstOrDefault();
            if (!cssFile.IsNullOrEmpty())
            {
                var sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(cssFile))
                {
                    bool insert = false;
                    while (!sr.EndOfStream)
                    {
                        var lineText = sr.ReadLine();
                        if (lineText != null)
                        {
                            if (!insert && !lineText.TrimStart().StartsWith("@import"))
                            {
                                insert = true;
                                sb.AppendLine("@import './app-basic.scss';");
                            }

                            sb.AppendLine(lineText);
                        }
                    }
                }

                await File.WriteAllTextAsync(cssFile, sb.ToString());
            }
        }

        private async Task CopyFilesAsync(string sourceDirectory, string targetDirectory, bool overwrite)
        {
            foreach (var file in Directory.EnumerateFiles(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                Logger.LogDebug("复制: {file}", file);
                var targetFilePathName = file.Replace(sourceDirectory, targetDirectory);
                if (File.Exists(targetFilePathName) && !overwrite)
                {
                    Logger.LogInformation("文件 “{targetFilePathName}” 已经存在，跳过复制。",
                        targetFilePathName);
                    return;
                }

                var templateText = await File.ReadAllTextAsync(file);

                var dir = Path.GetDirectoryName(targetFilePathName);
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                await File.WriteAllTextAsync(targetFilePathName, templateText);
                Logger.LogInformation("文件 “{targetFilePathName}” 复制成功。", targetFilePathName);
            }
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