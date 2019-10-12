using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Queries
{
    public class GetExchangeRatesQuery : IRequest<GetExchangeRatesQueryResult>
    {
    }

    public class GetExchangeRatesQueryResult
    {
        public CurrencyRateDto[] Rates { get; set; }
    }

    public class CurrencyRateDto
    {
        public string Currency { get; set; }

        public decimal Rate { get; set; }
    }
}
