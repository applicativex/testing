using System;
using System.Collections.Generic;

namespace Accounting.Domain
{
    public sealed class Account : IAccountable
    {
        private readonly List<AccountEntry> _entries = new List<AccountEntry>();

        public Account(Guid id, Guid userAccountId, AccountCurrency currency, List<AccountEntry> entries)
        {
            Id = id;
            UserId = userAccountId;
            Currency = currency;
            _entries = entries;
        }

        public Account(Guid id, Guid userAccountId, AccountCurrency currency)
            : this(id, userAccountId, currency, new List<AccountEntry>())
        {
        }

        public Guid Id { get; }

        public Guid UserId { get; } 

        public AccountCurrency Currency { get; }

        public IReadOnlyList<AccountEntry> Entries => _entries;

        public AccountEntry Credit(decimal amount)
        {
            return AddEntry(amount);
        }

        public AccountEntry Debit(decimal amount)
        {
            if (Balance() < amount)
            {
                throw new InvalidOperationException();
            }

            return AddEntry(-amount);
        }

        public decimal Balance()
        {
            decimal result = 0;
            foreach (var entry in _entries)
            {
                result += entry.Amount;
            }

            return result;
        }

        private AccountEntry AddEntry(decimal amount)
        {
            var entry = new AccountEntry(Guid.NewGuid(), Id, amount, DateTimeOffset.UtcNow);
            _entries.Add(entry);
            return entry;
        }
    }
}
