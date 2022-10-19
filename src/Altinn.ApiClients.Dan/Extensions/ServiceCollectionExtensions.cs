using System;
using System.Linq;
using Altinn.ApiClients.Dan.Handlers;
using Altinn.ApiClients.Dan.Interfaces;
using Altinn.ApiClients.Dan.Models;
using Altinn.ApiClients.Dan.Services;
using Altinn.ApiClients.Maskinporten.Config;
using Altinn.ApiClients.Maskinporten.Handlers;
using Altinn.ApiClients.Maskinporten.Interfaces;
using Altinn.ApiClients.Maskinporten.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Refit;

namespace Altinn.ApiClients.Dan.Extensions
{
    /// <summary>
    /// Service collection extensions for DAN
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a DAN Client to the service collection
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="danConfigurationProvider">Provider for configuration settings</param>
        /// <param name="configureClientDefinition">Delegate to configure Maskinporten client definition</param>
        /// <typeparam name="T">The Maskinporten client definition that DAN should use</typeparam>
        public static void AddDanClient<T>(this IServiceCollection services, Func<IServiceProvider, IDanConfiguration> danConfigurationProvider = null, Action<T> configureClientDefinition = null) where T : class, IClientDefinition, new()
        {
            // We need a provider to cache tokens. If one is not already provided by the user, use MemoryTokenCacheProvider
            if (services.All(x => x.ServiceType != typeof(ITokenCacheProvider)))
            {
                services.AddMemoryCache();
                services.TryAddSingleton<ITokenCacheProvider, MemoryTokenCacheProvider>();
            }

            services.TryAddSingleton(sp => danConfigurationProvider != null
                ? danConfigurationProvider.Invoke(sp)
                : new DefaultDanConfiguration());
            services.TryAddSingleton<IDanClient, DanClient>();
            services.TryAddSingleton<IMaskinportenService, MaskinportenService>();

            DanSettings danSettings = null;
            services.AddRefitClient<IDanApi>(sp =>
                {
                    danSettings = sp.GetRequiredService<IOptions<DanSettings>>().Value;
                    return new RefitSettings();
                })
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(GetUriForEnvironment(danSettings.Environment));
                    c.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", danSettings.SubscriptionKey);
                })
                .AddHttpMessageHandler(sp =>
                {
                    var clientSettings = sp.GetRequiredService<IOptions<MaskinportenSettings<T>>>().Value;
                    var clientDefinition = new T
                    {
                        ClientSettings = clientSettings
                    };
                    configureClientDefinition?.Invoke(clientDefinition);

                    return new MaskinportenTokenHandler(sp.GetRequiredService<IMaskinportenService>(), clientDefinition);
                });
        }

        /// <summary>
        /// Adds a DAN Client to the service collection with a custom token retriever for when Maskinporten-client shouldn't be used
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <typeparam name="T">The implementation of IAccessTokenRetriever to be used</typeparam>
        public static void AddDanClientWithAccessTokenRetriever<T>(this IServiceCollection services) where T : class, IAccessTokenRetriever
        {
            services.TryAddSingleton<IDanClient, DanClient>();
            services.TryAddSingleton<IAccessTokenRetriever, T>();
            services.TryAddTransient<AccessTokenRetrieverHandler>();

            DanSettings danSettings = null;
            services.AddRefitClient<IDanApi>(sp =>
                {
                    danSettings = sp.GetRequiredService<IOptions<DanSettings>>().Value;

                    return new RefitSettings();
                })
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(GetUriForEnvironment(danSettings.Environment));
                    c.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", danSettings.SubscriptionKey);
                }).AddHttpMessageHandler<AccessTokenRetrieverHandler>();
        }

        private static string GetUriForEnvironment(string environment)
        {
            return environment switch
            {
                "local" => "http://localhost:7071/api",
                "dev" => "https://apim-nadobe-dev.azure-api.net/v1",
                "staging" => "https://test-api.data.altinn.no/v1",
                "prod" => "https://api.data.altinn.no/v1",
                _ => throw new ArgumentException("Invalid environment setting. Valid values: local, dev, staging, prod")
            };
        }
    }
}