using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service
{
    public interface IRabbitMQService
    {
        void SendMessage(string message);

        void SendMessage<T>(T message, string queue);

        Task<TResponse> SendRequestAsync<TResponse>(object request, string queueName);
    }
}
