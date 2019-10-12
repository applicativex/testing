using MediatR;
using System;

namespace Accounting.Domain.Commands.CurrencyExchange
{
    public class SellCurrencyCommand : IRequest<SellCurrencyResult>
    {
        public Guid UserId { get; set; }

        public Guid AccountId { get; set; }

        public Guid CurrencyAccountId { get; set; }

        public decimal Amount { get; set; }
    }

    public class SellCurrencyResult
    {

    }
}
