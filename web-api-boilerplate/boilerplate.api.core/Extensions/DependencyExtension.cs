using boilerplate.api.core.Clients;
using boilerplate.api.core.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace boilerplate.api.core.Extensions
{
    public static class DependencyExtension
    {
        public static void AddApiClients(this IServiceCollection services, ApiConfig apiConfig)
        {
            services.AddSingleton<ApiConfig>(apiConfig);
            services.AddHttpClient(nameof(IMusicianClient),
                    c =>
                    {
                        c.BaseAddress = new Uri(apiConfig.BaseUrl);
                    })
                .AddTypedClient(c => Refit.RestService.For<IMusicianClient>(c));

            services.AddHttpClient(nameof(IMusicLabelClient),
                    c =>
                    {
                        c.BaseAddress = new Uri(apiConfig.BaseUrl);
                    })
                .AddTypedClient(c => Refit.RestService.For<IMusicLabelClient>(c));

            services.AddHttpClient(nameof(IPlatformClient),
                    c =>
                    {
                        c.BaseAddress = new Uri(apiConfig.BaseUrl);
                    })
                .AddTypedClient(c => Refit.RestService.For<IPlatformClient>(c));

            services.AddHttpClient(nameof(IContactClient),
                    c =>
                    {
                        c.BaseAddress = new Uri(apiConfig.BaseUrl);
                    })
                .AddTypedClient(c => Refit.RestService.For<IContactClient>(c));
        }
    }
}
