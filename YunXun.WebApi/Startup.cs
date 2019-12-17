using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using YunXun.WebApi.Unit;
using YunXun.WebApi;
using Microsoft.Extensions.PlatformAbstractions;
using YunXun.Jwt;
using Microsoft.AspNetCore.Mvc;
using YunXun.Dapper.DataFactory;

namespace YunXunWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<DbOption>("DBConnect", Configuration.GetSection("DbOpion"));
            #region  添加SwaggerUI

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "API接口文档",
                    Version = "v1",
                });
                options.IgnoreObsoleteActions();
                options.DocInclusionPredicate((docName, description) => true);
                options.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "YunXun.WebApi.xml"));
                options.DescribeAllEnumsAsStrings();
                options.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });

            #endregion
           // services.AddTransient<IJwt, Jwt>();//Jwt注入
            //注入全局异常捕获
            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(GlobalExceptions));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #region 依赖注入

            var builder = new ContainerBuilder();//实例化容器
            //注册所有模块module
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            //获取所有的程序集
            var assemblys = reflectHelper.GetAllAssemblies().ToArray();
            //注册仓储，所有IRepository接口到Repository的映射
            builder.RegisterAssemblyTypes(assemblys).Where(t => t.Name.EndsWith("Repository") && !t.Name.StartsWith("I")).AsImplementedInterfaces();
            builder.Populate(services);

            return new AutofacServiceProvider(builder.Build()); //第三方IOC接管 core内置DI容器 
            #endregion

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseJwt();
            #region 使用SwaggerUI

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            #endregion
            app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });
        }
    }
}
