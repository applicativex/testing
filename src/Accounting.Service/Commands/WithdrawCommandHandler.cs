using Accounting.Domain;
using Accounting.Domain.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.Commands
{
    public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand, WithdrawResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;

        public WithdrawCommandHandler(IAccountRepository accountRepository, IAccountTransactionRepository accountTransactionRepository)
        {
            _accountRepository = accountRepository;
            _accountTransactionRepository = accountTransactionRepository;
        }

        public async Task<WithdrawResult> Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.FindAsync(request.AccountId);
            var withdrawTransaction = account.Withdraw(request.Amount, $"Withdraw {request.Amount} {account.Currency.ToString()} from {account.Id.ToString("N")} operation");

            await _accountTransactionRepository.SaveAsync(withdrawTransaction);
            return new WithdrawResult
            {
                TransactionId = withdrawTransaction.Id
            };
        }
    }
}
