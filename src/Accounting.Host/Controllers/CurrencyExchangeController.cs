using Accounting.Domain;
using Accounting.Domain.Commands.CurrencyExchange;
using Accounting.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Host.Controllers
{
    [Route("api/currency-exchange")]
    public class CurrencyExchangeController : Controller
    {
        private readonly IMediator _mediator;

        public CurrencyExchangeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("rates")]
        public async Task<IActionResult> GetRates()
        {
            var result = await _mediator.Send(new GetExchangeRatesQuery());
            return Ok(result);
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy([FromBody] BuyCurrencyRequest request)
        {
            await _mediator.Send(new BuyCurrencyCommand
            {
                UserId = request.UserId,
                AccountId = request.AccountId,
                CurrencyAccountId = request.CurrencyAccountId,
                Amount = request.Amount
            });
            return Ok();
        }

        [HttpPost("sell")]
        public async Task<IActionResult> Sell([FromBody] SellCurrencyRequest request)
        {
            await _mediator.Send(new SellCurrencyCommand
            {
                UserId = request.UserId,
                AccountId = request.AccountId,
                CurrencyAccountId = request.CurrencyAccountId,
                Amount = request.Amount
            });
            return Ok();
        }

        [HttpPut("open-account")]
        public async Task<IActionResult> OpenCurrencyAccount([FromBody] OpenCurrencyAccountRequest request)
        {
            var result = await _mediator.Send(new OpenCurrencyAccountCommand
            {
                UserId = request.UserId,
                Currency = Enum.TryParse<AccountCurrency>(request.Currency, true, out var currency) ? currency : AccountCurrency.None
            });
            return Ok(result);
        }
    }

    public class BuyCurrencyRequest
    {
        public Guid UserId { get; set; }

        public Guid AccountId { get; set; }

        public Guid CurrencyAccountId { get; set; }

        public decimal Amount { get; set; }
    }

    public class SellCurrencyRequest
    {
        public Guid UserId { get; set; }

        public Guid AccountId { get; set; }

        public Guid CurrencyAccountId { get; set; }

        public decimal Amount { get; set; }
    }

    public class OpenCurrencyAccountRequest
    {
        public Guid UserId { get; set; }

        public string Currency { get; set; }
    }
}
