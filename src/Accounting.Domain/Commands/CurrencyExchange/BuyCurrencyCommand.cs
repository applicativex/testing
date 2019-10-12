using MediatR;
using System;

namespace Accounting.Domain.Commands.CurrencyExchange
{
    public class BuyCurrencyCommand : IRequest<BuyCurrencyResult>
    {
        public Guid UserId { get; set; }

        public Guid AccountId { get; set; }

        public Guid CurrencyAccountId { get; set; }
         
        public decimal Amount { get; set; }
    }

    public class BuyCurrencyResult
    {
    }
}
