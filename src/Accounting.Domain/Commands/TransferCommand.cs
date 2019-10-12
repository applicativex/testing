using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accounting.Domain.Commands
{
    public class TransferCommand : IRequest<TransferResult>
    {
        public Guid FromAccountId { get; set; }

        public Guid ToAccountId { get; set; }

        public decimal Amount { get; set; }
    }

    public class TransferResult
    {
        public Guid TransactionId { get; set; }
    }
}
