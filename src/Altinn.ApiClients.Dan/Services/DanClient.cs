using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Altinn.ApiClients.Dan.Interfaces;
using Altinn.ApiClients.Dan.Models;
using Refit;

namespace Altinn.ApiClients.Dan.Services
{
    public class DanClient : IDanClient
    {
        private readonly IDanApi _danApi;

        public DanClient(IDanApi danApi)
        {
            _danApi = danApi;
        }

        public async Task<DataSet> GetSynchronousDataset(string dataSetName, string subject,
            string requestor = null, Dictionary<string, string> parameters = null)
        {
            try
            {
                return  await _danApi.GetDirectharvest(dataSetName, subject, requestor, parameters);
            }
            catch (ApiException ex)
            {
                throw DanException.FromApiException(ex); 
            }
        }

        public async Task<Accreditation> CreateAsynchronousDatasetRequest(DataSetRequest dataSetRequest, string subject,
            string requestor = null)
        {
            AuthorizationRequest authorizationRequest = new AuthorizationRequest()
            {
                DataSetRequests = new List<DataSetRequest>() { dataSetRequest },
                Subject = subject,
                Requestor = requestor
            };

            try
            {
                return await _danApi.PostAuthorization(authorizationRequest);
            }
            catch (ApiException ex)
            {
                throw DanException.FromApiException(ex);
            }
        }
        public async Task<DataSet> GetAsynchronousDataset(string accreditationguid, string datasetname)
        {
            try
            {
                return await _danApi.GetEvidence(accreditationguid, datasetname);
            }
            catch (ApiException ex)
            {
                throw DanException.FromApiException(ex);
            }
        }

        public async Task<List<DataSetRequestStatus>> GetRequestStatus(string accreditationGuid, string dataSetName)
        {
            try
            {
                var dataSetRequestStatuses = await _danApi.GetEvidenceStatus(accreditationGuid);
                return dataSetRequestStatuses.Where(status => status.DataSetName.Equals(dataSetName)).ToList();
            }
            catch (ApiException ex)
            {
                throw DanException.FromApiException(ex);
            }
        }

        public async Task<List<DataSetRequestStatus>> GetRequestStatus(string accreditationGuid)
        {
            try
            {
                return await _danApi.GetEvidenceStatus(accreditationGuid);
            }
            catch (ApiException ex)
            {
                throw DanException.FromApiException(ex);
            }
        }
    }
}