using AutoMapper;
using boilerplate.api.data.Stores;
using Microsoft.Extensions.Logging;

namespace boilerplate.api.domain.Services
{
    public class MusicianService: BaseService<IMusicianStore>, IMusicianService
    {
        public MusicianService(IMusicianStore store, ILogger<MusicianService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {
            
        }
    }
}
