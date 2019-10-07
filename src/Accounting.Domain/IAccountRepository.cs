using System;
using System.Threading.Tasks;

namespace Accounting.Domain
{
    public interface IAccountRepository
    {
        Task<Account> FindAsync(Guid accountId);

        Task SaveAsync(Account account);
    }
}
