using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Shop.Common;
using Shop.Common.Dispatchers;
using Shop.Common.Mongo;
using Shop.Common.Mvc;
using Shop.Common.RabbitMq;
using Shop.Common.Redis;
using Shop.Common.Swagger;
using Shop.Services.Products.Messages.Commands;
using Shop.Services.Products.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Common.Consul;
using Consul;
using Shop.Common.Jaeger;
using Shop.Services.Products.Messages.Events;

namespace Shop.Services.Products
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

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddRabbitMq();
            builder.AddMongo();
            builder.AddMongoRepository<Product>("Products");
            builder.AddDispatchers();

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
                .SubscribeCommand<CreateProduct>(onError: (c, e) =>
                    new CreateProductRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<UpdateProduct>(onError: (c, e) =>
                    new UpdateProductRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<DeleteProduct>(onError: (c, e) =>
                    new DeleteProductRejected(c.Id, e.Message, e.Code))
                .SubscribeCommand<ReserveProducts>(onError: (c, e) =>
                    new ReserveProductsRejected(c.OrderId, e.Message, e.Code))
                .SubscribeCommand<ReleaseProducts>(onError: (c, e) =>
                    new ReleaseProductsRejected(c.OrderId, e.Message, e.Code));

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
