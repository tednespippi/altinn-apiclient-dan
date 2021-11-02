using System.Threading.Tasks;

namespace Altinn.ApiClients.Dan.Interfaces
{
    public interface IAccessTokenRetriever
    {
        Task<string> GetAccessToken(bool forceRefresh = false);
    }
}
