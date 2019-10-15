using Accounting.Domain;
using Accounting.Domain.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.Commands
{
    public class TransferCommandHandler : IRequestHandler<TransferCommand, TransferResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;

        public TransferCommandHandler(IAccountRepository accountRepository, IAccountTransactionRepository accountTransactionRepository)
        {
            _accountRepository = accountRepository;
            _accountTransactionRepository = accountTransactionRepository;
        }

        public async Task<TransferResult> Handle(TransferCommand request, CancellationToken cancellationToken)
        {
            var fromAccount = await _accountRepository.FindAsync(request.FromAccountId);
            var toAccount = await _accountRepository.FindAsync(request.ToAccountId);

            var transferTransaction = fromAccount.Transfer(
                toAccount, request.Amount, $"Transfer {request.Amount}{fromAccount.Currency.ToString()} from {fromAccount.Id.ToString("N")} to {toAccount.Id.ToString("N")} operation");
            await _accountTransactionRepository.SaveAsync(transferTransaction);

            return new TransferResult
            {
                TransactionId = transferTransaction.Id
            };
        }
    }
}
