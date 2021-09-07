using System.Collections.Generic;
using System.Threading.Tasks;
using Altinn.ApiClients.Dan.Models;

namespace Altinn.ApiClients.Dan.Interfaces
{
    interface IDanClient
    {
        Task<List<DataSet>> GetSynchronousDataset(string dataSetName, string subject, Dictionary<string, string> parameters,
            string requestor = null);

        Task<List<DataSet>> GetAsynchronousDataset(string datasetname, string accreditationguid);

        Task<Accreditation> CreateAsynchronousDatasetRequest(DataSetRequest dataSetRequest, string subject, string requestor = null); 

        Task<DataSetRequestStatus> GetRequestStatus(string dataSetName, string accreditationGuid); 
    }
}
