using System;
using System.Threading.Tasks;
using Altinn.ApiClients.Dan.Models;
using Altinn.ApiClients.Maskinporten.Config;
using Altinn.ApiClients.Maskinporten.Models;
using Altinn.ApiClients.Maskinporten.Service;
using Microsoft.Extensions.Options;
using SampleWebApp.Service;

namespace SampleWebApp.Services
{
    public class MyDanClientSecretService : IClientSecret<IMyDanClientSecretService>
    {
        private DanOptions _danOptions;

        public MyDanClientSecretService(IOptions<DanOptions> danOptions)
        {
            _danOptions = danOptions.Value;
        }

        public Task<ClientSecrets> GetClientSecrets()
        {
            throw new NotImplementedException();
        }
    }
}
