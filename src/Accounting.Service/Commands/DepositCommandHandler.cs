using Accounting.Domain;
using Accounting.Domain.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.Commands
{
    public class DepositCommandHandler : IRequestHandler<DepositCommand, DepositResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;

        public DepositCommandHandler(IAccountRepository accountRepository, IAccountTransactionRepository accountTransactionRepository)
        {
            _accountRepository = accountRepository;
            _accountTransactionRepository = accountTransactionRepository;
        }

        public async Task<DepositResult> Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindAsync(request.AccountId);
            var depositTransaction = account.Deposit(request.Amount, $"Deposit {request.Amount} {account.Currency.ToString()} to {account.Id.ToString("N")} operation");
            
            await _accountTransactionRepository.SaveAsync(depositTransaction);

            return new DepositResult
            {
                TransactionId = depositTransaction.Id
            };
        }
    }
}
