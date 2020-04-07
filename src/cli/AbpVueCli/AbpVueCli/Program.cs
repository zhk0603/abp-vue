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
                .UseDefaults()
                .Build();

            args = new string[] {"init"};

            await parser.InvokeAsync(args);

            application.Shutdown();
        }
    }
}
