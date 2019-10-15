using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using Accounting.Domain;
using System.Collections.Generic;

namespace Accounting.Host
{
    public class AccountOperationQueryRepository : IAccountOperationQueryRepository
    {
        private readonly AccountingContext _accountingContext;

        public AccountOperationQueryRepository(AccountingContext accountingContext)
        {
            _accountingContext = accountingContext;
        }

        public async Task<IReadOnlyCollection<AccountOperation>> GetAccountOperationsAsync(Guid accountId)
        {
            var debitEntries = await (
                from ae in _accountingContext.AccountEntries.AsNoTracking()
                join dae in _accountingContext.AccountTransactions.AsNoTracking() on ae.Id equals dae.DebitEntryId
                where ae.AccountId == accountId
                select new AccountOperation
                {
                    Id = ae.Id,
                    AccountId = ae.AccountId,
                    CreatedAt = ae.EntryDate,
                    Amount = ae.Amount,
                    OperationType = AccountOperationType.Debit,
                    Description = dae.Description
                }).ToListAsync();
            var creditEntries = await (
                from ae in _accountingContext.AccountEntries.AsNoTracking()
                join cae in _accountingContext.AccountTransactions.AsNoTracking() on ae.Id equals cae.CreditEntryId
                where ae.AccountId == accountId
                select new AccountOperation
                {
                    Id = ae.Id,
                    AccountId = ae.AccountId,
                    CreatedAt = ae.EntryDate,
                    Amount = ae.Amount,
                    OperationType = AccountOperationType.Credit,
                    Description = cae.Description
                }).ToListAsync();
            var result = debitEntries.Concat(creditEntries).OrderByDescending(x => x.CreatedAt).ToArray();
            return result;
        }
    }
}
