using System;
using System.Collections.Generic;

namespace Accounting.Domain
{
    public class AccountTransaction
    {
        public AccountTransaction()
        {

        }
        public AccountTransaction(decimal amount, Guid fromId, Guid toId, DateTimeOffset date)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            TransactionDate = date;
            DebitEntry = new AccountEntry(Guid.NewGuid(), fromId, -amount, date);
            CreditEntry = new AccountEntry(Guid.NewGuid(), toId, amount, date);

            //from.AddEntry(DebitEntry);

            //to.AddEntry(CreditEntry);
        }
        public AccountTransaction(decimal amount, Account from, Account to, DateTimeOffset date)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            TransactionDate = date;
            DebitEntry = new AccountEntry(Guid.NewGuid(), from.Id, -amount, date);
            CreditEntry = new AccountEntry(Guid.NewGuid(), to.Id, amount, date);
            
            from.AddEntry(DebitEntry);            
            to.AddEntry(CreditEntry);
        }

        public Guid Id { get; }
        public AccountEntry DebitEntry { get; }
        public AccountEntry CreditEntry { get; }

        public decimal Amount { get; }

        public DateTimeOffset TransactionDate { get; }

        public virtual void Execute()
        {

        }
    }

    public class DepositTransaction : AccountTransaction
    {
        private readonly Account account;

        public DepositTransaction(decimal amount, Account account, DateTime dateTime)
            :base(amount, account.Id, account.Id, dateTime)
        {
            this.account = account; 
        }

        public override void Execute()
        {
            account.AddEntry(CreditEntry);
        }
    }

    public class WithdrawTransaction : AccountTransaction
    {
        private readonly Account account;

        public WithdrawTransaction(decimal amount, Account account, DateTime dateTime)
            : base(amount, account.Id, account.Id, dateTime)
        {
            this.account = account;
        }

        public override void Execute()
        {
            account.AddEntry(DebitEntry);
        }
    }

    public class TransferTransaction : AccountTransaction
    {
        private readonly Account fromAccount;
        private readonly Account toAccount;

        public TransferTransaction(decimal amount, Account fromAccount, Account toAccount, DateTime dateTime)
            : base(amount, fromAccount.Id, toAccount.Id, dateTime)
        {
            this.fromAccount = fromAccount;
            this.toAccount = toAccount;
        }

        public override void Execute()
        {
            fromAccount.AddEntry(DebitEntry);
            toAccount.AddEntry(CreditEntry);
        }
    }
}
