using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Commands
{
    public class DepositCommand : IRequest<DepositResult>
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }

    public class DepositResult
    {
        public Guid TransactionId { get; set; }
    }
}
