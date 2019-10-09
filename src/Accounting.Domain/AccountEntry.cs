using System;

namespace Accounting.Domain
{
    public class AccountEntry
    {
        private static readonly Guid SystemAccountId = Guid.NewGuid();

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

        public static AccountEntry SystemDebit(decimal amount)
        {
            return new AccountEntry(Guid.NewGuid(), SystemAccountId, -amount, DateTimeOffset.UtcNow);
        }

        public static AccountEntry SystemCredit(decimal amount)
        {
            return new AccountEntry(Guid.NewGuid(), SystemAccountId, amount, DateTimeOffset.UtcNow);
        }
    }
}
