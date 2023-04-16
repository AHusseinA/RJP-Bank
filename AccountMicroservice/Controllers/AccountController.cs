using Account.Service;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> OpenAccount(CreateAccountInfo accountInfo)
        {
            var account = await _accountService.CreateAccountAsync(accountInfo);
            var accountDetails = await _accountService.GetAccountDetailsAsync(accountInfo.CustomerId);
            return Ok(accountDetails);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAccountByCustomerId(int customerId)
        {
            var account = await _accountService.GetAccountDetailsAsync(customerId);
            return Ok(account);
        }

        //Get all accounts
        [HttpGet()]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountService.GetAccountsAsync();
            if (accounts == null)
            {
                return NotFound();
            }
            return Ok(accounts);
        }
    }
}
