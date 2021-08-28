using System;
using System.IO;
using CraftsApi.Service.Background;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace CraftsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "CraftsApi.txt");
            //Log.Logger = new LoggerConfiguration()
            //                .MinimumLevel.Debug()
            //                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            //                .Enrich.FromLogContext()
            //                .WriteTo.File(filePath)
            //                .CreateLogger();
            //try
            //{
            //    Log.Information("Starting up the Service");
            //    CreateHostBuilder(args).Build().Run();
            //    return;
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "There was a problem starting Service");
            //    return;
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<DerivedBackgroundPage>();
                }).UseSerilog();
    }
}
