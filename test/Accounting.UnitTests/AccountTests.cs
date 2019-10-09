using Accounting.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Accounting.UnitTests
{
    public class AccountTests
    {
        private Account SUT;

        [SetUp]
        public void Setup()
        {
            var accountId = Guid.NewGuid();

            SUT = new Account(
                accountId,
                AccountCurrency.UAH, 
                new List<AccountEntry>
                {
                    new AccountEntry(Guid.NewGuid(), accountId, 100, DateTimeOffset.UtcNow)
                });
        }

        [Test]
        public void Account_Can_Be_Deposited()
        {
            SUT.Deposit(100);

            Assert.That(SUT.Balance(), Is.EqualTo(200m));
        }

        [Test]
        public void Account_Can_Be_Withdrawed()
        {
            SUT.Withdraw(50);

            Assert.That(SUT.Balance(), Is.EqualTo(50));
        }

        [Test]
        public void Account_Can_Transfer()
        {
            var depositAccount = new Account(Guid.NewGuid(), AccountCurrency.UAH);

            SUT.Transfer(depositAccount, 50);

            Assert.That(SUT.Balance(), Is.EqualTo(50));
            Assert.That(depositAccount.Balance(), Is.EqualTo(50));
        }
    }
}