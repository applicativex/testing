using Accounting.Domain;
using Accounting.Domain.Queries;
using Accounting.Host;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Accounting.IntegrationTests
{
    public class CurrencyExchangeTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CurrencyExchangeTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Check_Get_Rates()
        {
            // Arrange
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<ICurrencyExchangeAdapter, TestCurrencyExchangeAdapter>();
                });
            }).CreateClient();

            var response = await client.GetAsync("api/currency-exchange/rates");
            response.EnsureSuccessStatusCode();

            var rates = JsonConvert.DeserializeObject<GetExchangeRatesQueryResult>(await response.Content.ReadAsStringAsync()).Rates;

            Assert.True(rates.Length > 0);
        }
    }

    public sealed class TestCurrencyExchangeAdapter : ICurrencyExchangeAdapter
    {
        private readonly Dictionary<AccountCurrency, decimal> _rates = new Dictionary<AccountCurrency, decimal> 
        {
            [AccountCurrency.UAH] = 1,
            [AccountCurrency.EUR] = 25,
            [AccountCurrency.USD] = 20
        };

        public Task<decimal?> GetExchangeRate(AccountCurrency from, AccountCurrency to)
        {
            return Task.FromResult<decimal?>(1m);
        }
    }
}
