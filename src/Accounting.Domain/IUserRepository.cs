using System;
using System.Threading.Tasks;

namespace Accounting.Domain
{
    public interface IUserRepository
    {
        Task<User> FindAsync(Guid id);

        Task SaveAsync(User userAccount);
    }
}
