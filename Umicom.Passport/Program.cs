using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Umicom.Passport
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// 启动运行程序，使用Serlog打印用户日志
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
              .UseSerilog((context, configuration) =>
              {                  
                  configuration
                       //最小级别为Debug
                      .MinimumLevel.Debug()                   
                      .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                      .MinimumLevel.Override("System", LogEventLevel.Warning)
                      .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                      .Enrich.FromLogContext()
                      //输出日志记录到文件                      
                      .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}\\logs\\{DateTime.Now:yyyyMMdd}_log.txt")
                     //输出到控制台界面
                      .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate);
              })
            .Build();
    }
}
