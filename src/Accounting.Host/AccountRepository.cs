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

            return account.Entries.Aggregate(
                new Account(account.Id, (AccountCurrency)account.Currency), 
                (ac, x) => ac.AddEntry(new AccountEntry(x.Id, x.AccountId, x.Amount, x.EntryDate)));
        }

        public async Task SaveAsync(Account account)
        {
            await accountingContext.Accounts.AddAsync(new AccountData { Id = account.Id, Currency = (int)account.Currency, });
            await accountingContext.SaveChangesAsync();
        }

        public async Task SaveAsync(AccountTransaction accountingTransaction)
        {
            await accountingContext.AddRangeAsync(
                new AccountEntryData
                {
                    Id = accountingTransaction.DebitEntry.Id,
                    AccountId = accountingTransaction.DebitEntry.AccountId,
                    Amount = accountingTransaction.DebitEntry.Amount,
                    EntryDate = accountingTransaction.DebitEntry.EntryDate
                }, new AccountEntryData
                {
                    Id = accountingTransaction.CreditEntry.Id,
                    AccountId = accountingTransaction.CreditEntry.AccountId,
                    Amount = accountingTransaction.CreditEntry.Amount,
                    EntryDate = accountingTransaction.DebitEntry.EntryDate
                },
                new AccountTransactionData
                {
                    Id = accountingTransaction.Id,
                    Amount = accountingTransaction.Amount,
                    TransactionDate = accountingTransaction.TransactionDate,
                    DebitEntryId = accountingTransaction.DebitEntry.Id,
                    CreditEntryId = accountingTransaction.CreditEntry.Id,
                });
            await accountingContext.SaveChangesAsync();
        }
    }
}
