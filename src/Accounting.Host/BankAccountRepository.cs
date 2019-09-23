using Accounting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Host
{
    public sealed class BankAccountRepository : IBankAccountRepository
    {
        public Task<BankAccount> FindAsync(Guid bankAccountId)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(BankAccount bankAccount)
        {
            throw new NotImplementedException();
        }
    }

    public class BankAccountData
    {
        public Guid Id { get; set; }

        public Guid UserAccountId { get; set; }

        public int Currency { get; set; }   
    }

    public class BankAccountTransactionData
    {
        public Guid Id { get; set; }
        public int TransactionType { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
    }
}
