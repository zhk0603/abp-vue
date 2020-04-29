using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Abp.VueTemplate.EntityFrameworkCore;
using Abp.VueTemplate.MultiTenancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Abp.VueTemplate
{
    [DependsOn(
        typeof(VueTemplateHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(VueTemplateApplicationModule),
        typeof(VueTemplateEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreSerilogModule)
    )]
    public class VueTemplateHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureConventionalControllers();
            ConfigureAuthentication(context, configuration);
            ConfigureLocalization();
            ConfigureCache(configuration);
            ConfigureVirtualFileSystem(context);
            ConfigureRedis(context, configuration, hostingEnvironment);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);
        }

        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "VueTemplate:"; });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<VueTemplateDomainSharedModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath));
                    options.FileSets.ReplaceEmbeddedByPhysical<VueTemplateDomainModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath));
                    options.FileSets.ReplaceEmbeddedByPhysical<VueTemplateApplicationContractsModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath));
                    options.FileSets.ReplaceEmbeddedByPhysical<VueTemplateApplicationModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath));

                    //options.FileSets.ReplaceEmbeddedByPhysical<VueTemplateDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Abp.VueTemplate.Domain.Shared"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<VueTemplateDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Abp.VueTemplate.Domain"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<VueTemplateApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Abp.VueTemplate.Application.Contracts"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<VueTemplateApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Abp.VueTemplate.Application"));
                });
            }
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(VueTemplateApplicationModule).Assembly);
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "VueTemplate";
                });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "VueTemplate API", Version = "v1"});
                    options.DocInclusionPredicate((docName, description) => true);

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "在下框中输入请求头中需要添加 Jwt Token：Bearer Token",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    });

                    //全局加
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        [
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                        {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                                }
                            ]
                            = new string[] { }
                    });

                    options.OperationFilter<HttpHeaderOperationFilter>();
                    var hostingEnvironment = context.Services.GetHostingEnvironment();
                    if (hostingEnvironment.IsDevelopment())
                    {
                        var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                        options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.Application.xml"));
                        options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.Application.Contracts.xml"));
                        options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.Domain.xml"));
                        options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.Domain.Shared.xml"));
                        options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.HttpApi.xml"));
                        options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.HttpApi.Host.xml"));
                    }

                    //options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.Permission.Application.xml"));
                    //options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.Permission.Application.Contracts.xml"));
                    //options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.Permission.Domain.xml"));
                    //options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.Permission.Domain.Shared.xml"));
                    //options.IncludeXmlComments(Path.Combine(basePath, "Abp.VueTemplate.Permission.HttpApi.xml"));
                });
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureRedis(
            ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment
        )
        {
            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Configuration"];
            });

            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "VueTemplate-Protection-Keys");
            }
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            IdentityModelEventSource.ShowPII = true;
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseAuthorization();
            app.UseAbpRequestLocalization();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "VueTemplate API"); });

            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseMvcWithDefaultRouteAndArea();
        }
    }

    public class HttpHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Policy names map to scopes
            var requiredAuth = RequiredAuthorize(context);

            if (requiredAuth)
            {
                operation.Responses.Add("401", new OpenApiResponse {Description = "Unauthorized"});
                operation.Responses.Add("403", new OpenApiResponse {Description = "Forbidden"});

                var oAuthScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                };

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [oAuthScheme] = new string[] { }
                    }
                };
            }
        }

        private bool RequiredAuthorize(OperationFilterContext context)
        {
            var required = context.MethodInfo
                .GetCustomAttribute<AuthorizeAttribute>(true) != null;

            if (!required && context.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(true) == null)
            {
                required = ((ControllerActionDescriptor) context.ApiDescription.ActionDescriptor)
                    .ControllerTypeInfo
                    .GetCustomAttribute<AuthorizeAttribute>(true) != null;
            }

            return required;
        }
    }
}