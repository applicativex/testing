using System;
using System.Collections.Generic;

namespace Accounting.Domain
{
    public class AccountTransaction
    {
        private List<AccountEntry> entries = new List<AccountEntry>();

        public AccountTransaction(decimal amount, Account from, Account to, DateTimeOffset date)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            TransactionDate = date;
            DebitEntry = new AccountEntry(Guid.NewGuid(), from.Id, -amount, date);
            CreditEntry = new AccountEntry(Guid.NewGuid(), to.Id, amount, date);
            
            from.AddEntry(DebitEntry);
            entries.Add(DebitEntry);

            to.AddEntry(CreditEntry);
            entries.Add(CreditEntry);
        }
        //public void testBalanceUsingTransactions()
        //{
        //    revenue = new Account(Currency.USD);
        //    deferred = new Account(Currency.USD);
        //    receivables = new Account(Currency.USD);
        //    revenue.withdraw(Money.dollars(500), receivables, new MfDate(1, 4, 99));
        //    revenue.withdraw(Money.dollars(200), deferred, new MfDate(1, 4, 99));
        //    assertEquals(Money.dollars(500), receivables.balance());
        //    assertEquals(Money.dollars(200), deferred.balance());
        //    assertEquals(Money.dollars(-700), revenue.balance());
        //}

        public Guid Id { get; }
        public AccountEntry DebitEntry { get; }
        public AccountEntry CreditEntry { get; }

        public decimal Amount { get; }

        public DateTimeOffset TransactionDate { get; }
    }
}
