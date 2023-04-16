using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly IDbContextFactory<AccountDbContext> _contextFactory;

        public CustomerRepository(IDbContextFactory<AccountDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            return await _dbContext.Customers
                .Include(c => c.Accounts)
                .ToListAsync();
        }
    }
}
