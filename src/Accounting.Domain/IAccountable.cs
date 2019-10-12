namespace Accounting.Domain
{
    public interface IAccountable
    {
        AccountCurrency Currency { get; }

        AccountEntry Credit(decimal amount);

        AccountEntry Debit(decimal amount);
    }

    public static class IAccountableExt
    {
        public static AccountTransaction Deposit(this IAccountable account, decimal amount)
            =>
            AccountTransaction.DepositTransaction(account, amount);

        public static AccountTransaction Withdraw(this IAccountable account, decimal amount)
            =>
            AccountTransaction.WithdrawTransaction(account, amount);

        public static AccountTransaction Transfer(this IAccountable fromAccount, IAccountable toAccount, decimal amount)
            =>
            AccountTransaction.TransferTransaction(fromAccount, toAccount, amount);
    }
}
