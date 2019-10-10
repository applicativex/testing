using Accounting.Domain;
using Accounting.Domain.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.Commands
{
    public class DepositAccountCommandHandler : IRequestHandler<DepositAccountCommand, DepositAccountResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;

        public DepositAccountCommandHandler(IAccountRepository accountRepository, IAccountTransactionRepository accountTransactionRepository)
        {
            _accountRepository = accountRepository;
            _accountTransactionRepository = accountTransactionRepository;
        }

        public async Task<DepositAccountResult> Handle(DepositAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindAsync(request.AccountId);
            var depositTransaction = account.Deposit(request.Amount);
            await _accountTransactionRepository.SaveAsync(depositTransaction);
            return new DepositAccountResult
            {
                TransactionId = depositTransaction.Id
            };
        }
    }
}
