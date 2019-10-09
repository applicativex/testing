using System;
using System.Collections.Generic;

namespace Accounting.Domain
{
    public class Account
    {
        public Account(Guid id, AccountCurrency currency, List<AccountEntry> entries)
        {
            Id = id;
            Currency = currency;
            this.entries = entries;
        }
        public Account(Guid id, AccountCurrency currency)
            : this(id, currency, new List<AccountEntry>())
        {
        }

        private List<AccountEntry> entries = new List<AccountEntry>();

        public Guid Id { get; }

        public AccountCurrency Currency { get; }

        public IReadOnlyList<AccountEntry> Entries => entries;

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
            foreach (var entry in entries)
            {
                result += entry.Amount;
            }

            return result;
        }

        private AccountEntry AddEntry(decimal amount)
        {
            var entry = new AccountEntry(Guid.NewGuid(), Id, amount, DateTimeOffset.UtcNow);
            entries.Add(entry);
            return entry;
        }
    }

    public static class AccountExt
    {
        public static AccountTransaction Deposit(this Account account, decimal amount)
            =>
            AccountTransaction.DepositTransaction(account, amount);

        public static AccountTransaction Withdraw(this Account account, decimal amount)
            =>
            AccountTransaction.WithdrawTransaction(account, amount);

        public static AccountTransaction Transfer(this Account fromAccount, Account toAccount, decimal amount)
            =>
            AccountTransaction.TransferTransaction(fromAccount, toAccount, amount);
    }
}
