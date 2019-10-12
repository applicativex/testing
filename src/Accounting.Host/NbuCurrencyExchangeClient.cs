using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Accounting.Host
{
    public class NbuCurrencyExchangeClient
    {
        public HttpClient Client { get; }

        public NbuCurrencyExchangeClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://bank.gov.ua/NBUStatService/v1/statdirectory/");
            
            Client = client;
        }

        public async Task<NbuCurrencyRate[]> GetExchangeRates()
        {
            var response = await Client.GetAsync("exchange?json");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<NbuCurrencyRate[]>();

            return result;
        }
    }

    [DataContract]
    public class NbuCurrencyRate
    {
        [DataMember(Name = "r030")]
        public int Id { get; set; }

        [DataMember(Name = "txt")]
        public string Description { get; set; }
        
        [DataMember(Name = "rate")]
        public decimal Rate { get; set; }

        [DataMember(Name = "cc")]
        public string Code { get; set; }

        [DataMember(Name = "exchangedate")]
        public string ExchangeDate { get; set; }
    }
}
