using Accounting.Domain;
using Accounting.Domain.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
    {
        private readonly IUserRepository _userAccountRepository;
        private readonly IAccountRepository _accountRepository;

        public CreateUserCommandHandler(IUserRepository userAccountRepository, IAccountRepository accountRepository)
        {
            _userAccountRepository = userAccountRepository;
            _accountRepository = accountRepository;
        }

        public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userAccount = new User(Guid.NewGuid(), request.FirstName, request.LastName, request.Email);
            var account = new Account(Guid.NewGuid(), userAccount.Id, AccountCurrency.UAH);
            await _userAccountRepository.SaveAsync(userAccount);
            await _accountRepository.SaveAsync(account);
            return new CreateUserResult { UserId = userAccount.Id };
        }
    }
}
