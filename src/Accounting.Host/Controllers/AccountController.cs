using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounting.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Host.Controllers
{
    [Route("api/accounts")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccount(Guid accountId)
        {
            var account = await _accountRepository.FindAsync(accountId);
            return Ok(account.Balance());
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateAccount()
        {
            var account = new Account(Guid.NewGuid(), AccountCurrency.UAH);
            await _accountRepository.SaveAsync(account);

            return Ok(account.Id);
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> Transaction([FromBody] TransactionRequest transaction)
        {
            var from = await _accountRepository.FindAsync(transaction.From);
            var to = await _accountRepository.FindAsync(transaction.To);

            await _accountRepository.SaveAsync(from.Withdraw(transaction.Amount, to, DateTimeOffset.UtcNow));

            return Ok();
        }
    }

    public class TransactionRequest
    {
        public Guid From { get; set; }

        public Guid To { get; set; }

        public decimal Amount { get; set; }
    }
}