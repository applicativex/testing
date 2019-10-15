using System;
using System.Threading.Tasks;
using Accounting.Domain;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Host
{
    public class UserRepository : IUserRepository
    {
        private readonly AccountingContext _accountingContext;

        public UserRepository(AccountingContext accountingContext)
        {
            _accountingContext = accountingContext;
        }

        public async Task<User> FindAsync(Guid id)
        {
            var result =  await _accountingContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            return new User(result.Id, result.FirstName, result.LastName, result.Email);
        }

        public async Task SaveAsync(User userAccount)
        {
            await _accountingContext.AddAsync(new UserData
            {
                Id = userAccount.Id,
                FirstName = userAccount.FirstName,
                LastName = userAccount.LastName,
                Email = userAccount.Email
            });
            await _accountingContext.SaveChangesAsync();
        }
    }
}