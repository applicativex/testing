using Accounting.Domain;
using NUnit.Framework;
using System;

namespace Accounting.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var cashAccount = new Account();
            var bankAccount = new Account();

            bankAccount.Deposit(100, cashAccount, DateTimeOffset.UtcNow);

            Assert.That(cashAccount.Balance(), Is.EqualTo(-100m));
            Assert.That(bankAccount.Balance(), Is.EqualTo(100m));
        }
    }
}