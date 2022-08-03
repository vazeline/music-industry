using boilerplate.api.common.Models;
using boilerplate.api.data.AutoMapper;
using boilerplate.api.data.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace boilerplate.api.data.Extensions
{
    public static class DependencyExtension
    {
        public static void RegisterDataDependencies(this IServiceCollection services, ConnectionStrings connectionStrings)
        {
            if (!services.Any(s => s.ServiceType == typeof(ConnectionStrings)))
            {
                services.AddSingleton<ConnectionStrings>(connectionStrings);
            }

            services.AddAutoMapper(typeof(DataMapperProfile));

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionStrings.DefaultConnection));

            services.AddTransient<IMusicianStore, MusicianStore>();
            services.AddTransient<IMusicLabelStore, MusicLabelStore>();
            services.AddTransient<IPlatformStore, PlatformStore>();
            services.AddTransient<IContactStore, ContactStore>();
        }
    }
}
