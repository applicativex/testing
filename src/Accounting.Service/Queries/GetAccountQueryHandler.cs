using Accounting.Domain;
using Accounting.Domain.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.Queries
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, GetAccountQueryResult>
    {
        private readonly IAccountRepository _accountRepository;

        public GetAccountQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<GetAccountQueryResult> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindAsync(request.AccountId);
            return new GetAccountQueryResult
            {
                Id = account.Id,
                Currency = account.Currency.ToString(),
                Balance = account.GetBalance(),
                Description = "Account description"
            };
        }
    }
}
