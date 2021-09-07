using Altinn.ApiClients.Dan.Models;
using Altinn.ApiClients.Dan.Services;
using Altinn.ApiClients.Maskinporten.Config;
using Altinn.ApiClients.Maskinporten.Handlers;
using Altinn.ApiClients.Maskinporten.Service;
using Altinn.ApiClients.Maskinporten.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Altinn.ApiClients.Dan.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDanClient<T, T2>(this IServiceCollection services) 
            where T : ICustomClientSecret 
            where T2 : class, IClientSecret<T>
        {
            services.AddSingleton<IClientSecret<T>, T2>();
            services.AddSingleton<IMaskinportenService<T>, MaskinportenService<T>>();
            services.AddSingleton<MaskinportenTokenHandler<T>>();
            services.AddHttpClient<DanHttpClient>().AddHttpMessageHandler<MaskinportenTokenHandler<T>>();

        }
    }

}