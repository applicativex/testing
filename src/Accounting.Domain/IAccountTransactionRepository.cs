using System.Threading.Tasks;

namespace Accounting.Domain
{
    public interface IAccountTransactionRepository
    {
        Task SaveAsync(AccountTransaction transaction);
    }
}
