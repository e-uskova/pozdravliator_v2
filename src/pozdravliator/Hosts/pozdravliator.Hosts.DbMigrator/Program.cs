﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace pozdravliator.Hosts.DbMigrator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {
                services.AddServices(hostContext.Configuration);
            }).Build();

            await MigrateDatabaseAsync(host.Services);
            await host.RunAsync();
        }

        private static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MigrationDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
