using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting.Domain
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> FindAsync(Guid bankAccountId);

        Task SaveAsync(BankAccount bankAccount);
    }

    public sealed class BankAccount
    {
        private readonly List<BankAccountTransaction> _bankAccountTransactions = new List<BankAccountTransaction>();

        public BankAccount(Guid id, Guid userAccountId, BankAccountCurrency currency, decimal balance)
        {
            Id = id;
            UserAccountId = userAccountId;
            Currency = currency;
            Balance = balance;
        }

        public Guid Id { get; }
        public Guid UserAccountId { get; }
        public BankAccountCurrency Currency { get; }

        public decimal Balance { get; private set; }

        public void Apply(DebitTransaction debitTransaction)
        {
            if (Balance < debitTransaction.Amount)
            {
                throw new InvalidOperationException();
            }

            Balance -= debitTransaction.Amount;
            _bankAccountTransactions.Add(debitTransaction);
        }

        public void Apply(CreditTransaction creditTransaction)
        {
            Balance += creditTransaction.Amount;
            _bankAccountTransactions.Add(creditTransaction);
        }
    }

    public abstract class BankAccountTransaction
    {
        public BankAccountTransaction(Guid id, decimal amount, DateTimeOffset transactionDate)
        {
            if (amount <= 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            Id = id;
            Amount = amount;
            TransactionDate = transactionDate;
        }

        public Guid Id { get; }

        public decimal Amount { get; }

        public DateTimeOffset TransactionDate { get; }
    }

    public sealed class DebitTransaction : BankAccountTransaction
    {
        public DebitTransaction(Guid id, decimal amount, DateTimeOffset transactionDate)
            : base(id, amount, transactionDate)
        {

        }
    }

    public sealed class CreditTransaction : BankAccountTransaction
    {
        public CreditTransaction(Guid id, decimal amount, DateTimeOffset transactionDate)
            : base(id, amount, transactionDate)
        {

        }
    }

    public class Account
    {
        private List<AccountingEntry> entries = new List<AccountingEntry>();
        //private Currency currency;
        public void AddEntry(decimal amount, DateTimeOffset date)
        {
            // Assert.equals(currency, amount.currency());
            entries.Add(new AccountingEntry(amount, date));
        }
        //private Currency currency;
        public void AddEntry(AccountingEntry entry)
        {
            // Assert.equals(currency, amount.currency());
            entries.Add(entry);
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

        public void Withdraw(decimal amount, Account target, DateTimeOffset date)
        {
            new AccountingTransaction(amount, this, target, date);
        }

        public void Deposit(decimal amount, Account source, DateTimeOffset date)
        {
            new AccountingTransaction(amount, source, this, date);
        }
    }

    public class AccountingEntry
    {
        public AccountingEntry(decimal amount, DateTimeOffset date)
        {
            Amount = amount;
        }

        public decimal Amount { get; }
    }

    public class AccountingTransaction
    {
        private List<AccountingEntry> entries = new List<AccountingEntry>();

        public AccountingTransaction(decimal amount, Account from, Account to, DateTimeOffset date)
        {
            AccountingEntry fromEntry = new AccountingEntry(-amount, date);
            from.AddEntry(fromEntry);
            entries.Add(fromEntry);

            AccountingEntry toEntry = new AccountingEntry(amount, date);
            to.AddEntry(toEntry);
            entries.Add(toEntry);
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
    }
}
