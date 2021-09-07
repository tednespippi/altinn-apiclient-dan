using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DanClient.DelegatingHandlers
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Hent token fra MaskinPorten (Hvordan henter vi requestor?)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "token");
            request.Headers.Add("Ocp-Apim-Subscription-Key", "subscriptionkey");
            return base.SendAsync(request, cancellationToken);
        }
    }
}