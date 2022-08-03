using boilerplate.api.data;
using boilerplate.api.data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boilerplate.api.domain
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetService<ILogger<ApplicationDbContext>>();

                try
                {
                    var context = services.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    InitializationHelper.MigrateProcedures(context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}
