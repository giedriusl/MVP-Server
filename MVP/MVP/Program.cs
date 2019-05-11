
using Microsoft.AspNetCore;
﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MVP.DataAccess;
using MVP.DataAccess.Seed;
using MVP.Extensions;
using NLog.Web;

namespace MVP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args)
                .MigrateDbContext<MvpContext>((context, services) =>
                {
                    InitialDataSeed.SeedAsync(context, services).Wait();
                }).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog()
                .Build();
    }
}
