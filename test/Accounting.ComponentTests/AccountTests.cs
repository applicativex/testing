using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.ComponentTests
{
    [TestFixture]
    public sealed class AccountTests : ComponentTestsBase
    {
        [Test]
        public async Task Deposit_Account()
        {
            // Arrange
            var userId = await CreateUser();
            var userAccounts = await GetAsync<UserAccountsResponse>($"api/users/{userId}/accounts");
            var account = userAccounts.Accounts.Single();

            // Act
            await PostAsync<AmountRequest, TransactionResponse>($"api/accounts/{account.Id}/deposit", new AmountRequest { Amount = 100 });

            // Assert
            var depositedAccount = await GetAsync<AccountResponse>($"api/accounts/{account.Id}");
            Assert.That(depositedAccount.Balance, Is.EqualTo(100));
        }
        [Test]
        public async Task Withdraw_Account()
        {
            // Arrange
            var userId = await CreateUser();
            var userAccounts = await GetAsync<UserAccountsResponse>($"api/users/{userId}/accounts");
            var account = userAccounts.Accounts.Single();
            await PostAsync<AmountRequest, TransactionResponse>($"api/accounts/{account.Id}/deposit", new AmountRequest { Amount = 100 });
            
            // Act
            await PostAsync<AmountRequest, TransactionResponse>($"api/accounts/{account.Id}/withdraw", new AmountRequest { Amount = 50 });

            // Assert
            var withdrawedAccount = await GetAsync<AccountResponse>($"api/accounts/{account.Id}");
            Assert.That(withdrawedAccount.Balance, Is.EqualTo(50));
        }

        private async Task<Guid> CreateUser()
        {
            var createUserRequest = new CreateUserRequest
            {
                Email = $"{Guid.NewGuid().ToString("N")}@gmail.com",
                FirstName = $"{Guid.NewGuid().ToString("N")}",
                LastName = $"{Guid.NewGuid().ToString("N")}",
            };
            var createUserResponse = await PostAsync<CreateUserRequest, CreateUserResponse>("api/users", createUserRequest);
            return createUserResponse.UserId;
        }
    }

    public class AccountDto
    {
        public Guid Id { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }
    }

    public class TransactionResponse
    {
        public Guid TransactionId { get; set; }
    }

    public class AmountRequest
    {
        public decimal Amount { get; set; }
    }
}
