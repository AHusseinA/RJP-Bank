﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Service
{
    public interface IMessageHandler
    {
        void HandleMessage(BasicDeliverEventArgs args, IModel channel);
    }
}
