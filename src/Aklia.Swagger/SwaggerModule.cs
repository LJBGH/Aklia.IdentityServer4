using Aklia.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Aklia.Swagger
{
    public static class SwaggerModule
    {
        private static string _title = string.Empty;
        private static string _version = string.Empty;
        private static string _url = string.Empty;
        public static void AddSwaggerService(this IServiceCollection services)
        {
            _title = Appsettings.app(new[] { "Aklia", "Swagger", "Title" });
            _version = Appsettings.app(new[] { "Aklia", "Swagger", "Version" });
            _url = Appsettings.app(new[] { "Aklia", "Swagger", "Url" });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_version, new OpenApiInfo
                {
                    Title = _title,
                    Version = _version,
                    Description = "基于IdentityServer4的Aklia授权服务中心",
                    Contact = new OpenApiContact
                    {
                        Name = "Aklia.IdentityServer.Application",
                        Email = "1983810978@qq.com",
                        Url = new System.Uri("http://www.identityserver.com.cn/")
                    }
                });

                // 获取xml文件路径
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                // 获取所有xml文件
                var files = Directory.GetFiles(basePath, "*.xml");

                foreach (var item in files)
                {
                    // 添加控制器层注释，true表示显示控制器注释
                    c.IncludeXmlComments(item, true);
                }
                //一定要返回true
                c.DocInclusionPredicate((docName, description) =>
                {
                    return true;
                });

            });
        }

        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app)
        {
            app.UseSwagger(x =>
            {
                x.RouteTemplate = "doc/Aklia.IdentityServer.Application/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(_url, _version);
                c.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}
