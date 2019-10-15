using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accounting.Domain
{
    public interface IAccountOperationQueryRepository
    {
        Task<IReadOnlyCollection<AccountOperation>> GetAccountOperationsAsync(Guid accountId);
    }
}
