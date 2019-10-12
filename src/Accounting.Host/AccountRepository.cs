using Accounting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Host
{
    public sealed class AccountRepository : IAccountRepository
    {
        private readonly AccountingContext accountingContext;

        public AccountRepository(AccountingContext accountingContext)
        {
            this.accountingContext = accountingContext;
        }

        public async Task<Account> FindAsync(Guid accountId)
        {
            var account = await accountingContext.Accounts
                .AsNoTracking()
                .Include(x => x.Entries)
                .SingleOrDefaultAsync(x => x.Id == accountId);

            return new Account(
                account.Id,
                account.UserId,
                (AccountCurrency)account.Currency,
                account.Entries.Select(x => new AccountEntry(x.Id, x.AccountId, x.Amount, x.EntryDate)).ToList());
        }

        public async Task<IReadOnlyCollection<Account>> FindByUserAsync(Guid userAccountId)
        {
            var accounts = await accountingContext.Accounts
                .AsNoTracking()
                .Include(x => x.Entries)
                .Where(x => x.UserId == userAccountId)
                .ToListAsync();
            return accounts.Select(Convert).ToArray();
        }

        public async Task SaveAsync(Account account)
        {
            await accountingContext.Accounts.AddAsync(new AccountData { Id = account.Id, Currency = (int)account.Currency, });
            await accountingContext.SaveChangesAsync();
        }

        private Account Convert(AccountData account)
        {
            return new Account(
                account.Id,
                account.UserId,
                (AccountCurrency)account.Currency,
                account.Entries.Select(x => new AccountEntry(x.Id, x.AccountId, x.Amount, x.EntryDate)).ToList());
        }
    }
}
