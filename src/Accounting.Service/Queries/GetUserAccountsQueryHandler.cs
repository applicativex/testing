using Accounting.Domain;
using Accounting.Domain.Queries;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.Queries
{
    public class GetUserAccountsQueryHandler : IRequestHandler<GetUserAccountsQuery, GetUserAccountsQueryResult>
    {
        private readonly IAccountRepository _accountRepository;

        public GetUserAccountsQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<GetUserAccountsQueryResult> Handle(GetUserAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _accountRepository.FindByUserAsync(request.UserId);
            return new GetUserAccountsQueryResult
            {
                Accounts = accounts.Select(x => new AccountDto { Id = x.Id, Currency = x.Currency.ToString(), Balance = x.GetBalance(), Description = "" }).ToArray()
            };
        }
    }
}
