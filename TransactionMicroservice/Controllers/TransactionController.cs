using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transaction.Service;

namespace TransactionMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(TransactionInfo transactionDto)
        {
            await _transactionService.CreateTransactionAsync(transactionDto);
            return Ok();
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetTransactionsForAccount(int accountId)
        {
            var transactions = await _transactionService.GetTransactionsForAccountAsync(accountId);
            return Ok(transactions);
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("Transaction Microservice is running");
        }
    }
}
