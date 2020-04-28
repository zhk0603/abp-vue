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
                .WriteTo.File(Path.Combine(CliPaths.Log, "abp_vue_log.txt"))
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
            //    "-d", @"C:\Workspaces\github\zhk0603\abp-vue\src\vue",
            //    //"--overwrite",
            //    "-s", @"C:\Workspaces\github\zhk0603\abp-vue\src\vue" // VueProject根目录
            //};

            // sy - project
            //args = new string[]
            //{
            //    "generate",
            //    "crud",
            //    "car",
            //    "empty",
            //    "-d", @"C:\Workspaces\github\zhk0603\abp-vue\src\vue",
            //};

            // abp-vue
            args = new string[]
            {
                "generate",
                "crud",
                "menu",
                "/api/menus",
                "-d", @"C:\Workspaces\github\zhk0603\abp-vue\src\vue",
                "-o",
                "--no-permission-control"
            };

            //args = new string[]
            //{
            //    "generate",
            //    "api",
            //    "AbpTenant",
            //    "/api/abp/multi-tenancy/tenants",
            //    "-d", @"C:\Workspaces\github\zhk0603\abp-vue\src\vue",
            //    "-o",
            //    "-f",
            //    "../test-out"
            //};

            //args = new string[]
            //{
            //    "generate",
            //    "model",
            //    "tenant",
            //    "/api/multi-tenancy/tenants",
            //    "-d", @"C:\Workspaces\github\zhk0603\abp-vue\src\vue",
            //    "-o",
            //    "-f",
            //    "../test-out"
            //};

            //args = new string[]
            //{
            //    "generate",
            //    "list",
            //    "role",
            //    "/api/identity/roles",
            //    "-d", @"C:\Workspaces\github\zhk0603\abp-vue\src\vue",
            //     "-o",
            //    "-f",
            //    "../test-out",
            //    // "--no-permission-control"
            //};

            //args = new string[]
            //{
            //    "generate",
            //    "create",
            //    "tenant",
            //    "/api/multi-tenancy/tenants",
            //    "-d", @"C:\Workspaces\github\zhk0603\abp-vue\src\vue",
            //    "-o",
            //    "-f",
            //    "../test-out"
            //};

            //args = new string[]
            //{
            //    "generate",
            //    "edit",
            //    "tenant",
            //    "/api/multi-tenancy/tenants",
            //    "-d", @"C:\Workspaces\github\zhk0603\abp-vue\src\vue",
            //    "-o",
            //    "-f",
            //    "../test-out"
            //};
#endif

            await parser.InvokeAsync(args);

            application.Shutdown();
        }
    }
}
