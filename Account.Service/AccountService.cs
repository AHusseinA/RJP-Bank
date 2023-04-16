using Account.Data;
using DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Shared.Enums;

namespace Account.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IConfiguration _configuration;
        private readonly ITransactionServiceAvailabilityManager _transactionServiceAvailabilityManager;

        public AccountService(
            IAccountRepository accountRepository,
            IRabbitMQService rabbitMQService,
            IConfiguration configuration,
            ITransactionServiceAvailabilityManager transactionServiceAvailabilityManag)
        {
            _accountRepository = accountRepository;
            _rabbitMQService = rabbitMQService;
            _configuration = configuration;
            _transactionServiceAvailabilityManager = transactionServiceAvailabilityManag;
        }

        public async Task<AccountInfo> CreateAccountAsync(CreateAccountInfo createAccountInfo)
        {
            var account = await _accountRepository.GetAccountByCustomerIdAsync(createAccountInfo.CustomerId);

            if (account == null)
            {
                account = new Account.Data.Account
                {
                    CustomerId = createAccountInfo.CustomerId,
                    Balance = createAccountInfo.InitialCredit
                };

                // Save the account to the database
                await _accountRepository.CreateAccountAsync(account);
            }
            else
            {
                account.Balance += createAccountInfo.InitialCredit;

                // Update the account to the database
                await _accountRepository.UpdateAccountAsync(account);
            }


            // If initialCredit is not 0, send a message to the TransactionMicroservice to create a new transaction
            if (createAccountInfo.InitialCredit != 0)
            {
                var accountCreationMessage = new AccountCreationMessage
                {
                    AccountId = account.Id,
                    InitialCredit = createAccountInfo.InitialCredit,
                };

                try
                {
                    var messageBody = JsonConvert.SerializeObject(accountCreationMessage);
                    _rabbitMQService.SendMessage(messageBody);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error sending message to TransactionMicroservice", ex);
                }
            }

            return new AccountInfo
            {
                Id = account.Id,
                CustomerId = account.CustomerId,
                Balance = account.Balance,
            };
        }

        public async Task<AccountDetailsInfo> GetAccountDetailsAsync(int customerId)
        {
            var account = await _accountRepository.GetAccountByCustomerIdAsync(customerId);
            List<TransactionInfo> transactions = new List<TransactionInfo>();
            bool showTransaction = false;
            if (account == null)
            {
                return null;
            }

            // Check if the Transaction Microservice is available
            if (_transactionServiceAvailabilityManager.IsTransactionServiceAvailable())
            {
                transactions = await _rabbitMQService.SendRequestAsync<List<TransactionInfo>>(
                account.Id, _configuration["RabbitMQ:QueueGetTransaction"]);
                showTransaction = true;
            }

            var accountDetails = new AccountDetailsInfo
            {
                CustomerName = $"{account.Customer.Name} {account.Customer.Surname}",
                Balance = account.Balance,
                Transactions = transactions,
                FirstName = account.Customer.Name,
                LastName = account.Customer.Surname,
                Id = account.Id,
                ShowTransaction = showTransaction
            };

            return accountDetails;
        }

        public async Task<List<AccountDetailsInfo>> GetAccountsAsync()
        {
            var accounts = await _accountRepository.GetAllAccountInfosAsync();

            var accountsDetails = accounts.Select(account => new AccountDetailsInfo
            {
                CustomerName = $"{account.Customer.Name} {account.Customer.Surname}",
                Balance = account.Balance,
                FirstName = account.Customer.Name,
                LastName = account.Customer.Surname

            }).ToList();

            return accountsDetails;
        }
    }
}
