using System;

namespace Accounting.Domain
{
    public class AccountTransaction
    {
        public Guid Id { get; }

        public decimal Amount { get; }

        public DateTimeOffset TransactionDate { get; }

        public AccountEntry DebitEntry { get; }

        public AccountEntry CreditEntry { get; }

        public AccountTransaction(Guid id, decimal amount, DateTimeOffset transactionDate, AccountEntry debitEntry, AccountEntry creditEntry)
        {
            Id = id;
            Amount = amount;
            TransactionDate = transactionDate;
            DebitEntry = debitEntry;
            CreditEntry = creditEntry;
        }

        public static AccountTransaction DepositTransaction(Account account, decimal amount)
        {
            return new AccountTransaction(Guid.NewGuid(), amount, DateTimeOffset.UtcNow, AccountEntry.SystemDebit(amount), account.Credit(amount));
        }

        public static AccountTransaction WithdrawTransaction(Account account, decimal amount)
        {
            return new AccountTransaction(Guid.NewGuid(), amount, DateTimeOffset.UtcNow, account.Debit(amount), AccountEntry.SystemCredit(amount));
        }

        public static AccountTransaction TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
        {
            return new AccountTransaction(Guid.NewGuid(), amount, DateTimeOffset.UtcNow, fromAccount.Debit(amount), toAccount.Credit(amount));
        }
    }
}
