using AutoMapper;
using boilerplate.api.data.Stores;
using Microsoft.Extensions.Logging;

namespace boilerplate.api.domain.Services
{
    public class MusicLabelService : BaseService<IMusicLabelStore>, IMusicLabelService
    {
        public MusicLabelService(IMusicLabelStore store, ILogger<MusicLabelService> logger, IMapper mapper)
            : base(store, logger, mapper)
        {

        }
    }
}
