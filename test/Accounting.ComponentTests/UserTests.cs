using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Accounting.ComponentTests
{

    [TestFixture]
    public sealed class UserTests : ComponentTestsBase
    {
        [Test]
        public async Task Create_User()
        {
            var createUserRequest = new CreateUserRequest
            {
                Email = $"{Guid.NewGuid().ToString("N")}@gmail.com",
                FirstName = $"{Guid.NewGuid().ToString("N")}",
                LastName = $"{Guid.NewGuid().ToString("N")}",
            };
            var createUserResponse = await PostAsync<CreateUserRequest, CreateUserResponse>("api/users", createUserRequest);
            var user = await GetAsync<UserResponse>($"api/users/{createUserResponse.UserId}");

            Assert.That(user.Id, Is.EqualTo(createUserResponse.UserId));
            Assert.That(user.FirstName, Is.EqualTo(createUserRequest.FirstName));
            Assert.That(user.LastName, Is.EqualTo(createUserRequest.LastName));
            Assert.That(user.Email, Is.EqualTo(createUserRequest.Email));
        }

        [Test]
        public async Task User_Should_Be_Created_With_Base_Account()
        {
            var createUserRequest = new CreateUserRequest
            {
                Email = $"{Guid.NewGuid().ToString("N")}@gmail.com",
                FirstName = $"{Guid.NewGuid().ToString("N")}",
                LastName = $"{Guid.NewGuid().ToString("N")}",
            };
            var createUserResponse = await PostAsync<CreateUserRequest, CreateUserResponse>("api/users", createUserRequest);
            var userAccounts = await GetAsync<UserAccountsResponse>($"api/users/{createUserResponse.UserId}/accounts");

            Assert.That(userAccounts.Accounts.Length, Is.EqualTo(1));
            Assert.That(userAccounts.Accounts.Single().Balance, Is.EqualTo(0));
            Assert.That(userAccounts.Accounts.Single().Currency, Is.EqualTo("UAH"));
        }
    }

    public class AccountResponse
    {
        public Guid Id { get; set; }

        public string Currency { get; set; }

        public string Description { get; set; }

        public decimal Balance { get; set; }
    }

    public class CreateUserRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

    public class CreateUserResponse
    {
        public Guid UserId { get; set; }
    }

    public class UserResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

    public class UserAccountsResponse
    {
        public AccountResponse[] Accounts { get; set; }
    }
}