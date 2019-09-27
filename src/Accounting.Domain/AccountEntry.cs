using System;

namespace Accounting.Domain
{
    public class AccountEntry
    {
        public AccountEntry(Guid id, Guid accountId, decimal amount, DateTimeOffset entryDate)
        {
            Id = id;
            AccountId = accountId;
            Amount = amount;
            EntryDate = entryDate;
        }

        public Guid Id { get; }
        public Guid AccountId { get; }
        public decimal Amount { get; }
        public DateTimeOffset EntryDate { get; }
    }
}
