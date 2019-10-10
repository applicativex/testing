using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Queries
{
    public class GetAccountBalanceQuery : IRequest<GetAccountBalanceQuery>
    {
        public Guid AccountId { get; set; }
    }

    public class GetAccountBalanceQueryResult
    {
        public decimal Balance { get; set; }
    }
}
