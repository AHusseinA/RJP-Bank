using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbContextFactory<AccountDbContext> _contextFactory;

        public AccountRepository(IDbContextFactory<AccountDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Account> GetAccountByIdAsync(int Id)
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            return await _dbContext.Accounts.FindAsync(Id);
        }

        public async Task<Account> GetAccountInfoByIdAsync(int Id)
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            return await _dbContext.Accounts.Include(p => p.Customer).FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async  Task<Account> GetAccountByCustomerIdAsync(int customerId)
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            return await _dbContext.Accounts.Include(p => p.Customer).FirstOrDefaultAsync(a => a.CustomerId == customerId);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetAllAccountInfosAsync()
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            return await _dbContext.Accounts.Include(a => a.Customer).ToListAsync();
        }

        public async Task CreateAccountAsync(Account account)
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Account account)
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            _dbContext.Accounts.Update(account);
            await _dbContext.SaveChangesAsync();
        }
    }
}
