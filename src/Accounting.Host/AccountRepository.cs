using Accounting.Domain;
using Microsoft.EntityFrameworkCore;
using System;
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
                (AccountCurrency)account.Currency,
                account.Entries.Select(x => new AccountEntry(x.Id, x.AccountId, x.Amount, x.EntryDate)).ToList());
        }

        public async Task SaveAsync(Account account)
        {
            await accountingContext.Accounts.AddAsync(new AccountData { Id = account.Id, Currency = (int)account.Currency, });
            await accountingContext.SaveChangesAsync();
        }
    }
}
