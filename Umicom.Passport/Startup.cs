using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Umicom.Passport
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 运行时调用此方法。使用此方法向容器添加服务
        // 有关如何配置应用程序的详细信息, 请浏览 https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            InMemoryConfiguration.Configuration = this.Configuration;

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddSigningCredential(new X509Certificate2(Path.Combine(basePath,Configuration["Certificates:CerPath"]),Configuration["Certificates:Password"]))
                .AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())
                .AddTestUsers(InMemoryConfiguration.GetUsers().ToList())                
                .AddInMemoryClients(InMemoryConfiguration.GetClients())
                .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources());
            //启动快速启动UI
            services.AddMvc();
        }

        // 运行时调用此方法。使用此方法配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseIdentityServer();
            // for QuickStart-UI
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
