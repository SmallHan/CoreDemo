using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Services;
using CoreDemo.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreDemo
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //注册MVC 相关的服务到容器里面
            services.AddMvc();
            services.AddSingleton<ICinemaService, CinemaMemoryService>();
            services.AddSingleton<IMovieService, ModelMemoryService>();
            services.Configure<ConnectionOptions>(_configuration.GetSection("ConnectionStrings"));
        }

        //管道，中间件  IApplicationBuilder
        //环境变量  IHostingEnvironment
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //直接显示错误在页面上 app.UseStatusCodePages();
            //使用静态文件
            app.UseStaticFiles();
            //使用mvc中间件
            app.UseMvc(routes=> {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.Use(async (context,next) =>
            //{
            //    logger.LogInformation("M1 Start");
            //    await context.Response.WriteAsync("Hello World!");
            //    //调用下一个中间件
            //    await next();
            //    logger.LogInformation("M1 End");
            //});

            //app.Run(async (context) =>
            //{
            //    logger.LogInformation("M2 Start");
            //    await context.Response.WriteAsync("Another World!");
            //    logger.LogInformation("M2 End");
            //});
        }
    }
}
