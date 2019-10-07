using System;
using System.Collections.Generic;

namespace Accounting.Domain
{
    public class Account
    {
        public static readonly Guid SYSTEM_ACCOUNT_ID = Guid.NewGuid();
        
        public Account(Guid id, AccountCurrency currency)
        {
            Id = id;
            Currency = currency;
        }

        private List<AccountEntry> entries = new List<AccountEntry>();
        public Guid Id { get; }
        public AccountCurrency Currency { get; }

        //private Currency currency;
        public void AddEntry(decimal amount, DateTimeOffset date)
        {
            // Assert.equals(currency, amount.currency());
            entries.Add(new AccountEntry(Guid.NewGuid(), Id, amount, date));
        }
        //private Currency currency;
        public Account AddEntry(AccountEntry entry)
        {
            // Assert.equals(currency, amount.currency());
            entries.Add(entry);
            return this;
        }

        public decimal Balance()
        {
            decimal result = 0;
            foreach (var entry in entries)
            {
                result += entry.Amount;
            }

            return result;
        }

        public AccountTransaction Withdraw(decimal amount, Account target, DateTimeOffset date)
        {
            if (Balance() < amount)
            {
                throw new InvalidOperationException();
            }

            return new AccountTransaction(amount, this, target, date);
        }

        public void Deposit(decimal amount, Account source, DateTimeOffset date)
        {
            new AccountTransaction(amount, source, this, date);
        }
    }
}
