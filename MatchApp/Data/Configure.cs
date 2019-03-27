using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text; 

namespace MatchApp.Data
{
    public static class Configure
    {
        public static void ConfigureServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MatchDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("MatchApp.Data")));

        }
    }
}
