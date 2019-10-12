using System;
using System.Threading.Tasks;
using Accounting.Domain;
using Accounting.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Host.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccount(Guid accountId)
        {
            var result = await _mediator.Send(new GetAccountQuery
            {
                AccountId = accountId
            });
            return Ok(result);
        }

        [HttpGet("{accountId}/entries")]
        public async Task<IActionResult> GetAccountEntries(Guid accountId)
        {
            var result = await _mediator.Send(new GetAccountEntriesQuery
            {
                AccountId = accountId
            });
            return Ok(result);
        }

        //[HttpPost("")]
        //public async Task<IActionResult> CreateAccount()
        //{
        //    var account = new Account(Guid.NewGuid(), Guid.NewGuid(), AccountCurrency.UAH);
        //    await _accountRepository.SaveAsync(account);

        //    return Ok(account.Id.ToString("N"));
        //}

        //[HttpPost("transaction")]
        //public async Task<IActionResult> Transaction([FromBody] TransactionRequest transaction)
        //{
        //    var from = await _accountRepository.FindAsync(transaction.From);
        //    var to = await _accountRepository.FindAsync(transaction.To);
            
        //    return Ok();
        //}
    }

    public class AccountResponse
    {   
        public string Id { get; set; }

        public decimal Balance { get; set; }
    }

    public class TransactionRequest
    {
        public Guid From { get; set; }

        public Guid To { get; set; }

        public decimal Amount { get; set; }
    }

    public class CreateUserAccountRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}