using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Commands.CurrencyExchange
{
    public class OpenCurrencyAccountCommand : IRequest<OpenCurrencyAccountResult>
    {
        public Guid UserId { get; set; }

        public AccountCurrency Currency { get; set; }
    }

    public class OpenCurrencyAccountResult
    {
        public Guid AccountId { get; set; }
    }
}
