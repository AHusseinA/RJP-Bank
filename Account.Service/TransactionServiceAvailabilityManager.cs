using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Account.Service
{
    public class TransactionServiceAvailabilityManager : ITransactionServiceAvailabilityManager
    {
        private readonly HttpClient _httpClient;

        private readonly Timer _timer;
        private bool _isTransactionServiceAvailable;
        private readonly IConfiguration _configuration;

        public TransactionServiceAvailabilityManager(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _isTransactionServiceAvailable = true;

            CheckTransactionServiceAvailabilityAsync().GetAwaiter();

            // Set up a timer to check the availability of the Transaction Microservice every minute
            _timer = new Timer(30000);
            _timer.Elapsed += async (sender, args) => await CheckTransactionServiceAvailabilityAsync();
            _timer.Start();
        }

        public bool IsTransactionServiceAvailable()
        {
            return _isTransactionServiceAvailable;
        }

        private async Task CheckTransactionServiceAvailabilityAsync()
        {
            try
            {
                // Make a request to a known endpoint in the Transaction Microservice
                var response = await _httpClient.GetAsync(_configuration["Urls:TransactionMicroService"] + "/ping");

                // If the response is successful, mark the Transaction Microservice as available
                if (response.IsSuccessStatusCode)
                {
                    _isTransactionServiceAvailable = true;
                }
                // Otherwise, mark it as unavailable
                else
                {
                    _isTransactionServiceAvailable = false;
                }
            }
            catch (Exception ex)
            {
                _isTransactionServiceAvailable = false;
            }
        }
    }
}
