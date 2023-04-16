using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbContextFactory<TransactionDbContext> _contextFactory;
        

        public TransactionRepository(IDbContextFactory<TransactionDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            try
            {
                // With the new C# 8 syntax you can do
                using var _dbContext = _contextFactory.CreateDbContext();
                await _dbContext.Transactions.AddAsync(transaction);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
        }

        public async Task<Transaction> GetTransactionAsync(int transactionId)
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            return await _dbContext.Transactions.FindAsync(transactionId);
        }

        public async Task<List<Transaction>> GetTransactionsForAccountAsync(int accountId)
        {
            using var _dbContext = _contextFactory.CreateDbContext();
            return await _dbContext.Transactions.Where(t => t.AccountId == accountId).ToListAsync();
        }
    }
}
