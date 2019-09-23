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
}
