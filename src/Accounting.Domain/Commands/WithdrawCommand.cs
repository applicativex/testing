using MediatR;
using System;

namespace Accounting.Domain.Commands
{
    public class WithdrawCommand : IRequest<WithdrawResult>
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }

    public class WithdrawResult
    {
        public Guid TransactionId { get; set; }
    }
}
