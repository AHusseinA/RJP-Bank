using DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Data;
using Transaction.Service;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQ.Client.Exceptions;


namespace TransactionMicroservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //register a context factory
            services.AddDbContextFactory<TransactionDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddScoped<RabbitMQMessageHandler>();
            services.AddScoped<IMessageHandler, RabbitMQMessageHandler>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TransactionMicroservice", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TransactionMicroservice v1"));
            }

            RabbitMQListner(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void RabbitMQListner(IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;

            // create a channel and exchange
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            // create a queue for creating transactions
            var queueNameCreate = Configuration["RabbitMQ:QueueName"];
            channel.QueueDeclare(queue: queueNameCreate, durable: false, exclusive: false, autoDelete: false, arguments: null);

            // create a queue for getting transactions
            var queueNameGet = Configuration["RabbitMQ:QueueGetTransaction"];
            channel.QueueDeclare(queue: queueNameGet, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //channel.ExchangeDeclare(exchange: "myExchange", type: "direct");
            //channel.QueueBind(queue: queueNameCreate, exchange: "myExchange", routingKey: "createTransaction");
            //channel.QueueBind(queue: queueNameGet, exchange: "myExchange", routingKey: "getTransactions");

            // start listening for messages on both queues
            var consumerCreate = new EventingBasicConsumer(channel);
            consumerCreate.Received += (model, ea) =>
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var messageHandler = scope.ServiceProvider.GetService<IMessageHandler>();
                    messageHandler.HandleMessage(ea, channel);
                }
            };
            channel.BasicConsume(queue: queueNameCreate, autoAck: true, consumer: consumerCreate);

            var consumerGet = new EventingBasicConsumer(channel);
            consumerGet.Received += (model, ea) =>
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var messageHandler = scope.ServiceProvider.GetService<IMessageHandler>();
                    messageHandler.HandleMessage(ea, channel);
                }
            };
            channel.BasicConsume(queue: queueNameGet, autoAck: true, consumer: consumerGet);
        }
    }
}
