using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Altinn.ApiClients.Dan.Interfaces;
using Altinn.ApiClients.Dan.Models;
using Microsoft.Extensions.Options;
using Refit;

namespace Altinn.ApiClients.Dan.Services
{
    public class DanClient : IDanClient
    {
        private IDanApi _danApi;
        private readonly HttpClient _httpClient;
        private readonly DanOptions _options;

        public DanClient(DanHttpClient httpClient, IOptions<DanOptions> options, IDanApi danApi)
        // public DanClient(DanHttpClient httpClient, IOptions<DanOptions> options, IDanApi danApi)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _danApi = danApi;

            // var baseUri = GetUriForEnvironment(_options.Environment);
            // if (_options.BaseUri != null)
            // {
            //     baseUri = _options.BaseUri;
            // }

            // _httpClient.BaseAddress = new Uri("https://reqres.in/");
            // _httpClient.BaseAddress = new Uri("https://test-api.data.altinn.no/v1/");
            // _httpClient.BaseAddress = ...; // get uri based on environment set in options
            // _httpClient.DefaultRequestHeaders.Add("Ocim-Api-Key", _options.SubscriptionKey);
        }

        public async Task<List<DataSet>> GetSynchronousDataset(string dataSetName, string subject,
            Dictionary<string, string> parameters, string requestor = null)
        {
            //Hent requestor fra token(?)
            List<DataSet> dataSets = await _danApi.GetDirectharvest(dataSetName, subject, requestor);

            await sendSomeDummyStuff();

            //Non-DI-stuff..
            var settings = new RefitSettings()
            {
                CollectionFormat = CollectionFormat.Multi,
                AuthorizationHeaderValueGetter = AuthorizationHeaderValueGetter
            };
            var danApi = RestService.For<IDanApi>("https://test-api.data.altinn.no/v1", settings);
            // danApi.GetDirectharvest();
            
            throw new NotImplementedException();
        }

        private Task<string> AuthorizationHeaderValueGetter()
        {
            return Task.FromResult("token");
            throw new NotImplementedException();
        }

        public async Task<List<DataSet>> GetAsynchronousDataset(string datasetname, string accreditationguid)
        {
            throw new NotImplementedException();
        }

        public async Task<Accreditation> CreateAsynchronousDatasetRequest(DataSetRequest dataSetRequest, string subject, string requestor = null)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSetRequestStatus> GetRequestStatus(string dataSetName, string accreditationGuid)
        {
            throw new NotImplementedException();
        }

        private async Task sendSomeDummyStuff()
        {
            // var url = "api/users";
            var url = "directharvest/UnitBasicInformation?requestor=991825827&subject=974760673";

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            Debug.WriteLine($"request: {_httpClient.BaseAddress}{url}");

            var response = await _httpClient.SendAsync(request);

            Debug.WriteLine($"response: {response}");
            // Task<string> asyncContent = response.Content.ReadAsStringAsync();
            Debug.WriteLine($"content: {response.Content.ReadAsStringAsync().Result}");
        }
        
    }
}
