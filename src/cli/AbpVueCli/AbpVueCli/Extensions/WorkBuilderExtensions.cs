using Elsa.Activities;
using Elsa.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Elsa.Expressions;

namespace AbpVueCli.Extensions
{
    public static class WorkBuilderExtensions
    {
        public static IActivityBuilder SetStartupDirectoryVariable(this IWorkflowBuilder builder, string directory)
        {
            string startupDirectory = GetStartupDirectory(directory);

            return builder.StartWith<SetVariable>(step =>
            {
                step.VariableName = "StartupDirectory";
                step.ValueExpression = new LiteralExpression(startupDirectory);
            });
        }

        public static IActivityBuilder SetStartupDirectoryVariable(this IActivityBuilder builder, string directory)
        {
            string startupDirectory = GetStartupDirectory(directory);

            return builder.Then<SetVariable>(step =>
            {
                step.VariableName = "StartupDirectory";
                step.ValueExpression = new LiteralExpression(startupDirectory);
            });
        }

        public static IActivityBuilder InitRequiredVariable(this IWorkflowBuilder builder)
        {
            return builder.StartWith<SetVariable>(step =>
            {
                step.VariableName = "TemplateDirectory";
                step.ValueExpression = new LiteralExpression("Templates");
            });
        }

        public static IActivityBuilder InitRequiredVariable(this IActivityBuilder builder)
        {
            return builder.Then<SetVariable>(step =>
            {
                step.VariableName = "TemplateDirectory";
                step.ValueExpression = new LiteralExpression("Templates");
            });
        }

        private static string GetStartupDirectory(string directory)
        {
            if (directory.IsNullOrEmpty())
            {
                directory = Environment.CurrentDirectory;
            }
            else if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException();
            }

            return directory;
        }
    }
}
