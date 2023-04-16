using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Service
{
    public interface ICustomerService
    {
        Task<List<CustomerInfo>> GetCustomersAsync();
    }
}
