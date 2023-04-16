using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Service;
using DTO;
using Shared;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace TransactionMicroservice
{
    public class RabbitMQMessageHandler : IMessageHandler
    {
        private readonly ITransactionService _transactionService;
        private readonly IConfiguration _configuration;

        public RabbitMQMessageHandler(ITransactionService transactionService, IConfiguration configuration)
        {
            _transactionService = transactionService;
            _configuration = configuration;
        }

        public void HandleMessage(BasicDeliverEventArgs eventArgs, IModel channel)
        {
            try
            {
                var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                // process the message based on its content
                // for example, if the message is for creating a new transaction
                if (eventArgs.RoutingKey == _configuration["RabbitMQ:QueueName"])
                {
                    var accountCreationMessage = JsonConvert.DeserializeObject<AccountCreationMessage>(message);
                    if (accountCreationMessage != null)
                    {
                        var transaction = new TransactionInfo
                        {
                            AccountId = accountCreationMessage.AccountId,
                            Amount = Math.Abs(accountCreationMessage.InitialCredit),
                            Date = DateTime.Now,
                            TransactionType = accountCreationMessage.InitialCredit < 0 ? (short)Enums.TransactionType.Credit : (short)Enums.TransactionType.Debit,
                            
                        };

                        _transactionService.CreateTransactionAsync(transaction);
                    }
                }
                // else if the message is for getting the list of transactions for an account
                else if (eventArgs.RoutingKey == _configuration["RabbitMQ:QueueGetTransaction"])
                {
                    var accountId = int.Parse(message);
                    var transactions = _transactionService.GetTransactionsForAccountAsync(accountId).Result;

                    // Process the message and send back the response
                    var messageBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(transactions));

                    var replyProperties = channel.CreateBasicProperties();
                    replyProperties.CorrelationId = eventArgs.BasicProperties.CorrelationId;
                    channel.BasicPublish(exchange: "", routingKey: eventArgs.BasicProperties.ReplyTo, basicProperties: replyProperties, body: messageBody);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
