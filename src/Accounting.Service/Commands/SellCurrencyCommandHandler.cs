using System.Threading;
using System.Threading.Tasks;
using Accounting.Domain;
using Accounting.Domain.Commands.CurrencyExchange;
using MediatR;

namespace Accounting.Service.Commands
{
    public class SellCurrencyCommandHandler : IRequestHandler<SellCurrencyCommand, SellCurrencyResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly ICurrencyExchangeAdapter _currencyExchangeAdapter;

        public SellCurrencyCommandHandler(IAccountRepository accountRepository, IAccountTransactionRepository accountTransactionRepository, ICurrencyExchangeAdapter currencyExchangeAdapter)
        {
            _accountRepository = accountRepository;
            _accountTransactionRepository = accountTransactionRepository;
            _currencyExchangeAdapter = currencyExchangeAdapter;
        }

        public async Task<SellCurrencyResult> Handle(SellCurrencyCommand request, CancellationToken cancellationToken)
        {
            var baseAccount = await _accountRepository.FindAsync(request.AccountId);
            var currencyAccount = await _accountRepository.FindAsync(request.CurrencyAccountId);
            var exchangeBaseAccount = SystemAccount.FromCurrency(baseAccount.Currency);
            var exchangeCurrencyAccount = SystemAccount.FromCurrency(currencyAccount.Currency);

            var exchangeRate = await _currencyExchangeAdapter.GetExchangeRate(currencyAccount.Currency, baseAccount.Currency);
            var baseAmount = request.Amount * exchangeRate;

            var debiCurrencyAccount = currencyAccount.Transfer(exchangeCurrencyAccount, request.Amount);
            var creditBaseAccount = exchangeBaseAccount.Transfer(baseAccount, baseAmount.Value);

            await _accountTransactionRepository.SaveAsync(debiCurrencyAccount);
            await _accountTransactionRepository.SaveAsync(creditBaseAccount);
            return new SellCurrencyResult();
        }
    }
}
