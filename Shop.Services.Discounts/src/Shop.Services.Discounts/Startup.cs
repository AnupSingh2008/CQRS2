using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Consul;
using Shop.Common;
using Shop.Common.Consul;
using Shop.Common.Dispatchers;
using Shop.Common.Jaeger;
using Shop.Common.Mongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Common.Mvc;
using Shop.Common.RabbitMq;
using Shop.Common.RestEase;
using Shop.Services.Discounts.Domain;
using Shop.Services.Discounts.Messages.Commands;
using Shop.Services.Discounts.Messages.Events;
using Shop.Services.Discounts.Metrics;
using Shop.Services.Discounts.Services;
using OpenTracing;

namespace Shop.Services.Discounts
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddInitializers(typeof(IMongoDbInitializer));
            services.AddConsul();
            services.AddJaeger();
            services.RegisterServiceForwarder<IOrdersService>("orders-service");
            services.AddTransient<IMetricsRegistry, MetricsRegistry>();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddDispatchers();
            builder.AddMongo();
            builder.AddMongoRepository<Customer>("Customers");
            builder.AddMongoRepository<Discount>("Discounts");
            builder.AddRabbitMq();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime, IStartupInitializer initializer,
            IConsulClient consulClient)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            initializer.InitializeAsync();
            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeCommand<CreateDiscount>(onError: (cmd, ex)
                    => new CreateDiscountRejected(cmd.CustomerId, ex.Message, "customer_not_found"))
                .SubscribeEvent<CustomerCreated>(@namespace: "customers")
                .SubscribeEvent<OrderCompleted>(@namespace: "orders");
            var serviceId = app.UseConsul();

            applicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId);
                Container.Dispose();
            });
        }
    }
}
