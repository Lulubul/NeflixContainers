using Identity.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Identity.API
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var host = CreateWebHostBuilder(args).Build();
                host.MigrateDbContext<UserIdentityContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<UserIdentityContextSeed>>();
                    new UserIdentityContextSeed()
                        .SeedAsync(context, logger)
                        .Wait();
                });
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>();

    }
}
