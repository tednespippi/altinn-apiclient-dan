using System.Threading.Tasks;

namespace Altinn.ApiClients.Dan.Interfaces
{
    /// <summary>
    /// Interface for a custom access token retriever that can be used instead of MaskinportenClient
    /// </summary>
    public interface IAccessTokenRetriever
    {
        /// <summary>
        /// Gets an access token to be used in all requests to DAN API.
        /// </summary>
        /// <param name="forceRefresh">Whether or not to force a new token (ie. not use a cached token, if any)</param>
        /// <returns>An access token as a bearer string</returns>
        Task<string> GetAccessToken(bool forceRefresh = false);
    }
}
