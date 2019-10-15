using System;
using System.Threading.Tasks;
using Accounting.Domain.Commands;
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

        [HttpGet("{accountId}/operations")]
        public async Task<IActionResult> GetAccountOperations(Guid accountId)
        {
            var result = await _mediator.Send(new GetAccountOperationsQuery
            {
                AccountId = accountId
            });
            return Ok(result);
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> Deposit(Guid accountId, [FromBody] DepositRequest request)
        {
            var result = await _mediator.Send(new DepositCommand
            {
                AccountId = accountId,
                Amount = request.Amount
            });
            return Ok(result);
        }

        [HttpPost("{accountId}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid accountId, [FromBody] WithdrawRequest request)
        {
            var result = await _mediator.Send(new WithdrawCommand
            {
                AccountId = accountId,
                Amount = request.Amount
            });
            return Ok(result);
        }

        [HttpPost("{accountId}/transfer")]
        public async Task<IActionResult> Transfer(Guid accountId, [FromBody] TransferRequest request)
        {
            var result = await _mediator.Send(new TransferCommand
            {
                FromAccountId = accountId,
                ToAccountId = request.AccountId,
                Amount = request.Amount
            });
            return Ok(result);
        }
    }

    public class DepositRequest
    {
        public decimal Amount { get; set; }
    }

    public class WithdrawRequest
    {
        public decimal Amount { get; set; }
    }

    public class TransferRequest
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }
}