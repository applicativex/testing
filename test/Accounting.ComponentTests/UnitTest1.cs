using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Accounting.ComponentTests
{
    [TestFixture]
    public class Tests
    {
        private string API_URL = "http://accounting";

        [SetUp]
        public void SetUp()
        {
            var isLocalRun = Environment.GetEnvironmentVariable("RUN_ENV") == null;
            API_URL = isLocalRun ? "http://localhost:5000" : "http://accounting";
        }

        [TearDown]
        public void Cleanup()
        {
        }

        [Test]
        [Repeat(20)]
        public async Task Test()
        {
            var accountingClient = new HttpClient
            {
                BaseAddress = new Uri(API_URL)
            };
            var createAccountResponse = await accountingClient.PostAsync("api/accounts", null);
            var accountId = await createAccountResponse.Content.ReadAsStringAsync();
            var accountResponse = await accountingClient.GetStringAsync($"api/accounts/{accountId}");
            var account = JsonConvert.DeserializeObject<AccountResponse>(accountResponse);

            Assert.That(account.Id, Is.EqualTo(accountId));
            Assert.That(account.Balance, Is.EqualTo(0));
        }
    }

    public class AccountResponse
    {
        public string Id { get; set; }

        public decimal Balance { get; set; }
    }
}