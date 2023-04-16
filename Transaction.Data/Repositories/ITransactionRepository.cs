using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data
{
    public interface ITransactionRepository
    {
        Task CreateTransactionAsync(Transaction transaction);
        Task<Transaction> GetTransactionAsync(int transactionId);
        Task<List<Transaction>> GetTransactionsForAccountAsync(int accountId);
    }
}
