using Account.Data;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service
{

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IConfiguration _configuration;

        public CustomerService(
            ICustomerRepository customerRepository,
            IConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
        }

        public async Task<List<CustomerInfo>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            var accountsDetails = customers.Select(customer => new CustomerInfo
            {
                CustomerName = $"{customer.Name} {customer.Surname}",
                FirstName = customer.Name,
                LastName = customer.Surname,
                Id = customer.Id,
                HasAccount = customer.Accounts != null && customer.Accounts.Any()

            }).ToList();

            return accountsDetails;
        }
    }
}
