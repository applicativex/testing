using Accounting.Host;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Accounting.IntegrationTests
{
    [TestFixture]
    public class NbuCurrencyExchangeClientTests
    {
        private NbuCurrencyExchangeClient SUT;

        [SetUp]
        public void Setup()
        {
            SUT = new NbuCurrencyExchangeClient(new System.Net.Http.HttpClient());
        }

        [Test]
        public async Task Test()
        {
            var rates = SUT.GetExchangeRates().GetAwaiter().GetResult();
            
            Assert.That(rates.Length, Is.GreaterThan(0));
            Array.ForEach(rates, x =>
            {
                Assert.That(x.Rate, Is.GreaterThan(0));
                Assert.That(x.Code, Is.Not.Empty);
                Assert.That(x.Description, Is.Not.Empty);
            });
        }
    }
}