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
using Shop.Common.Redis;
using Shop.Common.RestEase;
using Shop.Common.Swagger;
using Shop.Services.Customers.Messages.Commands;
using Shop.Services.Customers.Domain;
using Shop.Services.Customers.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Services.Customers.Messages.Events;

namespace Shop.Services.Customers
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
            services.AddRedis();
            services.AddInitializers(typeof(IMongoDbInitializer));
            services.RegisterServiceForwarder<IProductsService>("products-service");

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddDispatchers();
            builder.AddRabbitMq();
            builder.AddMongo();
            builder.AddMongoRepository<Cart>("Carts");
            builder.AddMongoRepository<Customer>("Customers");
            builder.AddMongoRepository<Product>("Products");

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
                .SubscribeCommand<CreateCustomer>(onError: (c, e) =>
                    new CreateCustomerRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<AddProductToCart>(onError: (c, e) =>
                    new AddProductToCartRejected(c.CustomerId, c.ProductId, c.Quantity, e.Message, e.Code))
                .SubscribeCommand<DeleteProductFromCart>(onError: (c, e) =>
                    new DeleteProductFromCartRejected(c.CustomerId, c.ProductId, e.Message, e.Code))
                .SubscribeCommand<ClearCart>(onError: (c, e) =>
                    new ClearCartRejected(c.CustomerId, e.Message, e.Code))
                .SubscribeEvent<SignedUp>(@namespace: "identity")
                .SubscribeEvent<ProductCreated>(@namespace: "products")
                .SubscribeEvent<ProductUpdated>(@namespace: "products")
                .SubscribeEvent<ProductDeleted>(@namespace: "products")
                .SubscribeEvent<OrderApproved>(@namespace: "orders")
                .SubscribeEvent<OrderCompleted>(@namespace: "orders")
                .SubscribeEvent<OrderCanceled>(@namespace: "orders");

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
