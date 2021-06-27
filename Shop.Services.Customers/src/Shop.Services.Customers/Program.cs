using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Shop.Common.Logging;
using Shop.Common.Metrics;
using Shop.Common.Mvc;
using Shop.Common.Vault;

namespace Shop.Services.Customers
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
                .UseVault()
                .UseLockbox()
                .UseAppMetrics();
    }
}