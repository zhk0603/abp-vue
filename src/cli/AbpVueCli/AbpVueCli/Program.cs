using System;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.IO;
using System.Threading.Tasks;
using AbpVueCli.Commands;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;
using CommandLineBuilder = AbpVueCli.Commands.CommandLineBuilder;

namespace AbpVueCli
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine("logs", "abp_vue_log.txt"))
                .WriteTo.Console()
                .CreateLogger();

            using var application = AbpApplicationFactory.Create<AbpVueCliModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            });

            application.Initialize();

            var parser = new CommandLineBuilder(application.ServiceProvider, "abpvue")
                .AddCommand<InitCommand>()
                .AddCommand<GenerateCommand>()
                .UseDefaults()
                .Build();

#if DEBUG
            //args = new string[]
            //{
            //    "init",
            //    "-o", "https://localhost:44314/swagger/v1/swagger.json",
            //    "-u", "zhaokun",
            //    "-e", "abc@abc.com",
            //    "-d", @"D:\Workspaces\01.github\zhk0603\abp-vue\src\vue\src\views"
            //};

            //args = new string[]
            //{
            //    "generate",
            //    "crud",
            //    "project",
            //    "/api/Project",
            //    "-d", @"D:\Workspaces\02.ty-tfs\sy-project-collection\sy-project\sy-project-ops"
            //};

            args = new string[]
            {
                "generate",
                "crud",
                "tenant",
                "/api/multi-tenancy/tenants",
                "-d", @"D:\Workspaces\01.github\zhk0603\abp-vue\src\vue"
            };
#endif

            await parser.InvokeAsync(args);

            application.Shutdown();
        }
    }
}
