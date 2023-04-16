using Account.Service;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccountMicroservice
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConfiguration _configuration;
        private readonly IModel _channel;
        private string _responseQueueName;

        public RabbitMQService(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _responseQueueName = _channel.QueueDeclare().QueueName;
        }

        public void SendMessage(string message)
        {
            var queueName = _configuration["RabbitMQ:QueueName"];
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }

        public void SendMessage<T>(T message, string queue)
        {
            var queueName = queue;
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            //put the data on to the product queue
            _channel.BasicPublish(exchange: "", routingKey: queue, body: body);
        }

        private string _consumerTag;


        public async Task<TResponse> SendRequestAsync<TResponse>(object request, string queueName)
        {
            var correlationId = Guid.NewGuid().ToString();

            if (_consumerTag != null)
            {
                // Cancel the consumer
                _channel.BasicCancel(_consumerTag);
            }

            // Serialize the request object to JSON
            var requestJson = JsonConvert.SerializeObject(request);
            _responseQueueName = _channel.QueueDeclare().QueueName;

            // Create the request message properties
            var props = _channel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = _responseQueueName;

            // Publish the request message to the queue
            _channel.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: props,
                body: Encoding.UTF8.GetBytes(requestJson));

            // Create a task completion source to wait for the response message
            var tcs = new TaskCompletionSource<TResponse>();

            // Add a consumer to the response queue to receive the response message
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, args) =>
            {
                // Check that the correlation ID matches the request message
                if (args.BasicProperties.CorrelationId == correlationId)
                {
                    // Deserialize the response message body to the specified type
                    var responseJson = Encoding.UTF8.GetString(args.Body.ToArray());
                    var response = JsonConvert.DeserializeObject<TResponse>(responseJson);

                    // Set the result of the task completion source to the deserialized response object
                    tcs.SetResult(response);
                }
            };

            _consumerTag = _channel.BasicConsume(
                consumer: consumer,
                queue: _responseQueueName,
                autoAck: true);
            // Wait for the response message and return the result
            return await tcs.Task;
        }
    }
}
