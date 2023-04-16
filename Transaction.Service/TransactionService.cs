using DTO;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data;

namespace Transaction.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionInfo> CreateTransactionAsync(TransactionInfo transactionInfo)
        {
            if (transactionInfo == null)
            {
                throw new ArgumentNullException(nameof(transactionInfo));
            }

            var transaction = new Transaction.Data.Transaction
            {
                AccountId = transactionInfo.AccountId,
                Amount = transactionInfo.Amount,
                TransactionType = transactionInfo.TransactionType,
                Date = DateTime.UtcNow
            };

            await _transactionRepository.CreateTransactionAsync(transaction);

            return MapTransactionToTransactionInfo(transaction);
        }

        public async Task<List<TransactionInfo>> GetTransactionsForAccountAsync(int accountId)
        {
            var transactions = await _transactionRepository.GetTransactionsForAccountAsync(accountId);
            return transactions.Select(MapTransactionToTransactionInfo).ToList();
        }

        private TransactionInfo MapTransactionToTransactionInfo(Transaction.Data.Transaction transaction)
        {
            return new TransactionInfo
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                Amount = transaction.Amount,
                TransactionType = transaction.TransactionType,
                Date = transaction.Date,
                TransactionTypeDescription = transaction.TransactionType == (short)Enums.TransactionType.Credit ? "Credit" : "Debit"
            };
        }
    }
}
