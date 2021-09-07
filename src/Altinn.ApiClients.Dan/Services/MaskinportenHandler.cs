using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;

namespace DanClient
{
    public class MaskinportenHandler: DelegatingHandler
    {
        public MaskinportenHandler(string base64EncodedJwk, string clientId, string scope, string resource)
        {

        }
        public MaskinportenHandler(JsonWebKey jwk, string clientId, string scope, string resource)
        {

        }
        public MaskinportenHandler(X509Certificate2 cert, string clientId, string scope, string resource)
        {

        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!request.Headers.Contains("X-API-KEY"))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(
                        "You must supply an API key header called X-API-KEY")
                };
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
