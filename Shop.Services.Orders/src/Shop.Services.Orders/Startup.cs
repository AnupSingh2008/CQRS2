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
using Shop.Common.Mvc;
using Shop.Common.RabbitMq;
using Shop.Common.RestEase;
using Shop.Common.Swagger;
using Shop.Services.Orders.Messages.Commands;
using Shop.Services.Orders.Messages.Events;
using Shop.Services.Orders.Domain;
using Shop.Services.Orders.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Services.Orders
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
            services.AddSwaggerDocs();
            services.AddConsul();
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddInitializers(typeof(IMongoDbInitializer));
            services.RegisterServiceForwarder<IProductsService>("products-service");
            services.RegisterServiceForwarder<ICustomersService>("customers-service");

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddDispatchers();
            builder.AddRabbitMq();
            builder.AddMongo();
            builder.AddMongoRepository<Order>("Orders");
            builder.AddMongoRepository<OrderItem>("OrderItems");
            builder.AddMongoRepository<Customer>("Customers");

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime, IConsulClient client,
            IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseServiceId();
            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeCommand<CreateOrder>(onError: (c, e) =>
                    new CreateOrderRejected(c.Id, c.CustomerId, e.Message, e.Code))
                .SubscribeCommand<ApproveOrder>(onError: (c, e) =>
                    new ApproveOrderRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<CancelOrder>(onError: (c, e) =>
                    new CancelOrderRejected(c.Id, c.CustomerId, e.Message, e.Code))
                .SubscribeCommand<RevokeOrder>(onError: (c, e) =>
                    new RevokeOrderRejected(c.Id, c.CustomerId, e.Message, e.Code))
                .SubscribeCommand<CompleteOrder>(onError: (c, e) =>
                    new CompleteOrderRejected(c.Id, c.CustomerId, e.Message, e.Code))
                .SubscribeCommand<CreateOrderDiscount>()
                .SubscribeEvent<CustomerCreated>(@namespace: "customers")
                .SubscribeEvent<DiscountCreated>(@namespace: "discounts");

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
                Container.Dispose();
            });

            startupInitializer.InitializeAsync();
        }
    }
}
