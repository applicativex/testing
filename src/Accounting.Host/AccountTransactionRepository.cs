using Accounting.Domain;
using System.Threading.Tasks;

namespace Accounting.Host
{
    public class AccountTransactionRepository : IAccountTransactionRepository
    {
        private readonly AccountingContext accountingContext;

        public AccountTransactionRepository(AccountingContext accountingContext)
        {
            this.accountingContext = accountingContext;
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
