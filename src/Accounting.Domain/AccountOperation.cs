using System;

namespace Accounting.Domain
{
    public class AccountOperation
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public AccountOperationType OperationType { get; set; }
        public string Description { get; set; }

    }

    public enum AccountOperationType
    {
        Debit = 1,
        Credit = 2
    }
}
