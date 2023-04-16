using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Service
{
    public interface ITransactionService
    {
        Task<TransactionInfo> CreateTransactionAsync(TransactionInfo transactionInfo);
        Task<List<TransactionInfo>> GetTransactionsForAccountAsync(int accountId);
    }
}
