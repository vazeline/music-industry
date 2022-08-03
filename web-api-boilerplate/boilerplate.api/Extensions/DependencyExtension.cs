using boilerplate.api.common.Models;
using boilerplate.api.domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace boilerplate.api.Extensions
{
    public static class DependencyExtension
    {
        public static void RegisterApiDependencies(this IServiceCollection services, IConfiguration config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            var appsettings = config.Get<AppSettings>();

            services.RegisterDomainDependencies(appsettings.ConnectionStrings);
        }
    }
}
