using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace MessagesService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("MessagesService - Building and running host.");
            CreateHostBuilder(args).Build().Run();
            Console.WriteLine("MessagesService - Host shut down.");
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
