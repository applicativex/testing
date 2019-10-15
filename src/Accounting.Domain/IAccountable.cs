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
        public static AccountTransaction Deposit(this IAccountable account, decimal amount, string description)
            =>
            AccountTransaction.DepositTransaction(account, amount, description);

        public static AccountTransaction Withdraw(this IAccountable account, decimal amount, string description)
            =>
            AccountTransaction.WithdrawTransaction(account, amount, description);

        public static AccountTransaction Transfer(this IAccountable fromAccount, IAccountable toAccount, decimal amount, string description)
            =>
            AccountTransaction.TransferTransaction(fromAccount, toAccount, amount, description);
    }
}
