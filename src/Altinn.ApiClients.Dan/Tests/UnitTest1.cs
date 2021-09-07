using System.Collections.Generic;
using System.Threading.Tasks;
using Altinn.ApiClients.Dan.Services;
using DanClient;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CalcTest()
        {
            Assert.AreEqual(4, Calc.Add(2,2));
        }

        [Test]
        public async Task DanTest()
        {
            DanOptions danOptions = new DanOptions()
            {
                BaseUri = "",
                Environment = "",
                SubscriptionKey = ""
            };
            // IOptions<DanOptions> options = Options.Create(danOptions);

            Altinn.ApiClients.Dan.Services.DanClient client = new Altinn.ApiClients.Dan.Services.DanClient(new DanHttpClient(), Options.Create(danOptions));
            var dataset = await client.GetSynchronousDataset("", "", new Dictionary<string, string>(), "");
            
        }
    }
}