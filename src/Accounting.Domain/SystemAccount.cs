using System;
using System.Collections.Generic;

namespace Accounting.Domain
{
    public sealed class SystemAccount : IAccountable
    {
        public static Guid SystemUserId = Guid.Parse("6d452541-7c1d-4ba9-b480-d21ac3b30b83");

        private static readonly Dictionary<AccountCurrency, SystemAccount> _accountMap = new Dictionary<AccountCurrency, SystemAccount>
        {
            [AccountCurrency.UAH] = new SystemAccount(Guid.Parse("eb7ab20e-3130-47fc-a6a7-dbcd30a60a53"), AccountCurrency.UAH),
            [AccountCurrency.EUR] = new SystemAccount(Guid.Parse("b4db2e93-4b3b-4172-ba2d-568b7674b1d5"), AccountCurrency.EUR),
            [AccountCurrency.USD] = new SystemAccount(Guid.Parse("4eb3ae16-18ce-4fd9-a0e3-ccc1b87bfc11"), AccountCurrency.USD),
        };

        public Guid Id { get; }

        public AccountCurrency Currency { get; }

        public SystemAccount(Guid id, AccountCurrency currency)
        {
            Id = id;
            Currency = currency;
        }

        public static SystemAccount FromCurrency(AccountCurrency currency)
        {
            return _accountMap[currency];
        }

        public AccountEntry Credit(decimal amount)
        {
            return new AccountEntry(Guid.NewGuid(), Id, amount, DateTimeOffset.UtcNow);
        }

        public AccountEntry Debit(decimal amount)
        {
            return new AccountEntry(Guid.NewGuid(), Id, -amount, DateTimeOffset.UtcNow);
        }
    }
}
