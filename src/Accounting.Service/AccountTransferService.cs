﻿using Accounting.Domain;
using System;
using System.Threading.Tasks;

namespace Accounting.Service
{
    public class AccountTransferService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IAccountTransactionRepository accountTransactionRepository;

        public AccountTransferService(IAccountRepository accountRepository, IAccountTransactionRepository accountTransactionRepository)
        {
            this.accountRepository = accountRepository;
            this.accountTransactionRepository = accountTransactionRepository;
        }

        public async Task Transfer(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var fromAccount = await accountRepository.FindAsync(fromAccountId);
            var toAccount = await accountRepository.FindAsync(toAccountId);
            var transfer = fromAccount.Withdraw(amount, toAccount, DateTimeOffset.UtcNow);
            await accountTransactionRepository.SaveAsync(transfer);
            Console.WriteLine("yaluibliumeymeychyna");
        }
    }
}