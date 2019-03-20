using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build();
            var scope = webHost.Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<MatchDbContext>();
            UtilityApp.Services.MatchService startService = new UtilityApp.Services.MatchService(dbcontext);
            Task.Run(() => startService.GetMatchTimeSpanAndSave(TimeSpan.FromSeconds(15), true));
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build())
                .UseStartup<Startup>();
    }
}
