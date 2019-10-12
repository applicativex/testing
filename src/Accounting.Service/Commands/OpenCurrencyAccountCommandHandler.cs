using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Accounting.Domain;
using Accounting.Domain.Commands.CurrencyExchange;
using MediatR;

namespace Accounting.Service.Commands
{
    public class OpenCurrencyAccountCommandHandler : IRequestHandler<OpenCurrencyAccountCommand, OpenCurrencyAccountResult>
    {
        private readonly IAccountRepository _accountRepository;

        public OpenCurrencyAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<OpenCurrencyAccountResult> Handle(OpenCurrencyAccountCommand request, CancellationToken cancellationToken)
        {
            var userAccounts = await _accountRepository.FindByUserAsync(request.UserId);
            var currencyAccount = userAccounts.SingleOrDefault(x => x.Currency == request.Currency);
            if (currencyAccount == null)
            {
                currencyAccount = new Account(Guid.NewGuid(), request.UserId, request.Currency);
                await _accountRepository.SaveAsync(currencyAccount);
            }
            return new OpenCurrencyAccountResult
            {
                AccountId = currencyAccount.Id
            };
        }
    }
}
