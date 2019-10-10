using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Commands
{
    public class DepositAccountCommand : IRequest<DepositAccountResult>
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }

    public class DepositAccountResult
    {
        public Guid TransactionId { get; set; }
    }
}
