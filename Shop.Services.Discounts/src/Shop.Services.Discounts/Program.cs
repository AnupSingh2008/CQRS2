using Shop.Common.Logging;
using Shop.Common.Metrics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Shop.Services.Discounts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseLogging()
                .UseAppMetrics();
    }
}
