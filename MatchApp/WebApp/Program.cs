using MatchApp.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MatchApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            IWebHost webHost = CreateWebHostBuilder(args).Build();
            var scope = webHost.Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<MatchDbContext>();
            UtilityApp.Services.MatchService startService = new UtilityApp.Services.MatchService(dbcontext);
            //Task.Run(() => startService.GetMatchTimeSpanAndSave(TimeSpan.FromSeconds(3), true,CancellationToken.None));
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build())
                .UseStartup<Startup>();
    }
}
