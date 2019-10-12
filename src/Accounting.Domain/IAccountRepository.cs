using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting.Domain
{
    public interface IAccountRepository
    {
        Task<Account> FindAsync(Guid accountId);

        Task<IReadOnlyCollection<Account>> FindByUserAsync(Guid userId);

        Task SaveAsync(Account account);
    }
}
