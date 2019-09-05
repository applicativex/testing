using System;
using NUnit.Framework;

namespace Accounting.ComponentTests
{
    [TestFixture]
    public class Tests
    {
        private string API_URL = "http://accounting";
        private dynamic _bus;

        [SetUp]
        public void SetUp()
        {
            var isLocalRun = Environment.GetEnvironmentVariable("RUN_ENV") == null;
            API_URL = isLocalRun ? "http://localhost:5000" : "http://accounting";
            var rabbitmqUrl = isLocalRun ? "localhost" : "rabbitmq";
            var rabbitmqHost = $"rabbitmq://{rabbitmqUrl}";

            _bus = new object();
            _bus.StartAsync();
        }

        [TearDown]
        public void Cleanup()
        {
            _bus.StopAsync();
        }

        [Test]
        public void Test()
        {

        }
    }
}