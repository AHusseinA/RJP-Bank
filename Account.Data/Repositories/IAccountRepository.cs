using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<Account> GetAccountInfoByIdAsync(int accountId);
        Task<Account> GetAccountByCustomerIdAsync(int customerId);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<IEnumerable<Account>> GetAllAccountInfosAsync();
        Task CreateAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
    }
}
