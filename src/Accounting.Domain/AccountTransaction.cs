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

        public static AccountTransaction DepositTransaction(IAccountable account, decimal amount)
        {
            return TransferTransaction(SystemAccount.FromCurrency(account.Currency), account, amount);
        }

        public static AccountTransaction WithdrawTransaction(IAccountable account, decimal amount)
        {
            return TransferTransaction(account, SystemAccount.FromCurrency(account.Currency), amount);
        }

        public static AccountTransaction TransferTransaction(IAccountable fromAccount, IAccountable toAccount, decimal amount)
        {
            if (fromAccount.Currency != toAccount.Currency)
            {
                throw new InvalidOperationException("Accounts should have same currency.");
            }

            return new AccountTransaction(Guid.NewGuid(), amount, DateTimeOffset.UtcNow, fromAccount.Debit(amount), toAccount.Credit(amount));
        }
    }
}
