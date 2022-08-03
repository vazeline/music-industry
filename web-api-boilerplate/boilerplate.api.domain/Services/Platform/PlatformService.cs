using AutoMapper;
using boilerplate.api.data.Stores;
using Microsoft.Extensions.Logging;

namespace boilerplate.api.domain.Services
{
    public class PlatformService : BaseService<IPlatformStore>, IPlatformService
    {
        public PlatformService(IPlatformStore store, ILogger<PlatformService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {

        }
    }
}
